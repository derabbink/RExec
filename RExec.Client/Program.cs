﻿using RExec.ClientProxy;
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
            ClientFactory<IExecutor> factory = new ClientFactory<IExecutor>("*");

            using (Client<IExecutor> client = factory.GetClient())
            {
                Console.WriteLine("Testing IExecutor");
                Console.WriteLine("  .Execute()");
                client.Channel.Execute();
                
                Console.WriteLine();
            }
        }
    }
}