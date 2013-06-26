using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Plugin;
using RExec.Dispatcher.Service.AssemblyManager;
using RExec.Dispatcher.Service.Executor;

namespace RExec.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginManager pm = new PluginManager();

            // Create ServiceHosts for the service contracts.
            ExecutorService eService = new ExecutorService(pm);
            ServiceHost eHost = new ServiceHost(eService);

            AssemblyManagerService amService = new AssemblyManagerService(pm);
            ServiceHost amHost = new ServiceHost(amService);
            
            eHost.Open();
            Console.WriteLine("The IExecutor service is ready.");
            
            amHost.Open();
            Console.WriteLine("The IAssmeblyManager service is ready");
            
            Console.WriteLine("Press <ENTER> to terminate the service.");
            Console.ReadLine();

            amHost.Close();
            eHost.Close();
            pm.Dispose();
        }
    }
}
