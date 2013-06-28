using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DependencyResolver;
using NUnit.Framework;
using RExec.ClientProxy;
using RExec.Dispatcher.Contracts.Service;

namespace RExec.Client.Test
{
    //can't have abstract [SetUpFixture] class
    public abstract class AssemblySetupTearDown
    {
        private Client<IAssemblyManager> _am;
        protected IAssemblyManager assemblyManager { get { return _am.Channel; } }
        protected abstract string assemblyPath { get; }

        //can't have [SetUp] in abstract class
        public void Setup()
        {
            createClient();
            transmitAssemblies();
        }

        private void createClient()
        {
            ClientFactory<IAssemblyManager> amFactory = new ClientFactory<IAssemblyManager>("*");
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

        //can't have [TearDown] in abstract class
        public void TearDown()
        {
            clearAssemblies();
            disposeClient();
        }

        private void clearAssemblies()
        {
            _am.Channel.Clear();
        }

        private void disposeClient()
        {
            _am.Dispose();
        }
    }
}
