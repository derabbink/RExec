﻿using RExec.Dispatcher.Contracts.Data;
using RExec.Dispatcher.Contracts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RExec.Client.Samples.Host.Internal
{
    /// <summary>
    /// Demonstrates executing instructions located in the host's own assembly
    /// </summary>
    internal class Sample
    {
        internal static void Run(IExecutor executor)
        {
            Console.WriteLine("Executing code from host's own assembly");
            simpleInstructions(executor);
            Console.WriteLine();
            runtimeInfoGeneratorInstructions(executor);
            Console.WriteLine();
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