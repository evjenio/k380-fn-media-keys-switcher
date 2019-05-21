using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FnSwitcher.Core
{
    public class Watcher : IDisposable
    {
        private K380 keyboard;
        private readonly Timer timer;

        public Watcher()
        {
            timer = new Timer(async p => await OnTimer(), null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            timer?.Dispose();
            keyboard?.Dispose();
        }

        public async Task<bool> InitAsync()
        {
            keyboard = await FindDeviceAsync();
            timer.Change(TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
            return keyboard != null;
        }

        private async Task OnTimer()
        {
            if (keyboard == null)
            {
                keyboard = await FindDeviceAsync();
            }

            await keyboard?.EnableFunctionKeysAsync();
        }

        private async Task<K380> FindDeviceAsync()
        {
            async Task<K380> SelectWorkingAsync(List<K380> devices)
            {
                foreach (K380 device in devices)
                {
                    var open = device.Open();
                    if (!open)
                    {
                        continue;
                    }

                    var write = await device.EnableFunctionKeysAsync();
                    if (!write)
                    {
                        continue;
                    }

                    return device;
                }

                return null;
            }

            List<K380> all = DeviceManager.Enumerate();
            try
            {
                var working = await SelectWorkingAsync(all);
                all.Remove(working);
                return working;
            }
            finally
            {
                all.ForEach(d => d.Dispose());
            }
        }
    }
}
