using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RExec.Client.InternalInstructions
{
    public class RuntimeInfoGenerator
    {
        public void PrintAssemblyNameAndFQTypeNameAndActionName()
        {
            Console.WriteLine("RExec.Client.InternalInstructions.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()");
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
            Console.WriteLine("RExec.Client.InternalInstructions.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionNameDependency()");
            new RExec.Client.InternalInstructions.InternalDependency.RuntimeInfoGenerator().PrintAssemblyNameAndFQTypeNameAndActionName();
        }

        /// <summary>
        /// calls external method in another (external) assembly
        /// </summary>
        public void PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency()
        {
            Console.WriteLine("RExec.Client.InternalInstructions.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency()");
            new Instructions.Reference.Client.ExternalDependency.RuntimeInfoGenerator().PrintAssemblyNameAndFQTypeNameAndActionName();
        }
    }
}
