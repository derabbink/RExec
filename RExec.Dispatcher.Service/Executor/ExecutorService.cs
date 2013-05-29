using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RExec.Dispatcher.Contracts.Service;
using RExec.Dispatcher.Contracts.Data;
using System.Reflection;

namespace RExec.Dispatcher.Service.Executor
{
    public class ExecutorService : IExecutor
    {
        public void Execute(Instruction instruction)
        {
            Type t = Type.GetType(string.Format("{0}, {1}", instruction.FQTypeName, instruction.AssemblyName), true);
            object instance = Activator.CreateInstance(t);
            MethodInfo action = t.GetMethod(instruction.ActionName);
            action.Invoke(instance, new object[]{});
        }
    }
}
