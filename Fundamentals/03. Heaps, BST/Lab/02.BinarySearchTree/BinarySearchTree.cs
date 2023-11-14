namespace _02.BinarySearchTree
{
    using System;


    public class BinarySearchTree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public Node(T value)
            {
                Value = value;
            }

            public T Value { get; private set; }

            public Node LeftChild { get; set; }

            public Node RightChild { get; set; }
        }

        private Node root;

        public BinarySearchTree() { }

        private BinarySearchTree(Node node)
        {
            PreOrderCopy(node);
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            Insert(node.Value);
            PreOrderCopy(node.LeftChild);
            PreOrderCopy(node.RightChild);
        }

        public bool Contains(T element)
        {
            return Find(element, root) != null;
        }

        public void EachInOrder(Action<T> action)
        {
            EachInOrder(action, root);
        }

        public void Insert(T element)
        {
            root = Insert(element, root);
        }

        private void EachInOrder(Action<T> action, Node node)
        {
            if (node.LeftChild != null)
            {
                EachInOrder(action, node.LeftChild);
            }

            action(node.Value);

            if (node.RightChild != null)
            {
                EachInOrder(action, node.RightChild);
            }
        }

        private Node Find(T element, Node node)
        {
            while (node != null)
            {
                if (element.CompareTo(node.Value) < 0)
                {
                    node = node.LeftChild;
                }

                else if (element.CompareTo(node.Value) > 0)
                {
                    node = node.RightChild;
                }

                else
                {
                    break;
                }
            }

            return node;
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                node = new Node(element);
            }

            else if (node.Value.CompareTo(element) < 0)
            {
                node.RightChild = Insert(element, node.RightChild);
            }

            else if (node.Value.CompareTo(element) > 0)
            {
                node.LeftChild = Insert(element, node.LeftChild);
            }

            return node;
        }

        public IBinarySearchTree<T> Search(T element)
        {
            IBinarySearchTree<T> result = null;
            Node node = Find(element, root);

            if (node != null)
            {
                result = new BinarySearchTree<T>(node);
            }

            return result;
        }
    }
}
