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
    public abstract class RExecTest
    {
        private Client<IExecutor> _ex;
        private Client<IAssemblyManager> _am;
        protected IExecutor executor { get { return _ex.Channel; } }
        protected IAssemblyManager assemblyManager { get { return _am.Channel; } }

        protected abstract string assemblyName { get; }
        protected abstract string assemblyPath { get; }
        protected abstract string fqTypeName { get; }
        
        [TestFixtureSetUp]
        public void Setup()
        {
            createClients();
            transmitAssemblies();
        }

        private void createClients()
        {
            ClientFactory<IExecutor> executorFactory = new ClientFactory<IExecutor>("*");
            ClientFactory<IAssemblyManager> amFactory = new ClientFactory<IAssemblyManager>("*");
            _ex = executorFactory.GetClient();
            _am = amFactory.GetClient();
        }

        private void transmitAssemblies()
        {
            AssemblyName start = AssemblyName.GetAssemblyName(assemblyPath);
            IObservable<AssemblyName> dependencies = Resolver.GetAllDependencies(start);
            dependencies.Subscribe(an =>
            {
                String path = new Uri(an.CodeBase).LocalPath;
                using (Stream assyFileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var msg = new Dispatcher.Contracts.Message.Assembly { AssemblyStream = assyFileStream, Name = an.Name, FullName = an.FullName };
                    assemblyManager.AddAssembly(msg);
                }
            });
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
            clearAssemblies();
            disposeClients();
        }

        private void clearAssemblies()
        {
            _am.Channel.Clear();
        }

        private void disposeClients()
        {
            _am.Dispose();
            _ex.Dispose();
        }
    }
}
