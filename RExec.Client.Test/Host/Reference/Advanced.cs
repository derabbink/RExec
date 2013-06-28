using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Host.Reference
{
    [TestFixture]
    public class Advanced : RExecTest
    {
        protected override string assemblyName { get { return "Instructions.Reference.Host"; } }
        protected override string assemblyPath { get { return "..\\..\\..\\Instructions.Reference.Host\\bin\\Debug\\Instructions.Reference.Host.dll"; } }
        protected override string fqTypeName { get { return "Instructions.Reference.Host.RuntimeInfoGenerator"; } }

        [Test]
        public new void InvokeMethod([Values(
                                         "PrintAssemblyNameAndFQTypeNameAndActionName",
                                         "PrintAssemblyNameAndFQTypeNameAndActionNameDependency",
                                         "PrintAssemblyNameAndFQTypeNameAndActionNameReferenceDependency")] string
                                         methodName)
        {
            base.InvokeMethod(methodName);
            Assert.Pass();
        }
    }
}
