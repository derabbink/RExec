using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Client.Reference
{
    [TestFixture]
    public class Advanced : RExecTest
    {
        protected override string assemblyName { get { return "Instructions.Reference.Client"; } }
        protected override string fqTypeName { get { return "Instructions.Reference.Client.RuntimeInfoGenerator"; } }

        [Test]
        public void InvokeMethod([Values(
                new[]{"PrintAssemblyNameAndFQTypeNameAndActionName",
                      "Instructions.Reference.Client.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(Instructions.Reference.Client,Instructions.Reference.Client.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"},
                new[]{"PrintAssemblyNameAndFQTypeNameAndActionNameDependency",
                      "Instructions.Reference.Client.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionNameDependency()->Instructions.Reference.Client.InternalDependency.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(Instructions.Reference.Client,Instructions.Reference.Client.InternalDependency.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"},
                new[]{"PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency",
                      "Instructions.Reference.Client.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency()->Instructions.Reference.Client.Dependency.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(Instructions.Reference.Client.Dependency,Instructions.Reference.Client.Dependency.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"})]
            string[] args)
        {
            string actualResult = base.InvokeMethod(args[MethodName]) as string;
            Assert.That(actualResult, Is.EqualTo(args[ExpectedResult]));
        }
    }
}
