using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instructions.Reference.Host.ExternalDependency
{
    /// <summary>
    /// Contains instructions intended for invocation from some an assembly that THIS assembly is a reference to
    /// concretely: RExec.Host.InternalInstructions.Simple invokes this instruction
    /// </summary>
    public class Simple
    {
        public void Do()
        {
            Console.WriteLine("  Instructions.Reference.Host.ExternalDependency.Simple.Do() is now being executed");
        }
    }
}
