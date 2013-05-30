using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Instructions.Reference.Host.Dependency
{
    /// <summary>
    /// Contains instructions intended for invocation from some an assembly that THIS assembly is a reference to
    /// </summary>
    public class RuntimeInfoGenerator
    {
        public void PrintAssemblyNameAndFQTypeNameAndActionName()
        {
            Console.WriteLine("  Instructions.Reference.Host.Dependency.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()");
            string assemblyName = typeof(RuntimeInfoGenerator).Assembly.GetName().Name;
            string fqTypename = typeof(RuntimeInfoGenerator).FullName;
            string actionName = MethodBase.GetCurrentMethod().Name;

            Console.WriteLine("    AssemblyName: {0}", assemblyName);
            Console.WriteLine("    FQTypeName: {0}", fqTypename);
            Console.WriteLine("    ActionName: {0}", actionName);
        }
    }
}
