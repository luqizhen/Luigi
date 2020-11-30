using System.Text;
using System.Threading.Tasks;

namespace luigi.dataStructures
{
    public partial class MarkovChain
    {
        public Node AddNode(object body)
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
        public Node Root { get => _root; set => _root = value; }
    }
}
