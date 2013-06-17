using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Plugin;
using RExec.Dispatcher.Contracts.Message;
using RExec.Dispatcher.Contracts.Service;

namespace RExec.Dispatcher.Service.AssemblyManager
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AssemblyManagerService : IAssemblyManager
    {
        //private readonly PluginManager _pluginManager;

        public AssemblyManagerService()//PluginManager pluginManager)
        {
            //this._pluginManager = pluginManager;
        }

        public void AddAssembly(Assembly assembly)
        {
            Console.WriteLine("Receiving assembly to load");
            MemoryStream memStream = new MemoryStream();
            assembly.AssemblyStream.CopyTo(memStream);
            byte[] streamData = memStream.ToArray();

            //_pluginManager.Load(streamData);
        }
    }
}
