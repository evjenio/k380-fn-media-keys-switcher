using System;
using FnSwitcher.Core;
using Topshelf;

namespace FnSwitcher.Service
{
    internal class Startup : ServiceControl
    {
        private readonly Watcher watcher = new Watcher();

        /// <inheritdoc/>
        public bool Start(HostControl hostControl) => watcher.InitAsync().GetAwaiter().GetResult();

        /// <inheritdoc/>
        public bool Stop(HostControl hostControl)
        {
            watcher.Dispose();
            return true;
        }
    }
}
