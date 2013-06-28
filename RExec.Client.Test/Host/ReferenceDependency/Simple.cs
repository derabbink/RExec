using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Host.ReferenceDependency
{
    [TestFixture]
    public class Simple : RExecTest
    {
        protected override string assemblyName { get { return "Instructions.Reference.Host.Dependency"; } }
        protected override string fqTypeName { get { return "Instructions.Reference.Host.Dependency.Simple"; } }

        [Test]
        public void InvokeMethod()
//cannot do [Values()] with one arg
//            [Values(
//                new[]{"Do",
//                      "Instructions.Reference.Host.Dependency.Simple.Do()"})]
//            string[] args)
        {
            string[] args = new[]{"Do",
                                  "Instructions.Reference.Host.Dependency.Simple.Do()"};
            string actualResult = base.InvokeMethod(args[MethodName]) as string;
            Assert.That(actualResult, Is.EqualTo(args[ExpectedResult]));
        }
    }
}
