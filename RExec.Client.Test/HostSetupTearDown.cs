using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Test.ApplicationControl;
using NUnit.Framework;
using RExec.Client.Test.Util;

namespace RExec.Client.Test
{
    [SetUpFixture]
    public class HostSetupTearDown
    {
        private AutomatedApplication _hostProcess;

        [SetUp]
        public void StartHost()
        {
            _hostProcess = HostProcessHelper.Start();
        }

        [TearDown]
        public void StopHost()
        {
            HostProcessHelper.Stop(_hostProcess);
        }
    }
}
