using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instructions.Reference.Host;

namespace RExec.Host.InternalInstructions
{
    public class Simple
    {
        private const string typeName = "RExec.Host.InternalInstructions.Simple";

        public string Do()
        {
            string methodName = "Do";
            string result = string.Format("{0}.{1}()", typeName, methodName);
            Console.WriteLine("{0} is now being executed", result);
            return result;
        }
        
        /// <summary>
        /// calls another method within the same (this) assembly
        /// </summary>
        public string DoDependency()
        {
            string methodName = "DoDependency";
            string result = string.Format("{0}.{1}()", typeName, methodName);
            Console.WriteLine("{0} is now being executed", result);
            result = string.Format("{0}->{1}", result,
                                   new RExec.Host.InternalInstructions.InternalDependency.Simple().Do());
            return result;
        }

        /// <summary>
        /// calls external method in another (external) assembly
        /// </summary>
        public string DoReferenceDependency()
        {
            string methodName = "DoReferenceDependency";
            string result = string.Format("{0}.{1}()", typeName, methodName);
            Console.WriteLine("{0} is now being executed", result);
            result = string.Format("{0}->{1}", result,
                                   new Instructions.Reference.Host.ExternalDependency.Simple().Do());
            return result;
        }
    }
}
