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
        public void Do()
        {
            Console.WriteLine("RExec.Host.InternalInstructions.Simple.Do() is now being executed");
        }

        /// <summary>
        /// calls another method within the same (this) assembly
        /// </summary>
        public void DoDependency()
        {
            Console.WriteLine("RExec.Host.InternalInstructions.Simple.DoDependency() is now being executed");
            new RExec.Host.InternalInstructions.InternalDependency.Simple().Do();
        }

        /// <summary>
        /// calls external method in another (external) assembly
        /// </summary>
        public void DoReferenceDependency()
        {
            Console.WriteLine("RExec.Host.InternalInstructions.Simple.DoReferenceDependency() is now being executed");
            new Instructions.Reference.Host.ExternalDependency.Simple().Do();
        }
    }
}
