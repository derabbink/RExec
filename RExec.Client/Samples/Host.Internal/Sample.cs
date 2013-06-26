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

namespace RExec.Client.Samples.Host.Internal
{
    /// <summary>
    /// Demonstrates executing instructions located in the host's own assembly
    /// </summary>
    internal class Sample
    {
        public static void Run(IAssemblyManager aManager, IExecutor executor)
        {
            Console.WriteLine("Executing code from host's own assembly");
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
            AssemblyName start = AssemblyName.GetAssemblyName("..\\..\\..\\RExec.Host\\bin\\Debug\\RExec.Host.exe");
            IObservable<AssemblyName> dependencies = Resolver.GetAllDependencies(start);
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
            Instruction instr = new Instruction() { AssemblyName = "RExec.Host", FQTypeName = "RExec.Host.InternalInstructions.Simple", ActionName = "Do" };
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
            Instruction instr = new Instruction() { AssemblyName = "RExec.Host", FQTypeName = "RExec.Host.InternalInstructions.RuntimeInfoGenerator", ActionName = "PrintAssemblyNameAndFQTypeNameAndActionName" };
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
