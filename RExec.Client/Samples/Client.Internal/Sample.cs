using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RExec.Dispatcher.Contracts.Data;
using RExec.Dispatcher.Contracts.Service;
using DependencyResolver;
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
            clearAssemblyManager(aManager);
            Console.WriteLine();
        }

        private static void transportAssembly(IAssemblyManager aManager)
        {
            Console.WriteLine("  Getting executing assembly");
            System.Reflection.Assembly assy = System.Reflection.Assembly.GetExecutingAssembly();
            IObservable<AssemblyName> dependencies = Resolver.GetAllDependencies(assy.GetName());
            dependencies.Subscribe(an =>
                {
                    String path = new Uri(an.CodeBase).LocalPath;
                    Console.WriteLine("    Transmitting assembly as FileStream ({0} bytes, {1})", new FileInfo(path).Length, path);
                    using (Stream assyFileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        Assembly msg = new Assembly { AssemblyStream = assyFileStream, Name = an.Name, FullName = an.FullName };
                        aManager.AddAssembly(msg);
                    }
                });
        }

        private static void simpleInstructions(IExecutor executor)
        {
            Instruction instr = new Instruction() { AssemblyName = "RExec.Client", FQTypeName = "RExec.Client.InternalInstructions.Simple", ActionName = "Do" };
            Console.WriteLine("  {0}:{1}.{2}()", instr.AssemblyName, instr.FQTypeName, instr.ActionName);
            string result = executor.Execute(instr) as string;
            Console.WriteLine("  = {0}", result);

            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = instr.FQTypeName, ActionName = "DoDependency" };
            Console.WriteLine("  {0}:{1}.{2}()", instr.AssemblyName, instr.FQTypeName, instr.ActionName);
            result = executor.Execute(instr) as string;
            Console.WriteLine("  = {0}", result);

            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = instr.FQTypeName, ActionName = "DoReferenceDependency" };
            Console.WriteLine("  {0}:{1}.{2}()", instr.AssemblyName, instr.FQTypeName, instr.ActionName);
            result = executor.Execute(instr) as string;
            Console.WriteLine("  = {0}", result);

            Console.WriteLine();
        }

        private static void runtimeInfoGeneratorInstructions(IExecutor executor)
        {
            Instruction instr = new Instruction() { AssemblyName = "RExec.Client", FQTypeName = "RExec.Client.InternalInstructions.RuntimeInfoGenerator", ActionName = "PrintAssemblyNameAndFQTypeNameAndActionName" };
            Console.WriteLine("  {0}:{1}.{2}()", instr.AssemblyName, instr.FQTypeName, instr.ActionName);
            string result = executor.Execute(instr) as string;
            Console.WriteLine("  = {0}", result);
            
            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = instr.FQTypeName, ActionName = "PrintAssemblyNameAndFQTypeNameAndActionNameDependency" };
            Console.WriteLine("  {0}:{1}.{2}()", instr.AssemblyName, instr.FQTypeName, instr.ActionName);
            result = executor.Execute(instr) as string;
            Console.WriteLine("  = {0}", result);

            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = instr.FQTypeName, ActionName = "PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency" };
            Console.WriteLine("  {0}:{1}.{2}()", instr.AssemblyName, instr.FQTypeName, instr.ActionName);
            result = executor.Execute(instr) as string;
            Console.WriteLine("  = {0}", result);

            Console.WriteLine();
        }

        private static void clearAssemblyManager(IAssemblyManager aManager)
        {
            Console.WriteLine("  Sending assembly clear command");
            aManager.Clear();
        }
    }
}
