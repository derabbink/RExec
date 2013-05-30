using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Instructions.Reference.Client
{
    public class RuntimeInfoGenerator
    {
        public void PrintAssemblyNameAndFQTypeNameAndActionName()
        {
            Console.WriteLine("Instructions.Reference.Client.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()");
            string assemblyName = typeof(RuntimeInfoGenerator).Assembly.GetName().Name;
            string fqTypename = typeof(RuntimeInfoGenerator).FullName;
            string actionName = MethodBase.GetCurrentMethod().Name;

            Console.WriteLine("  AssemblyName: {0}", assemblyName);
            Console.WriteLine("  FQTypeName: {0}", fqTypename);
            Console.WriteLine("  ActionName: {0}", actionName);
        }

        /// <summary>
        /// calls another method within the same (this) assembly
        /// </summary>
        public void PrintAssemblyNameAndFQTypeNameAndActionNameDependency()
        {
            Console.WriteLine("Instructions.Reference.Client.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionNameDependency()");
            new Instructions.Reference.Client.InternalDependency.RuntimeInfoGenerator().PrintAssemblyNameAndFQTypeNameAndActionName();
        }

        /// <summary>
        /// calls external method in another (external) assembly
        /// </summary>
        public void PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency()
        {
            Console.WriteLine("Instructions.Reference.Client.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency()");
            new Instructions.Reference.Client.Dependency.RuntimeInfoGenerator().PrintAssemblyNameAndFQTypeNameAndActionName();
        }
    }
}
