using System;
using System.Threading.Tasks;
using HidLibrary;

namespace FnSwitcher.Core
{
    public sealed class K380 : IDisposable
    {
        public const int ProductId = 0xb342;
        public const int VendorId = 0x046d;
        private const int WriteTimeoutMs = 1000;

        private static readonly byte[] enableFunctionKeys =
        {
            0x10,
            0xff,
            0x0b,
            0x1e,
            0x00,
            0x00,
            0x00
        };

        private static readonly byte[] enableMediaKeys =
        {
            0x10,
            0xff,
            0x0b,
            0x1e,
            0x01,
            0x00,
            0x00
        };

        private readonly HidDevice keyboard;

        public K380(HidDevice keyboard)
        {
            this.keyboard = keyboard;
        }

        public void Close() => keyboard.CloseDevice();

        /// <inheritdoc/>
        public void Dispose()
        {
            Close();
            keyboard?.Dispose();
        }

        public async Task<bool> EnableFunctionKeysAsync() => await keyboard.WriteAsync(enableFunctionKeys, WriteTimeoutMs);

        public async Task<bool> EnableMediaKeysAsync() => await keyboard.WriteAsync(enableMediaKeys, WriteTimeoutMs);

        public bool Open()
        {
            keyboard.OpenDevice(DeviceMode.Overlapped,
                DeviceMode.Overlapped,
                ShareMode.ShareRead | ShareMode.ShareWrite);
            return keyboard.IsOpen;
        }
    }
}
