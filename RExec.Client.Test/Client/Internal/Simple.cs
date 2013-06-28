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

namespace RExec.Client.Test.Client.Internal
{
    [TestFixture]
    public class Simple : RExecTest
    {
        protected override string assemblyName { get { return "RExec.Client"; } }
        protected override string fqTypeName { get { return "RExec.Client.InternalInstructions.Simple"; } }

        [Test]
        public void InvokeMethod([Values(
                new[]{"Do",
                      "RExec.Client.InternalInstructions.Simple.Do()"},
                new[]{"DoDependency",
                      "RExec.Client.InternalInstructions.Simple.DoDependency()->RExec.Client.InternalInstructions.InternalDependency.Simple.Do()"},
                new[]{"DoReferenceDependency",
                      "RExec.Client.InternalInstructions.Simple.DoReferenceDependency()->Instructions.Reference.Client.ExternalDependency.Simple.Do()"})]
            string[] args)
        {
            string actualResult = base.InvokeMethod(args[MethodName]) as string;
            Assert.That(actualResult, Is.EqualTo(args[ExpectedResult]));
        }
    }
}
