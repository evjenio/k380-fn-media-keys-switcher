using System;
using System.Threading.Tasks;
using FnSwitcher.Core;

namespace FnSwitcher.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var device = await DeviceManager.FindDeviceAsync();
            device.Dispose();
        }
    }
}
