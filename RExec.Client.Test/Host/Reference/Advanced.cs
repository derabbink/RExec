using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Host.Reference
{
    [TestFixture]
    public class Advanced : RExecTest
    {
        protected override string assemblyName { get { return "Instructions.Reference.Host"; } }
        protected override string fqTypeName { get { return "Instructions.Reference.Host.RuntimeInfoGenerator"; } }

        [Test]
        public void InvokeMethod([Values(
                new[]{"PrintAssemblyNameAndFQTypeNameAndActionName",
                      "Instructions.Reference.Host.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(Instructions.Reference.Host,Instructions.Reference.Host.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"},
                new[]{"PrintAssemblyNameAndFQTypeNameAndActionNameDependency",
                      "Instructions.Reference.Host.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionNameDependency()->Instructions.Reference.Host.InternalDependency.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(Instructions.Reference.Host,Instructions.Reference.Host.InternalDependency.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"},
                new[]{"PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency",
                      "Instructions.Reference.Host.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency()->Instructions.Reference.Host.Dependency.RuntimeInfoGenerator.Instructions.Reference.Host.Dependency.RuntimeInfoGenerator()=(Instructions.Reference.Host.Dependency,Instructions.Reference.Host.Dependency.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"})]
            string[] args)
        {
            string actualResult = base.InvokeMethod(args[MethodName]) as string;
            Assert.That(actualResult, Is.EqualTo(args[ExpectedResult]));
        }
    }
}
