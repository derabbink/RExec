using RExec.Dispatcher.Contracts.Data;
using RExec.Dispatcher.Contracts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RExec.Client.Samples
{
    internal class ExecuteInternal
    {
        internal static void Run(IExecutor executor)
        {
            Console.WriteLine("Executing code from host's reference assembly");

            Instruction instr = new Instruction() { AssemblyName = "Instructions.Reference.Host", FQTypeName = "Instructions.Reference.Host.Simple", ActionName = "Do" };
            Console.WriteLine("{0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
            executor.Execute(instr);

            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = "Instructions.Reference.Host.RuntimeInfoGenerator", ActionName = "PrintAssemblyNameAndFQTypeNameAndActionName" };
            Console.WriteLine("{0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
            executor.Execute(instr);

            Console.WriteLine();
        }
    }
}
