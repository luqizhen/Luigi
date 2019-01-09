using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovChain
{
    class Program
    {
        static void Main(string[] args)
        {
        }


    }

    public class MarkovChain
    {
        internal Node AddNode(object body)
        {
            if(Root == null)
            {
                Root = new Node(body);
                return Root;
            }

            Node newNode = new Node(body);
            Root.AddNeighbour(newNode);
            newNode.AddNeighbour(Root);

            return newNode;
        }

        private Node _root = null;
        internal Node Root { get => _root; set => _root = value; }
    }

    internal class Node
    {
        private static Random random = new Random(Environment.TickCount);

        private object _body = null;
        internal object Body { get => _body; set => _body = value; }

        private Dictionary<Node, double> _rateTo = new Dictionary<Node, double>();
        internal Dictionary<Node, double> RateTo { get => _rateTo; set => _rateTo = value; }

        internal Node(object body = null)
        {
            Body = body;
            RateTo.Add(this, 1);
        }

        internal void AddNeighboors(IEnumerable<Node> nodes)
        {
            foreach (var node in nodes)
            {
                RateTo.Add(node, RateTo[this]);
            }
            NormalizeRates();
        }

        internal void AddNeighbour(Node node)
        {
            RateTo.Add(node, RateTo[this]);
            NormalizeRates();
        }

        internal Node GoToNext()
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
            if(sum <= 0)
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
