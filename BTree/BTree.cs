using System;
using System.Collections;
using System.Collections.Generic;

namespace luigi.algorithms
{
    public partial class BTree<T> : ICollection<T>
    {

        public BTree()
        {
        }

        public int Count { get; private set; } = 0;

        public bool IsReadOnly { get; private set; } = false;

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
