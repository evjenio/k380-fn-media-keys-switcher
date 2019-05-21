using System;
using System.Collections.Generic;
using System.Linq;
using HidLibrary;

namespace FnSwitcher.Core
{
    public static class DeviceManager
    {
        public static List<K380> Enumerate() => HidDevices.Enumerate(K380.VendorId, K380.ProductId).Select(kbd => new K380(kbd)).ToList();
    }
}
