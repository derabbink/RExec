﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RExec.Host.InternalInstructions.InternalDependency
{
    /// <summary>
    /// Contains instructions intended for invocation from some place within the same (this) assembly
    /// </summary>
    public class RuntimeInfoGenerator
    {
        public void PrintAssemblyNameAndFQTypeNameAndActionName()
        {
            Console.WriteLine("  RExec.Host.InternalInstructions.InternalDependency.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()");
            string assemblyName = typeof(RuntimeInfoGenerator).Assembly.GetName().Name;
            string fqTypename = typeof(RuntimeInfoGenerator).FullName;
            string actionName = MethodBase.GetCurrentMethod().Name;

            Console.WriteLine("    AssemblyName: {0}", assemblyName);
            Console.WriteLine("    FQTypeName: {0}", fqTypename);
            Console.WriteLine("    ActionName: {0}", actionName);
        }
    }
}
