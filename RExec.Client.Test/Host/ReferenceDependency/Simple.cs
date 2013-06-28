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
        public new void InvokeMethod([Values("Do")] string methodName)
        {
            base.InvokeMethod(methodName);
            Assert.Pass();
        }
    }
}
