using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using RExec.Dispatcher.Service.AssemblyManager;
using RExec.Dispatcher.Service.Executor;

namespace RExec.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a ServiceHost for the service contract.
            ServiceHost executorHost = new ServiceHost(typeof(ExecutorService));
            ServiceHost amHost = new ServiceHost(typeof(AssemblyManagerService));
            
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
