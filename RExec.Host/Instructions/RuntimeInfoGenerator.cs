using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RExec.Host.Instructions
{
    public class RuntimeInfoGenerator
    {
        public void PrintAssemblyNameAndFQTypeNameAndActionName()
        {
            Console.WriteLine("RExec.Host.Instructions.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()");
            string assemblyName = typeof(Simple).Assembly.GetName().Name;
            string fqTypename = typeof(Simple).FullName;
            string actionName = MethodBase.GetCurrentMethod().Name;

            Console.WriteLine("  AssemblyName: {0}", assemblyName);
            Console.WriteLine("  FQTypeName: {0}", fqTypename);
            Console.WriteLine("  ActionName: {0}", actionName);
        }
    }
}
