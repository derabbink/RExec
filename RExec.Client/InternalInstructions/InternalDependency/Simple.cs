using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RExec.Client.InternalInstructions.InternalDependency
{
    /// <summary>
    /// Contains instructions intended for invocation from some place within the same (this) assembly
    /// </summary>
    public class Simple
    {
        private const string typeName = "RExec.Client.InternalInstructions.InternalDependency.Simple";

        public string Do()
        {
            string methodName = "Do";
            string result = string.Format("{0}.{1}()", typeName, methodName);
            Console.WriteLine("{0} is now being executed", result);
            return result;
        }
    }
}
