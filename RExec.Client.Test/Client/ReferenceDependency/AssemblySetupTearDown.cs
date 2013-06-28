using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RExec.Client.Test.Client.ReferenceDependency
{
    [SetUpFixture]
    public class AssemblySetupTearDown : RExec.Client.Test.AssemblySetupTearDown
    {
        protected override string assemblyPath { get { return "..\\..\\..\\Instructions.Reference.Client.Dependency\\bin\\Debug\\Instructions.Reference.Client.Dependency.dll"; } }

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
