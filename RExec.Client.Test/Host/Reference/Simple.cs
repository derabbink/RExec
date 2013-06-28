using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Host.Reference
{
    [TestFixture]
    public class Simple : RExecTest
    {
        protected override string assemblyName { get { return "Instructions.Reference.Host"; } }
        protected override string fqTypeName { get { return "Instructions.Reference.Host.Simple"; } }

        [Test]
        public void InvokeMethod([Values(
                new[]{"Do",
                      "Instructions.Reference.Host.Simple.Do()"},
                new[]{"DoDependency",
                      "Instructions.Reference.Host.Simple.DoDependency()->Instructions.Reference.Host.InternalDependency.Simple.Do()"},
                new[]{"DoReferenceDependency",
                      "Instructions.Reference.Host.Simple.DoReferenceDependency()->Instructions.Reference.Host.Dependency.Simple.Do()"})]
            string[] args)
        {
            string actualResult = base.InvokeMethod(args[MethodName]) as string;
            Assert.That(actualResult, Is.EqualTo(args[ExpectedResult]));
        }
    }
}
