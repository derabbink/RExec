using System.IO;
using System.Reflection;
using DependencyResolver;
using RExec.Dispatcher.Contracts.Data;
using RExec.Dispatcher.Contracts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assembly = RExec.Dispatcher.Contracts.Message.Assembly;

namespace RExec.Client.Samples.Host.Reference
{
    /// <summary>
    /// Demonstrates executing instructions located in one of the host's reference assemblies
    /// </summary>
    internal class Sample
    {
        internal static void Run(IAssemblyManager aManager, IExecutor executor)
        {
            Console.WriteLine("Executing code from host's reference assembly");
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
            AssemblyName start = AssemblyName.GetAssemblyName("..\\..\\..\\Instructions.Reference.Host\\bin\\Debug\\Instructions.Reference.Host.dll");
            IObservable<AssemblyName> dependencies = Resolver.GetAllDependencies(start);
            dependencies.Subscribe(an =>
            {
                String path = new Uri(an.CodeBase).LocalPath;
                Console.WriteLine("    Transmitting assembly as FileStream ({0} bytes, {1})", new FileInfo(path).Length, path);
                using (Stream assyFileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    Dispatcher.Contracts.Message.Assembly msg = new Assembly { AssemblyStream = assyFileStream, Name = an.Name, FullName = an.FullName };
                    aManager.AddAssembly(msg);
                }
            });
        }

        private static void simpleInstructions(IExecutor executor)
        {
            
            Instruction instr = new Instruction() { AssemblyName = "Instructions.Reference.Host", FQTypeName = "Instructions.Reference.Host.Simple", ActionName = "Do" };
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
            Instruction instr = new Instruction() { AssemblyName = "Instructions.Reference.Host", FQTypeName = "Instructions.Reference.Host.RuntimeInfoGenerator", ActionName = "PrintAssemblyNameAndFQTypeNameAndActionName" };
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
