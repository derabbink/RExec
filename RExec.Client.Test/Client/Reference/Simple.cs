using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RExec.ClientProxy;
using RExec.Dispatcher.Contracts.Service;

namespace RExec.Client.Test.Client.Reference
{
    [TestFixture]
    public class Simple : RExecTest
    {
        protected override string assemblyName { get { return "Instructions.Reference.Client"; } }
        protected override string fqTypeName { get { return "Instructions.Reference.Client.Simple"; } }

        [Test]
        public void InvokeMethod([Values(
                new[]{"Do",
                      "Instructions.Reference.Client.Simple.Do()"},
                new[]{"DoDependency",
                      "Instructions.Reference.Client.Simple.DoDependency()->Instructions.Reference.Client.InternalDependency.Simple.Do()"},
                new[]{"DoReferenceDependency",
                      "Instructions.Reference.Client.Simple.DoReferenceDependency()->Instructions.Reference.Client.Dependency.Simple.Do()"})]
            string[] args)
        {
            string actualResult = base.InvokeMethod(args[MethodName]) as string;
            Assert.That(actualResult, Is.EqualTo(args[ExpectedResult]));
        }
    }
}
