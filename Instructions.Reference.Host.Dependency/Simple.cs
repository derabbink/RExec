using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instructions.Reference.Host.Dependency
{
    /// <summary>
    /// Contains instructions intended for invocation from some an assembly that THIS assembly is a reference to
    /// </summary>
    public class Simple
    {
        private const string typeName = "Instructions.Reference.Host.Dependency.Simple";

        public string Do()
        {
            string methodName = "Do";
            string result = string.Format("{0}.{1}()", typeName, methodName);
            Console.WriteLine("{0} is now being executed", result);
            return result;
        }
    }
}
