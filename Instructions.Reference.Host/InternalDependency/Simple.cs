using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instructions.Reference.Host.InternalDependency
{
    /// <summary>
    /// Contains instructions intended for invocation from some place within the same (this) assembly
    /// </summary>
    public class Simple
    {
        public void Do()
        {
            Console.WriteLine("  Instructions.Reference.Host.InternalDependency.Simple.Do() is now being executed");
        }
    }
}
