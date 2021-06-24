namespace luigi.algorithms
{
    public partial class BTree<T>
    {
        class Node
        {
            #region static
            public static int GlobalDimension { get; private set; } = 1;
            public static void SetDimension(int dimension)
            {
                GlobalDimension = dimension > 1 ? dimension : 1;
            }
            #endregion

            public Node(T element, Node parent, int dimension = 0)
            {
                Dimension = dimension > 0 ? dimension : GlobalDimension;
                Elements = new T[Dimension];
                Elements[0] = element;
                Parent = parent;
            }

            public int Dimension { get; private set; }
            public T[] Elements { get; private set; }
            public Node Parent { get; private set; }
            public Node[] Children { get; private set; }
        }
    }
}
