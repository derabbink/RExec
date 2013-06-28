using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RExec.Host.InternalInstructions.InternalDependency
{
    /// <summary>
    /// Contains instructions intended for invocation from some place within the same (this) assembly
    /// </summary>
    public class RuntimeInfoGenerator
    {
        private const string typeName = "RExec.Host.InternalInstructions.InternalDependency.RuntimeInfoGenerator";

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
    }
}
