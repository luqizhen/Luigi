using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DataStucture.List
{
    public interface INode
    {
        INode Previous { get; set; }
        INode Next { get; set; }
        object Value { get; set; }
        string ToStringAll();
    }

    public class Node : INode
    {
        public Node (object value)
        {
            _previous = null;
            _next = null;
            Value = value;
        }

        private INode _previous;
        private INode _next;

        public INode Previous
        {
            get { return _previous; }
            set
            {
                if (_previous != null && value != null && Object.ReferenceEquals(_previous, value))
                {
                    return;
                }
                if (value == null && _previous == null)
                {
                    return;
                }

                INode temp = null;
                if (_previous != null)
                {
                    temp = _previous.Next;
                }
                _previous = value;
                if (temp != null)
                {
                    temp = null;
                }

                if (value != null)
                {
                    value.Next = this;
                }
            }
        }
        public INode Next
        {
            get { return _next; }
            set
            {
                if (_next != null && value != null && Object.ReferenceEquals(_next, value))
                {
                    return;
                }
                if (value == null && _next == null)
                {
                    return;
                }

                INode temp = null;
                if (_next != null)
                {
                    temp = _next.Previous;
                }
                _next = value;
                if (temp != null)
                {
                    temp = null;
                }

                if (value != null)
                {
                    value.Previous = this;
                }
            }
        }

        public object Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public string ToStringAll()
        {
            string stringAll = "";

            for (INode node = Previous; node != null; node = node.Previous)
            {
                stringAll = node.ToString() + " -> " + stringAll;
            }

            stringAll += ToString();

            for (INode node = Next; node != null; node = node.Next)
            {
                stringAll =stringAll + " -> " + node.ToString();
            }

            return stringAll;
        }
    }

    public interface INode<T> : INode
    {
        new T Value { get; }
    }

    public class Node<T>: Node, INode<T>
    {
        public Node(T value) : base((object)value) { }

        public new T Value => (T)base.Value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public interface IList
    {
        void Add(object value);
        void Delete(int index);
        void Insert(object value, int index);
        object PopHead();
        object PopTail();

        bool Contains(object value);

        int Length { get; }
        object Head { get; set; }
        object Tail { get; set; }
        object this[int index] { get; }
    }

    public class List : IList
    {
        private INode _head;
        private INode _tail;

        public int Length { get; private set; }
        public object Head
        {
            get { return _head?.Value; }
            set { _head.Value = value; }
        }
        public object Tail
        {
            get { return _tail?.Value; }
            set { _tail.Value = value; }
        }

        public List()
        {
            _head = null;
            _tail = null;
            Length = 0;
        }

        public object this[int index]
        {
            get
            {
                return GetNode(index).Value;
            }
        }

        private INode GetNode(int index)
        {
            INode node = _head;
            if (index > 0)
            {
                while (index-- > 0)
                {
                    node = node.Next;
                    if (node == null)
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
            }
            else if (index < 0)
            {
                node = _tail;
                index++;

                while (index++ < 0)
                {
                    node = node.Previous;
                    if (node == null)
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
            }
            return node;
        }

        public void Add(object value)
        {
            if (value == null)
            {
                return;
            }
            Add(new Node(value));
        }

        private void Add(INode node)
        {
            if (node == null)
                return;
            if (node.Previous != null)
                node.Previous = null;
            if (node.Next != null)
                node.Next = null;

            if (node == null)
                return;
            if (node.Previous != null)
                node.Previous = null;
            if (node.Next != null)
                node.Next = null;

            if (_head == null)
            {
                _head = _tail = node;
            }
            else
            {
                _tail.Next = node;
                _tail = _tail.Next;
            }
            Length++;
        }

        public void Delete(int index)
        {
            INode node = GetNode(index);
            Delete(node);
        }

        private void Delete(INode node)
        {
            if (Contains(node))
            {
                if (node.Previous == null)
                {
                    _head = node.Next;
                    _head.Previous = null;
                }
                else if (node.Next == null)
                {
                    _tail = node.Previous;
                    _tail.Next = null;
                }
                else
                {
                    node.Previous = node.Next;
                }
                Length--;
            }
        }

        public bool Contains(object value)
        {
            Type type = value.GetType();
            for (INode iter = _head; iter != null; iter = iter.Next)
            {
                if(iter.Value.GetType() == type)
                {
                    dynamic iterValue = iter.Value;
                    dynamic findValue = value;
                    if (iterValue == findValue)
                        return true;
                }
            }
            return false;
        }

        private bool Contains(INode node)
        {
            for (INode iter = _head; iter != null; iter = iter.Next)
            {
                if (Object.ReferenceEquals(iter, node))
                {
                    return true;
                }
            }
            return false;
        }

        public void Insert(object value, int index)
        {
            Insert(new Node(value), index);
        }

        private void Insert(INode node, int index)
        {
            if (node == null)
                return;
            if (node.Previous != null)
                node.Previous = null;
            if (node.Next != null)
                node.Next = null;

            if (index == 0)
            {
                _head.Previous = node;
                _head = node;
            }
            else if (index == Length)
            {
                _tail.Next = node;
                _tail = node;
            }
            else
            {
                INode previousNode = GetNode(index - 1);
                INode nextNode = GetNode(index);
                previousNode.Next = node;
                nextNode.Previous = node;
            }
            Length++;
        }

        public object PopHead()
        {
            object head = Head;
            Delete(_head);
            return head;
        }

        public object PopTail()
        {
            object tail = Tail;
            Delete(_tail);
            return tail;
        }

        public override string ToString()
        {
            return _head.ToStringAll();
        }
    }
}
