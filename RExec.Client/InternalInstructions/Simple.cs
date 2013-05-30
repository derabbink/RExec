using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instructions.Reference.Client;

namespace RExec.Client.InternalInstructions
{
    public class Simple
    {
        public void Do()
        {
            Console.WriteLine("RExec.Client.InternalInstructions.Simple.Do() is now being executed");
        }

        /// <summary>
        /// calls another method within the same (this) assembly
        /// </summary>
        public void DoDependency()
        {
            Console.WriteLine("RExec.Client.InternalInstructions.Simple.DoDependency() is now being executed");
            new RExec.Client.InternalInstructions.InternalDependency.Simple().Do();
        }

        /// <summary>
        /// calls external method in another (external) assembly
        /// </summary>
        public void DoReferenceDependency()
        {
            Console.WriteLine("RExec.Client.InternalInstructions.Simple.DoReferenceDependency() is now being executed");
            new Instructions.Reference.Client.ExternalDependency.Simple().Do();
        }
    }
}
