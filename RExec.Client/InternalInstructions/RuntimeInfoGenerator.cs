using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RExec.Client.InternalInstructions
{
    public class RuntimeInfoGenerator
    {
        private const string typeName = "RExec.Client.InternalInstructions.RuntimeInfoGenerator";

        public string PrintAssemblyNameAndFQTypeNameAndActionName()
        {
            string methodName = "PrintAssemblyNameAndFQTypeNameAndActionName";
            string result = string.Format("{0}.{1}()", typeName, methodName);
            Console.WriteLine("{0} is now being executed", result);
            
            string assemblyName = typeof(RuntimeInfoGenerator).Assembly.GetName().Name;
            string fqTypename = typeof(RuntimeInfoGenerator).FullName;
            string actionName = MethodBase.GetCurrentMethod().Name;
            string computation = string.Format("({0},{1},{2})", assemblyName, fqTypename, actionName);
            Console.WriteLine("  {0}", computation);

            result = string.Format("{0}={1}", result, computation);
            return result;
        }

        /// <summary>
        /// calls another method within the same (this) assembly
        /// </summary>
        public string PrintAssemblyNameAndFQTypeNameAndActionNameDependency()
        {
            string methodName = "PrintAssemblyNameAndFQTypeNameAndActionNameDependency";
            string result = string.Format("{0}.{1}()", typeName, methodName);
            Console.WriteLine("{0} is now being executed", result);
            result = string.Format("{0}->{1}", result,
                                   new RExec.Client.InternalInstructions.InternalDependency.RuntimeInfoGenerator().PrintAssemblyNameAndFQTypeNameAndActionName());
            return result;
        }

        /// <summary>
        /// calls external method in another (external) assembly
        /// </summary>
        public string PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency()
        {
            string methodName = "PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency";
            string result = string.Format("{0}.{1}()", typeName, methodName);
            Console.WriteLine("{0} is now being executed", result);
            result = string.Format("{0}->{1}", result,
                                   new Instructions.Reference.Client.ExternalDependency.RuntimeInfoGenerator().PrintAssemblyNameAndFQTypeNameAndActionName());
            return result;
        }
    }
}
