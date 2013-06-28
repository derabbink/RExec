using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instructions.Reference.Client
{
    public class Simple
    {
        private const string typeName = "Instructions.Reference.Client.Simple";

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
                                   new Instructions.Reference.Client.InternalDependency.Simple().Do());
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
                                   new Instructions.Reference.Client.Dependency.Simple().Do());
            return result;
        }
    }
}
