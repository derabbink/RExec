using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Instructions.Reference.Host.ExternalDependency
{
    /// <summary>
    /// Contains instructions intended for invocation from some an assembly that THIS assembly is a reference to
    /// concretely: RExec.Host.InternalInstructions.RuntimeInfoGenerator invokes this instruction
    /// </summary>
    public class RuntimeInfoGenerator
    {
        private const string typeName = "Instructions.Reference.Host.ExternalDependency.RuntimeInfoGenerator";

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
