using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RExec.Dispatcher.Contracts.Data;
using RExec.Dispatcher.Contracts.Message;
using RExec.Dispatcher.Contracts.Service;

namespace RExec.Client.Samples.Client.Internal
{
    internal class Sample
    {
        /// <summary>
        /// demonstrates executing instructions located in THIS (client's own) assembly
        /// </summary>
        public static void Run(IAssemblyManager aManager, IExecutor executor)
        {
            Console.WriteLine("Executing code from client's own assembly");
            transportAssembly(aManager);
            Console.WriteLine();
            simpleInstructions(executor);
            Console.WriteLine();
            runtimeInfoGeneratorInstructions(executor);
            Console.WriteLine();
        }

        private static void transportAssembly(IAssemblyManager aManager)
        {
            //this won't work yet. dependency assemblies need to be transported first.
            Console.WriteLine("  Getting executing assembly");
            System.Reflection.Assembly assy = System.Reflection.Assembly.GetExecutingAssembly();
            String path = assy.Location;
            using (Stream assyFileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                Console.WriteLine("    Transmitting assembly as FileStream");
                Assembly msg = new Assembly {AssemblyStream = assyFileStream};
                aManager.AddAssembly(msg);
            }
        }

        private static void simpleInstructions(IExecutor executor)
        {
            Instruction instr = new Instruction() { AssemblyName = "RExec.Client", FQTypeName = "RExec.Client.InternalInstructions.Simple", ActionName = "Do" };
            Console.WriteLine("  {0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
            executor.Execute(instr);

            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = instr.FQTypeName, ActionName = "DoDependency" };
            Console.WriteLine("  {0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
            executor.Execute(instr);

            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = instr.FQTypeName, ActionName = "DoReferenceDependency" };
            Console.WriteLine("  {0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
            executor.Execute(instr);

            Console.WriteLine();
        }

        private static void runtimeInfoGeneratorInstructions(IExecutor executor)
        {
            Instruction instr = new Instruction() { AssemblyName = "RExec.Client", FQTypeName = "RExec.Client.InternalInstructions.RuntimeInfoGenerator", ActionName = "PrintAssemblyNameAndFQTypeNameAndActionName" };
            Console.WriteLine("  {0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
            executor.Execute(instr);

            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = instr.FQTypeName, ActionName = "PrintAssemblyNameAndFQTypeNameAndActionNameDependency" };
            Console.WriteLine("  {0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
            executor.Execute(instr);

            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = instr.FQTypeName, ActionName = "PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency" };
            Console.WriteLine("  {0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
            executor.Execute(instr);

            Console.WriteLine();
        }
    }
}
