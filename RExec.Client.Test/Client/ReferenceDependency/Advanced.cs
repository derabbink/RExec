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
        public new void InvokeMethod([Values("PrintAssemblyNameAndFQTypeNameAndActionName")] string methodName)
        {
            base.InvokeMethod(methodName);
            Assert.Pass();
        }
    }
}
