using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace Plugin
{
    /// <summary>
    /// test implementation of reactive
    /// depth-first search
    /// </summary>
    public static class RxDFS
    {
        public static IObservable<Node> GetAllParents(Node node)
        {
            Console.WriteLine("creating observable");
            var result = GetAllParentsRecursive(node, new List<Node>());
            Console.WriteLine("creation complete");
            return result;
        }

        /// <summary>
        /// does the recursive descent
        /// </summary>
        /// <param name="node"></param>
        /// <param name="filter">used to accumulate objects we already encountered</param>
        /// <returns></returns>
        private static IObservable<Node> GetAllParentsRecursive(Node node, List<Node> filter)
        {
            node.Prepare();

            IObservable<Node> result = node.GetParents().ToObservable().
                Do(n => Console.WriteLine("  {0} depends on {1}", node.Number, n.Id)).
                Where(n => !filter.Contains(n)).
                //cannot do SelectMany, because we want sequential processing
                Select(n => GetAllParentsRecursive(n, filter)).
                Concat().
                Concat(Observable.Return(node)).
                //Distinct(new NodeComparer()). //not adequate, we need to cut out earlier
                Do(filter.Add);

            return result;
        }
    }
}
