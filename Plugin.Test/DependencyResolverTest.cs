using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace Plugin.Test
{
    [TestFixture]
    public class DependencyResolverTest
    {
        [Test]
        public void MockRxDFS()
        {
            RxDFS.GetAllParents(new Node(10)).
                Subscribe(n => Console.WriteLine("    {0}", n.Number));
            Assert.Inconclusive();
        }

        [Test]
        public void GetAllDependenciesTest()
        {
            Assembly self = Assembly.GetExecutingAssembly();
            AssemblyName name = new AssemblyName("Instructions.Reference.Client");
            //name = self.GetName()
            DependencyResolver.GetAllDependencies(name).
                               Subscribe(an => Console.WriteLine("    {0} ({1})", an.Name, an.CodeBase),
                                         ex => Console.WriteLine(ex.ToString()));
            Assert.Inconclusive();
        }
    }
}
