using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Host.ReferenceDependency
{
    [TestFixture]
    public class Advanced : RExecTest
    {
        protected override string assemblyName { get { return "Instructions.Reference.Host.Dependency"; } }
        protected override string assemblyPath { get { return "..\\..\\..\\Instructions.Reference.Host.Dependency\\bin\\Debug\\Instructions.Reference.Host.Dependency.dll"; } }
        protected override string fqTypeName { get { return "Instructions.Reference.Host.Dependency.RuntimeInfoGenerator"; } }

        [Test]
        public new void InvokeMethod([Values("PrintAssemblyNameAndFQTypeNameAndActionName")] string methodName)
        {
            base.InvokeMethod(methodName);
            Assert.Pass();
        }
    }
}
