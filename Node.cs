using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_Sophie_WF
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }

        public Node()
        {
            Next = null;
            Prev = null;
        }

        public Node(T value)
        {
            Value = value;
            Next = null;
            Prev = null;
        }
        public Node(T value, Node<T> next, Node<T> prev)
        {
            Value = value;
            Next = next;
            Prev = prev;
        }
    }
}
