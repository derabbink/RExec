using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RExec.Dispatcher.Contracts.Data;

namespace RExec.Client.Test
{
    public class ClientInternalAdvanced : RExecTest
    {
        protected override string assemblyName { get { return "RExec.Client"; } }
        protected override string assemblyPath { get { return "..\\..\\..\\RExec.Client\\bin\\Debug\\RExec.Client.exe"; } }
        protected override string fqTypeName { get { return "RExec.Client.InternalInstructions.RuntimeInfoGenerator"; } }

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
