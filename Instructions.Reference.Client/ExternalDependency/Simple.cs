using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instructions.Reference.Client.ExternalDependency
{
    /// <summary>
    /// Contains instructions intended for invocation from some an assembly that THIS assembly is a reference to
    /// concretely: RExec.Client.InternalInstructions.Simple invokes this instruction
    /// </summary>
    public class Simple
    {
        private const string typeName = "Instructions.Reference.Client.ExternalDependency.Simple";

        public string Do()
        {
            string methodName = "Do";
            string result = string.Format("{0}.{1}()", typeName, methodName);
            Console.WriteLine("{0} is now being executed", result);
            return result;
        }
    }
}
