using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Host.Internal
{
    [TestFixture]
    public class Simple : RExecTest
    {
        protected override string assemblyName { get { return "RExec.Host"; } }
        protected override string fqTypeName { get { return "RExec.Host.InternalInstructions.Simple"; } }

        [Test]
        public void InvokeMethod([Values(
                new[]{"Do",
                      "RExec.Host.InternalInstructions.Simple.Do()"},
                new[]{"DoDependency",
                      "RExec.Host.InternalInstructions.Simple.DoDependency()->RExec.Host.InternalInstructions.InternalDependency.Simple.Do()"},
                new[]{"DoReferenceDependency",
                      "RExec.Host.InternalInstructions.Simple.DoReferenceDependency()->Instructions.Reference.Host.ExternalDependency.Simple.Do()"})]
            string[] args)
        {
            string actualResult = base.InvokeMethod(args[MethodName]) as string;
            Assert.That(actualResult, Is.EqualTo(args[ExpectedResult]));
        }
    }
}
