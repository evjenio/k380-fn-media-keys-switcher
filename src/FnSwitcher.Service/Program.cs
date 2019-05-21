using System;
using Topshelf;

namespace FnSwitcher.Service
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            return (int)HostFactory.Run(conf =>
            {
                conf.Service<Startup>();
                conf.SetDisplayName("FnSwitcher K380");
                conf.SetServiceName("FnSwitcher K380");
                conf.SetDescription("fn media keys switcher for k380 keyboard");
            });
        }
    }
}
