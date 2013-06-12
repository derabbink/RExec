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
            simpleInstructions(aManager, executor);
            Console.WriteLine();
            runtimeInfoGeneratorInstructions(executor);
            Console.WriteLine();
        }

        private static void simpleInstructions(IAssemblyManager aManager, IExecutor executor)
        {
            using (Stream alphabetStream = new MemoryStream())
            {
                //with MemoryStream write data first, then reset position
                Console.WriteLine("  Writing data to stream");
                Encoding enc = new UnicodeEncoding();
                //write the alphabet to stream
                for (char c = 'A'; c <= 'Z'; c++)
                {
                    byte[] buffer = enc.GetBytes(new[] {c}, 0, 1);
                    alphabetStream.Write(buffer, 0, buffer.Length);
                }
                alphabetStream.Position = 0;

                Console.WriteLine("  Streaming data to host...");
                Assembly assy = new Assembly() { AssemblyStream = alphabetStream };
                aManager.AddAssembly(assy);
            }
//            Instruction instr = new Instruction() { AssemblyName = "RExec.Client", FQTypeName = "RExec.Client.InternalInstructions.Simple", ActionName = "Do" };
//            Console.WriteLine("  {0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
//            executor.Execute(instr);
//
//            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = instr.FQTypeName, ActionName = "DoDependency" };
//            Console.WriteLine("  {0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
//            executor.Execute(instr);
//
//            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = instr.FQTypeName, ActionName = "DoReferenceDependency" };
//            Console.WriteLine("  {0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
//            executor.Execute(instr);

            Console.WriteLine();
        }

        private static void runtimeInfoGeneratorInstructions(IExecutor executor)
        {
//            Instruction instr = new Instruction() { AssemblyName = "RExec.Client", FQTypeName = "RExec.Client.InternalInstructions.RuntimeInfoGenerator", ActionName = "PrintAssemblyNameAndFQTypeNameAndActionName" };
//            Console.WriteLine("  {0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
//            executor.Execute(instr);
//
//            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = instr.FQTypeName, ActionName = "PrintAssemblyNameAndFQTypeNameAndActionNameDependency" };
//            Console.WriteLine("  {0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
//            executor.Execute(instr);
//
//            instr = new Instruction() { AssemblyName = instr.AssemblyName, FQTypeName = instr.FQTypeName, ActionName = "PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency" };
//            Console.WriteLine("  {0}(), {1}, {2}", instr.ActionName, instr.FQTypeName, instr.AssemblyName);
//            executor.Execute(instr);

            Console.WriteLine();
        }
    }
}
