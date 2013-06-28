using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Host.Internal
{
    [TestFixture]
    public class Advanced : RExecTest
    {
        protected override string assemblyName { get { return "RExec.Host"; } }
        protected override string fqTypeName { get { return "RExec.Host.InternalInstructions.RuntimeInfoGenerator"; } }

        [Test]
        public void InvokeMethod([Values(
                new[]{"PrintAssemblyNameAndFQTypeNameAndActionName",
                      "RExec.Host.InternalInstructions.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(RExec.Host,RExec.Host.InternalInstructions.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"},
                new[]{"PrintAssemblyNameAndFQTypeNameAndActionNameDependency",
                      "RExec.Host.InternalInstructions.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionNameDependency()->RExec.Host.InternalInstructions.InternalDependency.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(RExec.Host,RExec.Host.InternalInstructions.InternalDependency.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"},
                new[]{"PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency",
                      "RExec.Host.InternalInstructions.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency()->Instructions.Reference.Host.ExternalDependency.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(Instructions.Reference.Host,Instructions.Reference.Host.ExternalDependency.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"})]
            string[] args)
        {
            string actualResult = base.InvokeMethod(args[MethodName]) as string;
            Assert.That(actualResult, Is.EqualTo(args[ExpectedResult]));
        }
    }
}
