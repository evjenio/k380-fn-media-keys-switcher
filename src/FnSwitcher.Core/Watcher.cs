using System;
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
            keyboard = await DeviceManager.FindDeviceAsync();
            timer.Change(TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
            return keyboard != null;
        }

        private async Task OnTimer()
        {
            if (keyboard == null)
            {
                keyboard = await DeviceManager.FindDeviceAsync();
            }

            await keyboard?.EnableFunctionKeysAsync();
        }


    }
}
