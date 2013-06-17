using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plugin;
using RExec.Dispatcher.Contracts.Data;
using RExec.Dispatcher.Contracts.Service;
using Assembly = RExec.Dispatcher.Contracts.Message.Assembly;

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
            Console.WriteLine("  Getting executing assembly");
            System.Reflection.Assembly assy = System.Reflection.Assembly.GetExecutingAssembly();
            IObservable<AssemblyName> dependencies = DependencyResolver.GetAllDependencies(assy.GetName());
            dependencies.Subscribe(an =>
                {
                    String path = new Uri(an.CodeBase).LocalPath;
                    Console.WriteLine("    Transmitting assembly as FileStream ({0})", path);
                    using (Stream assyFileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        Assembly msg = new Assembly { AssemblyStream = assyFileStream };
                        aManager.AddAssembly(msg);
                    }
                });
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
