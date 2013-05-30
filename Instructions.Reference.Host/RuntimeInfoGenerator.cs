using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Instructions.Reference.Host
{
    public class RuntimeInfoGenerator
    {
        public void PrintAssemblyNameAndFQTypeNameAndActionName()
        {
            Console.WriteLine("Instructions.Reference.Host.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()");
            string assemblyName = typeof(Simple).Assembly.GetName().Name;
            string fqTypename = typeof(Simple).FullName;
            string actionName = MethodBase.GetCurrentMethod().Name;

            Console.WriteLine("  AssemblyName: {0}", assemblyName);
            Console.WriteLine("  FQTypeName: {0}", fqTypename);
            Console.WriteLine("  ActionName: {0}", actionName);
        }
    }
}
