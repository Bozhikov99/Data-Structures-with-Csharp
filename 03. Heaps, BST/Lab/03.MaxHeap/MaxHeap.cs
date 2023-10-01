namespace _03.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T> where T : IComparable<T>
    {
        private List<T> elements;

        public MaxHeap()
        {
            elements = new List<T>();
        }

        public int Size => elements.Count;

        public void Add(T element)
        {
            elements.Add(element);
            HeapifyUp(Size - 1);
        }
        public T ExtractMax()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException();
            }

            T element = elements[0];
            Swap(0, Size - 1);
            elements.RemoveAt(Size - 1);
            HeapifyDown(0);

            return element;
        }

        private void HeapifyDown(int index)
        {
            int biggerChildIndex = GetBiggerChildIndex(index);

            while (IsValidIndex(biggerChildIndex) && elements[biggerChildIndex].CompareTo(elements[index]) > 0)
            {
                Swap(biggerChildIndex, index);

                index = biggerChildIndex;
                biggerChildIndex = GetBiggerChildIndex(index);
            }
        }

        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < Size;
        }

        private int GetBiggerChildIndex(int index)
        {
            int leftChildIndex = index * 2 + 1;
            int rightChildIndex = index * 2 + 2;

            if (rightChildIndex < Size)
            {
                if (elements[rightChildIndex].CompareTo(elements[leftChildIndex]) > 0)
                {
                    return rightChildIndex;
                }

                return leftChildIndex;
            }

            else if (leftChildIndex < Size)
            {
                return leftChildIndex;
            }

            else
            {
                return -1;
            }
        }

        public T Peek()
        {
            return elements[0];
        }

        private void HeapifyUp(int index)
        {
            int parentIndex = (index - 1) / 2;
            bool isChildBigger = elements[index].CompareTo(elements[parentIndex]) > 0;

            while (index > 0 && isChildBigger)
            {
                Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        private void Swap(int index, int parentIndex)
        {
            var oldParent = elements[parentIndex];
            elements[parentIndex] = elements[index];
            elements[index] = oldParent;
        }
    }
}
