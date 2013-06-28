using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Host.Internal
{
    [SetUpFixture]
    public class AssemblySetupTearDown : RExec.Client.Test.AssemblySetupTearDown
    {
        protected override string assemblyPath { get { return "..\\..\\..\\RExec.Host\\bin\\Debug\\RExec.Host.exe"; } }

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
