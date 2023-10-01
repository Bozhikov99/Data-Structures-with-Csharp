namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T element, IAbstractBinaryTree<T> left, IAbstractBinaryTree<T> right)
        {
            Value = element;
            LeftChild = left;
            RightChild = right;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            StringBuilder output = new StringBuilder();

            output.Append(' ', indent++ * 2)
                .AppendLine($"{Value}");

            if (LeftChild != null)
            {
                output.Append(LeftChild.AsIndentedPreOrder(indent));
            }

            if (RightChild != null)
            {
                output.Append(RightChild.AsIndentedPreOrder(indent));
            }

            return output.ToString();
        }

        public void ForEachInOrder(Action<T> action)
        {
            if (LeftChild != null)
            {
                LeftChild.ForEachInOrder(action);
            }

            action(Value);

            if (RightChild != null)
            {
                RightChild.ForEachInOrder(action);
            }
        }

        public IEnumerable<IAbstractBinaryTree<T>> InOrder()
        {
            List<IAbstractBinaryTree<T>> result = new List<IAbstractBinaryTree<T>>();

            if (LeftChild != null)
            {
                result.AddRange(LeftChild.InOrder());
            }

            result.Add(this);

            if (RightChild != null)
            {
                result.AddRange(RightChild.InOrder());
            }

            return result;
        }

        public IEnumerable<IAbstractBinaryTree<T>> PostOrder()
        {
            List<IAbstractBinaryTree<T>> result = new List<IAbstractBinaryTree<T>>();

            if (LeftChild != null)
            {
                result.AddRange(LeftChild.PostOrder());
            }

            if (RightChild != null)
            {
                result.AddRange(RightChild.PostOrder());
            }

            result.Add(this);

            return result;
        }

        public IEnumerable<IAbstractBinaryTree<T>> PreOrder()
        {
            List<IAbstractBinaryTree<T>> result = new List<IAbstractBinaryTree<T>>();

            result.Add(this);

            if (LeftChild != null)
            {
                result.AddRange(LeftChild.PreOrder());
            }

            if (RightChild != null)
            {
                result.AddRange(RightChild.PreOrder());
            }

            return result;
        }
    }
}
