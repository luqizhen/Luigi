using System;
using System.Collections.Generic;
using System.Linq;

namespace luigi.algorithms
{
    public partial class MarkovChain
    {
        public class Node
        {
            private static Random random = new Random(Environment.TickCount);

            private object _body = null;
            public object Body { get => _body; set => _body = value; }

            private Dictionary<Node, double> _rateTo = new Dictionary<Node, double>();
            public Dictionary<Node, double> RateTo { get => _rateTo; set => _rateTo = value; }

            public Node(object body = null)
            {
                Body = body;
                RateTo.Add(this, 1);
            }

            public void AddNeighboors(IEnumerable<Node> nodes)
            {
                foreach (var node in nodes)
                {
                    RateTo.Add(node, RateTo[this]);
                }
                NormalizeRates();
            }

            public void AddNeighbour(Node node)
            {
                RateTo.Add(node, RateTo[this]);
                NormalizeRates();
            }

            public Node GoToNext()
            {
                Node nextNode = this;

                double rate = random.NextDouble();
                foreach (var node in RateTo.Keys)
                {
                    if ((rate -= RateTo[node]) < 0)
                    {
                        nextNode = node;
                        break;
                    }
                }

                Update(nextNode);
                return nextNode;
            }

            private void Update(Node nextNode, bool needUpdateSelf = true)
            {
                if (!needUpdateSelf && nextNode == this)
                {
                    return;
                }
                RateTo[nextNode] /= 2;
                NormalizeRates();
            }

            private void NormalizeRates()
            {
                double sum = RateTo.Values.Sum();
                if (sum <= 0)
                {
                    Console.WriteLine("Sum of rates should not be 0!");
                    return;
                }
                foreach (var node in RateTo.Keys)
                {
                    RateTo[node] /= sum;
                }
            }
        }
    }
}
