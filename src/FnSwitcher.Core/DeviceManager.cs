using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HidLibrary;

namespace FnSwitcher.Core
{
    public static class DeviceManager
    {
        public static List<K380> Enumerate() => HidDevices.Enumerate(K380.VendorId, K380.ProductId).Select(kbd => new K380(kbd)).ToList();

        public static async Task<K380> FindDeviceAsync()
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

            List<K380> all = Enumerate();
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
