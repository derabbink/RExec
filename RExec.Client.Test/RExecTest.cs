using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using RExec.ClientProxy;
using RExec.Dispatcher.Contracts.Data;
using RExec.Dispatcher.Contracts.Service;
using DependencyResolver;

namespace RExec.Client.Test
{
    [TestFixture]
    public abstract class RExecTest
    {
        private Client<IExecutor> _ex;
        protected IExecutor executor { get { return _ex.Channel; } }
        
        protected abstract string assemblyName { get; }
        protected abstract string fqTypeName { get; }
        
        [TestFixtureSetUp]
        public void Setup()
        {
            createClient();
        }

        private void createClient()
        {
            ClientFactory<IExecutor> executorFactory = new ClientFactory<IExecutor>("*");
            _ex = executorFactory.GetClient();
        }

        public void InvokeMethod(string methodName)
        {
            Instruction instr = new Instruction()
            {
                AssemblyName = assemblyName,
                FQTypeName = fqTypeName,
                ActionName = methodName
            };
            executor.Execute(instr);
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            disposeClient();
        }

        private void disposeClient()
        {
            _ex.Dispose();
        }
    }
}
