using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Test.ApplicationControl;
using NUnit.Framework;
using RExec.Client.Test.Util;
using RExec.ClientProxy;
using RExec.Dispatcher.Contracts.Data;
using RExec.Dispatcher.Contracts.Service;
using DependencyResolver;

namespace RExec.Client.Test
{
    public class ClientInternalSimple : RExecTest
    {
        protected override string assemblyName { get { return "RExec.Client"; } }
        protected override string assemblyPath { get { return "..\\..\\..\\RExec.Client\\bin\\Debug\\RExec.Client.exe"; } }
        protected override string fqTypeName { get { return "RExec.Client.InternalInstructions.Simple"; } }

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
