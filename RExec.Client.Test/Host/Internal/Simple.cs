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
        protected override string assemblyPath { get { return "..\\..\\..\\RExec.Host\\bin\\Debug\\RExec.Host.exe"; } }
        protected override string fqTypeName { get { return "RExec.Host.InternalInstructions.Simple"; } }

        [Test]
        public new void InvokeMethod([Values(
                                         "Do",
                                         "DoDependency",
                                         "DoReferenceDependency")] string
                                         methodName)
        {
            base.InvokeMethod(methodName);
            Assert.Pass();
        }
    }
}
