using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Host.Reference
{
    [SetUpFixture]
    public class AssemblySetupTearDown : RExec.Client.Test.AssemblySetupTearDown
    {
        protected override string assemblyPath { get { return "..\\..\\..\\Instructions.Reference.Host\\bin\\Debug\\Instructions.Reference.Host.dll"; } }

        [SetUp]
        public new void Setup()
        {
            base.Setup();
        }

        [TearDown]
        public new void TearDown()
        {
            base.TearDown();
        }
    }
}
