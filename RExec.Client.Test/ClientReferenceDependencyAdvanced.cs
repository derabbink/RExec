using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test
{
    [TestFixture]
    public class ClientReferenceDependencyAdvanced : RExecTest
    {
        protected override string assemblyName { get { return "Instructions.Reference.Client.Dependency"; } }
        protected override string assemblyPath { get { return "..\\..\\..\\Instructions.Reference.Client.Dependency\\bin\\Debug\\Instructions.Reference.Client.Dependency.dll"; } }
        protected override string fqTypeName { get { return "Instructions.Reference.Client.Dependency.RuntimeInfoGenerator"; } }

        [Test]
        public new void InvokeMethod([Values("PrintAssemblyNameAndFQTypeNameAndActionName")] string methodName)
        {
            base.InvokeMethod(methodName);
            Assert.Pass();
        }
    }
}
