using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Plugin.Test
{
    [TestFixture]
    public class DependencyResolverTest
    {
        [Test]
        public void AppDomainCallback()
        {
            RxDFS.GetAllParents(new Node(10)).
                Subscribe(n => Console.WriteLine("    {0}", n.Number));
            Assert.Inconclusive();
        }
    }
}
