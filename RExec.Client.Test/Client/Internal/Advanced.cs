using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RExec.Dispatcher.Contracts.Data;

namespace RExec.Client.Test.Client.Internal
{
    [TestFixture]
    public class Advanced : RExecTest
    {
        protected override string assemblyName { get { return "RExec.Client"; } }
        protected override string fqTypeName { get { return "RExec.Client.InternalInstructions.RuntimeInfoGenerator"; } }

        [Test]
        public void InvokeMethod([Values(
                new[]{"PrintAssemblyNameAndFQTypeNameAndActionName",
                      "RExec.Client.InternalInstructions.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(RExec.Client,RExec.Client.InternalInstructions.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"},
                new[]{"PrintAssemblyNameAndFQTypeNameAndActionNameDependency",
                      "RExec.Client.InternalInstructions.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionNameDependency()->RExec.Client.InternalInstructions.InternalDependency.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(RExec.Client,RExec.Client.InternalInstructions.InternalDependency.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"},
                new[]{"PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency",
                      "RExec.Client.InternalInstructions.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency()->Instructions.Reference.Client.ExternalDependency.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(Instructions.Reference.Client,Instructions.Reference.Client.ExternalDependency.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"})]
            string[] args)
        {
            string actualResult = base.InvokeMethod(args[MethodName]) as string;
            Assert.That(actualResult, Is.EqualTo(args[ExpectedResult]));
        }
    }
}
