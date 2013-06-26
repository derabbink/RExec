using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Plugin;
using RExec.Dispatcher.Contracts.Message;
using RExec.Dispatcher.Contracts.Service;
using Plugin;

namespace RExec.Dispatcher.Service.AssemblyManager
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AssemblyManagerService : IAssemblyManager, IDisposable
    {
        private PluginManager _pluginManager;

        public AssemblyManagerService(PluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }

        public void AddAssembly(Assembly assembly)
        {
            Console.WriteLine("Receiving assembly to load: {0} ({1})", assembly.FullName, assembly.Name);
            _pluginManager.Load(assembly.AssemblyStream, assembly.Name, assembly.FullName);
        }

        public void Clear()
        {
            _pluginManager.Reset();
        }

        public void Dispose()
        {
            _pluginManager.Dispose();
        }
    }
}
