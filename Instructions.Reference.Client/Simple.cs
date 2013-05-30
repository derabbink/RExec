using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instructions.Reference.Client
{
    public class Simple
    {
        public void Do()
        {
            Console.WriteLine("Instructions.Reference.Client.Simple.Do() is now being executed");
        }

        /// <summary>
        /// calls another method within the same (this) assembly
        /// </summary>
        public void DoDependency()
        {
            Console.WriteLine("Instructions.Reference.Client.Simple.DoDependency() is now being executed");
            new Instructions.Reference.Client.InternalDependency.Simple().Do();
        }

        /// <summary>
        /// calls external method in another (external) assembly
        /// </summary>
        public void DoReferenceDependency()
        {
            Console.WriteLine("Instructions.Reference.Client.Simple.DoReferenceDependency() is now being executed");
            new Instructions.Reference.Client.Dependency.Simple().Do();
        }
    }
}
