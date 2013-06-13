using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Plugin
{
    public class Node : IEquatable<Node>
    {
        public enum State
        {
            Ready,
            NotReady
        }
        private int _number;
        private State _state;

        public Node(int number)
        {
            _state = State.NotReady;
            _number = number;
        }

        /// <summary>
        /// used for comparing/equality
        /// </summary>
        public int Id { get { return _number; } }
        
        public int Number {
            get
            {
                if (_state != State.Ready)
                    throw new Exception("Node not ready yet. Call Prepare() first.");
                return _number;
            }
        }

        public void Prepare()
        {
            if (_state != State.NotReady)
                throw new Exception("State was already Ready.");
            //simulates slow loading
            Thread.Sleep(500);
            _state = State.Ready;
        }

        public IEnumerable<Node> GetParents()
        {
            int num = this.Number;
            if (num == 1)
                return Enumerable.Empty<Node>();

            int start = num / 2;
            int count = num - start;
            if (count <= 0)
                return Enumerable.Empty<Node>();

            return Enumerable.Range(start, count).Select(i => new Node(i));
        }

        public bool Equals(Node other)
        {
            return Id == other.Id;
        }
    }

    public class NodeComparer : IEqualityComparer<Node>
    {
        public bool Equals(Node x, Node y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Node node)
        {
            return node.Id.GetHashCode();
        }
    }
}
