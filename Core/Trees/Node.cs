using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL.Core.Trees
{
    public class Node<T>
    {
        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }

        public T Content { get; }

        public Node(T content)
        {
            Content = content;
        }
    }
}
