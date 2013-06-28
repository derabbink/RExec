using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Plugin;
using RExec.Dispatcher.Contracts.Service;
using RExec.Dispatcher.Contracts.Data;
using System.Reflection;

namespace RExec.Dispatcher.Service.Executor
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ExecutorService : IExecutor
    {
        private PluginManager _pluginManager;

        public ExecutorService(PluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }

        public object Execute(Instruction instruction)
        {
            return _pluginManager.Execute(instruction.AssemblyName, instruction.FQTypeName, instruction.ActionName);
        }
    }
}
