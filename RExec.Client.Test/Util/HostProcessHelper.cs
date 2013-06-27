using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Test.ApplicationControl;

namespace RExec.Client.Test.Util
{
    internal static class HostProcessHelper
    {
        /// <summary>
        /// blocks until started
        /// </summary>
        /// <returns></returns>
        internal static AutomatedApplication Start()
        {
            string exeFile = ConfigurationManager.AppSettings.Get("RExec.Client.Test.host-exe-file");

            var result = new OutOfProcessApplication(new OutOfProcessApplicationSettings()
                {
                    ProcessStartInfo = new ProcessStartInfo(exeFile),
                    ApplicationImplementationFactory = new UIAutomationOutOfProcessApplicationFactory()
                });
            result.Start();
            
            return result;
        }

        /// <summary>
        /// blocks until closed
        /// </summary>
        /// <param name="process"></param>
        internal static void Stop(AutomatedApplication process)
        {
            process.Close();
        }
    }
}
