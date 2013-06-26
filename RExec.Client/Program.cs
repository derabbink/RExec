using RExec.Client.Samples.Host.Internal;
using RExec.Client.Samples.Host.Reference;
using RExec.ClientProxy;
using RExec.Dispatcher.Contracts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RExec.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // "*" takes the first endpoint configuration that matches the service contract
            ClientFactory<IExecutor> executorFactory = new ClientFactory<IExecutor>("*");
            ClientFactory<IAssemblyManager> amFactory = new ClientFactory<IAssemblyManager>("*");

            Client<IExecutor> ex = executorFactory.GetClient();
            Client<IAssemblyManager> am = amFactory.GetClient();

            Console.WriteLine("Invoking IExecutor");
            Console.WriteLine("");
            Samples.Host.Internal.Sample.Run(am.Channel, ex.Channel);
            Samples.Host.Reference.Sample.Run(am.Channel, ex.Channel);
            Samples.Client.Internal.Sample.Run(am.Channel, ex.Channel);
            
            am.Dispose();
            ex.Dispose();
            
            Console.WriteLine("Client finished running. Press <ENTER> to quit.");
            Console.ReadLine();
        }
    }
}
