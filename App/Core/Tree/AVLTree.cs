using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVL.Core.Trees;

namespace App.Tree
{
    public class AVLTree
    {
        private Node<Content> _rootNode;

        public AVLTree(Node<Content> rootNode)
        {
            _rootNode = rootNode;
        }

        public Node<Content> BinarySearch(Decimal value, out Int32 stepsCount)
        {
            Node<Content> node = _rootNode;
            stepsCount = 0;

            while (node != null && node.Content.Value != value)
            {
                node = (value > node.Content.Value) ? node.Right : node.Left;
                stepsCount++;
            }
            
            return node;
        }

        public Boolean IsExists(Decimal value, out Int32 stepsCount)
        {
            return BinarySearch(value, out stepsCount) != null;
        }

        public bool TryInsert(Decimal value)
        {
            Node<Content> newHeadNode = Insert(_rootNode, value);

            if (newHeadNode == null) return false;

            _rootNode = newHeadNode;
            RootNodeChangedEvent?.Invoke(_rootNode);
            return true;
        }

        public IEnumerable<Decimal> Traversal()
        {
            var values = new List<Decimal>();
            TraversalR(_rootNode, values);
            return values;
        }


        private void TraversalR(Node<Content> node, IList<Decimal> list)
        {
            if (node.Left != null) TraversalR(node.Left, list);
            if (node.Right != null) TraversalR(node.Right, list);

            list.Add(node.Content.Value);
        }


        private BalanceFactor GetBalanceFactor(Node<Content> node)
        {
            int rightHeight = node.Right?.Content.Height ?? 0;
            int leftHeight = node.Left?.Content.Height ?? 0;
            int balanceFactor = rightHeight - leftHeight;

            switch (balanceFactor)
            {
                case 2: return BalanceFactor.RightMoreOnTwo;
                case 1: return BalanceFactor.RightMoreOnOne;
                case 0: return BalanceFactor.Equal;
                case -1: return BalanceFactor.LeftMoreOnOne;
                case -2: return BalanceFactor.LeftMoreOnTwo;
                default: throw new Exception($"BalanceFactor error. balanceFactor = {balanceFactor}");
            }
            
        }

        private void SetHeight(Node<Content> node)
        {
            int rightHeight = node.Right?.Content.Height ?? 0;
            int leftHeight = node.Left?.Content.Height ?? 0;

            node.Content.Height = (leftHeight > rightHeight ? leftHeight : rightHeight) + 1;
        }

        private Node<Content> rotateRight(Node<Content> node)
        {
            var leftNode = node.Left;
            node.Left = leftNode.Right;
            leftNode.Right = node;

            SetHeight(leftNode.Right);
            SetHeight(leftNode);
            return leftNode; // указатель на новый корень поддерева
        }

        private Node<Content> rotateLeft(Node<Content> node)
        {
            var rightNode = node.Right;
            node.Right = rightNode.Left;
            rightNode.Left = node;

            SetHeight(rightNode.Left);
            SetHeight(rightNode);
            return rightNode; // указатель на новый корень поддерева
        }

        private Node<Content> Balance(Node<Content> node)
        {
            SetHeight(node);

            if (GetBalanceFactor(node) == BalanceFactor.RightMoreOnTwo)
            {
                if (GetBalanceFactor(node.Right) == BalanceFactor.LeftMoreOnOne)
                    node.Right = rotateRight(node.Right);
                return rotateLeft(node);
            }
            
            if (GetBalanceFactor(node) == BalanceFactor.LeftMoreOnTwo)
            {
                if (GetBalanceFactor(node.Left) == BalanceFactor.RightMoreOnTwo)
                    node.Left = rotateLeft(node.Left);
                return rotateRight(node);
            }

            return node;
        }

        private Node<Content> Insert(Node<Content> node, Decimal value)
        {
            if (node == null) return new Node<Content>(new Content(value, 1));
            if (node.Content.Value == value) return null;

            if (value > node.Content.Value)
            {
                var newNode = Insert(node.Right, value);
                if (newNode == null) return null;
                node.Right = newNode;
            }

            if (value < node.Content.Value)
            {
                var newNode = Insert(node.Left, value);
                if (newNode == null) return null;
                node.Left = newNode;
            }

            return Balance(node);
        }


        public void FillingTraversal()
        {
            FillingTraversalInternal(_rootNode, Decimal.MinValue, Decimal.MaxValue , 0);
        }

        private void FillingTraversalInternal(Node<Content> node, Decimal leftLimit, Decimal rightLimit, int depth)
        {
            depth++;

            if (node.Left != null || depth < Height)
            {
                if (node.Left == null && depth < Height)
                {
                    Decimal averageValue = leftLimit == Decimal.MinValue ?
                        (node.Content.Value - 1) : (leftLimit / 2 + node.Content.Value / 2);
                    node.Left = new Node<Content>(new Content(averageValue, 1));
                }

                FillingTraversalInternal(node.Left, leftLimit, node.Content.Value, depth);
                SetHeight(node);
            }

            if (node.Right != null || depth < Height)
            {
                if (node.Right == null && depth < Height)
                {
                    Decimal averageValue = rightLimit == Decimal.MaxValue ?
                        (node.Content.Value + 1) : (node.Content.Value / 2 + rightLimit / 2);
                    node.Right = new Node<Content>(new Content(averageValue, 1));
                }

                FillingTraversalInternal(node.Right, node.Content.Value, rightLimit, depth);
                SetHeight(node);
            }
        }

        private Node<Content> RemoveMin(Node<Content> node, out Node<Content> min)
        {
            if (node.Left == null)
            {
                min = node;
                return node.Right;
            }

            node.Left = RemoveMin(node.Left, out min);
            return Balance(node);
        }

        public void Remove(Decimal value)
        {
            _rootNode = Remove(_rootNode, value);
            RootNodeChangedEvent?.Invoke(_rootNode);
        }

        private Node<Content> Remove(Node<Content> node, Decimal value)
        {
            if (node == null) return null;

            if (value > node.Content.Value)
                node.Right = Remove(node.Right, value);
            else if (value < node.Content.Value)
                node.Left = Remove(node.Left, value);
            else // value == node.Content.Value
            {
                if (node.Right == null) return node.Left;

                var right = RemoveMin(node.Right, out var min);
                min.Right = right;
                min.Left = node.Left;
                return Balance(min);
            }

            return Balance(node);
        }

        private int GetHeight(Node<Content> node) // +1
        {
            int leftHeight = (node.Left != null) ? GetHeight(node.Left) + 1 : 0;
            int rightHeight = (node.Right != null) ? GetHeight(node.Right) + 1 : 0;
            return (leftHeight >= rightHeight) ? leftHeight : rightHeight;
        }


        public int Height => _rootNode.Content.Height;
        // GetHeight(_rootNode) + 1;


        public RootNodeChangedEvent RootNodeChangedEvent;

        private enum BalanceFactor
        {
            LeftMoreOnTwo,
            LeftMoreOnOne,
            RightMoreOnTwo,
            RightMoreOnOne,
            Equal
        }

    }

    public delegate void RootNodeChangedEvent(Node<Content> rootNode);
}
