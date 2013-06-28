using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RExec.ClientProxy;
using RExec.Dispatcher.Contracts.Service;

namespace RExec.Client.Test.Client.Reference
{
    [TestFixture]
    public class Simple : RExecTest
    {
        protected override string assemblyName { get { return "Instructions.Reference.Client"; } }
        protected override string assemblyPath { get { return "..\\..\\..\\Instructions.Reference.Client\\bin\\Debug\\Instructions.Reference.Client.dll"; } }
        protected override string fqTypeName { get { return "Instructions.Reference.Client.Simple"; } }

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
