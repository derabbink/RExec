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
            // Create ServiceHosts for the service contracts.
            ServiceHost executorHost = new ServiceHost(typeof(ExecutorService));

            AssemblyManagerService amService = new AssemblyManagerService(new PluginManager());
            ServiceHost amHost = new ServiceHost(amService);
            
            executorHost.Open();
            Console.WriteLine("The IExecutor service is ready.");
            
            amHost.Open();
            Console.WriteLine("The IAssmeblyManager service is ready");
            
            Console.WriteLine("Press <ENTER> to terminate the service.");
            Console.ReadLine();

            amHost.Close();
            executorHost.Close();
        }
    }
}
