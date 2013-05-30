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
        public void Do()
        {
            Console.WriteLine("  Instructions.Reference.Client.ExternalDependency.Simple.Do() is now being executed");
        }
    }
}
