using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Client.ReferenceDependency
{
    [TestFixture]
    public class Advanced : RExecTest
    {
        protected override string assemblyName { get { return "Instructions.Reference.Client.Dependency"; } }
        protected override string fqTypeName { get { return "Instructions.Reference.Client.Dependency.RuntimeInfoGenerator"; } }

        [Test]
        public void InvokeMethod()
//cannot do [Values()] with one arg
//            [Values(
//                new[]{"PrintAssemblyNameAndFQTypeNameAndActionName",
//                      "Instructions.Reference.Client.Dependency.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(Instructions.Reference.Client.Dependency,Instructions.Reference.Client.Dependency.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"})]
//            string[] args)
        {
            string[] args = new[]{"PrintAssemblyNameAndFQTypeNameAndActionName",
                                  "Instructions.Reference.Client.Dependency.RuntimeInfoGenerator.PrintAssemblyNameAndFQTypeNameAndActionName()=(Instructions.Reference.Client.Dependency,Instructions.Reference.Client.Dependency.RuntimeInfoGenerator,PrintAssemblyNameAndFQTypeNameAndActionName)"};
            string actualResult = base.InvokeMethod(args[MethodName]) as string;
            Assert.That(actualResult, Is.EqualTo(args[ExpectedResult]));
        }
    }
}
