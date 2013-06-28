using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Host.Internal
{
    [TestFixture]
    public class Advanced : RExecTest
    {
        protected override string assemblyName { get { return "RExec.Host"; } }
        protected override string fqTypeName { get { return "RExec.Host.InternalInstructions.RuntimeInfoGenerator"; } }

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
