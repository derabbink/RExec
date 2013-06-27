using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test
{
    public class ClientReferenceAdvanced : RExecTest
    {
        protected override string assemblyName { get { return "Instructions.Reference.Client"; } }
        protected override string assemblyPath { get { return "..\\..\\..\\Instructions.Reference.Client\\bin\\Debug\\Instructions.Reference.Client.dll"; } }
        protected override string fqTypeName { get { return "Instructions.Reference.Client.RuntimeInfoGenerator"; } }

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
