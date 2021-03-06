﻿using System.IO;
using System.Reflection;
using DependencyResolver;
using RExec.Dispatcher.Contracts.Data;
using RExec.Dispatcher.Contracts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assembly = RExec.Dispatcher.Contracts.Message.Assembly;

namespace RExec.Client.Samples.Client.Reference.Dependency
{
    /// <summary>
    /// Demonstrates executing instructions located in one of the client's reference's reference assemblies
    /// </summary>
    internal class Sample
    {
        internal static void Run(IAssemblyManager aManager, IExecutor executor)
        {
            Console.WriteLine("Executing code from client's reference's reference assembly");
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
            AssemblyName start = AssemblyName.GetAssemblyName("..\\..\\..\\Instructions.Reference.Client.Dependency\\bin\\Debug\\Instructions.Reference.Client.Dependency.dll");
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
            
            Instruction instr = new Instruction() { AssemblyName = "Instructions.Reference.Client.Dependency", FQTypeName = "Instructions.Reference.Client.Dependency.Simple", ActionName = "Do" };
            Console.WriteLine("  {0}:{1}.{2}()", instr.AssemblyName, instr.FQTypeName, instr.ActionName);
            string result = executor.Execute(instr) as string;
            Console.WriteLine("  = {0}", result);

            Console.WriteLine();
        }

        private static void runtimeInfoGeneratorInstructions(IExecutor executor)
        {
            Instruction instr = new Instruction() { AssemblyName = "Instructions.Reference.Client.Dependency", FQTypeName = "Instructions.Reference.Client.Dependency.RuntimeInfoGenerator", ActionName = "PrintAssemblyNameAndFQTypeNameAndActionName" };
            Console.WriteLine("  {0}:{1}.{2}()", instr.AssemblyName, instr.FQTypeName, instr.ActionName);
            string result = executor.Execute(instr) as string;
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
