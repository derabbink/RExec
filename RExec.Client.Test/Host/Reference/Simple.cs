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
