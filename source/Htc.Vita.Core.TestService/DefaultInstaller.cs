using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.TestService
{
    [RunInstaller(true)]
    public class DefaultInstaller : Installer
    {
        internal static readonly string ServiceName = "HtcVitaCoreTestService";

        public DefaultInstaller()
        {
            Installers.Add(new ServiceProcessInstaller
            {
                    Account = ServiceAccount.LocalSystem,
                    Username = null,
                    Password = null
            });

            Installers.Add(new ServiceInstaller
            {
                    // DelayedAutoStart = true,
                    Description = "HTC Vita Core Test Service",
                    DisplayName = "HTC Vita Core Test Service",
                    ServiceName = ServiceName,
                    StartType = ServiceStartMode.Automatic
            });

            BeforeUninstall += OnBeforeUninstall;
            AfterInstall += OnAfterInstall;
        }

        private static void OnAfterInstall(object sender, InstallEventArgs args)
        {
            try
            {
                using (var serviceController = new ServiceController(ServiceName))
                {
                    serviceController.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void OnBeforeUninstall(object sender, InstallEventArgs args)
        {
            try
            {
                using (var serviceController = new ServiceController(ServiceName))
                {
                    serviceController.Stop();
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultInstaller)).Error(e.ToString());
            }
        }
    }
}
