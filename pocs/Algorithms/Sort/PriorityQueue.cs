using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort.PriorityQueue
{
    public class Element
    {
        public Element(int key)
        {
            Key = key;
        }

        public int Key { get; private set; }
    }

    public class Element<T> : Element
    {
        public Element(int key, T value) : base(key)
        {
            Value = value;
        }

        public T Value { get; private set; }

        public override string ToString()
        {
            return $"<{Key.ToString()}: {Value.ToString()}>";
        }
    }

    public interface IPriorityQueue
    {
        void Insert(Element[] array, Element element);
        Element Maximum(Element[] array);
        Element ExtractMax(Element[] array);
        void IncreaseKey(Element[] array, Element element, int key);
    }

    public class PriorityQueue : IPriorityQueue
    {
        public Element ExtractMax(Element[] array)
        {
            throw new NotImplementedException();
        }

        public void IncreaseKey(Element[] array, Element element, int key)
        {
            throw new NotImplementedException();
        }

        public void Insert(Element[] array, Element element)
        {
            throw new NotImplementedException();
        }

        public Element Maximum(Element[] array)
        {
            throw new NotImplementedException();
        }
    }
}
