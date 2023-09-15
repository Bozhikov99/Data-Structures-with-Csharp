namespace Problem01.CircularQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CircularQueue<T> : IAbstractQueue<T>
    {
        private int startIndex = 0;
        private int endIndex = 0;
        private T[] array;

        public CircularQueue(int capacity = 4)
        {
            array = new T[capacity];
        }

        public int Count { get; private set; }

        public T Dequeue()
        {
            EnsureNotEmpty();

            if (Count == array.Length / 2)
            {
                Resize(array.Length / 2);
            }

            T value = array[startIndex];
            array[startIndex] = default;
            startIndex = (startIndex + 1) % array.Length;

            Count--;

            return value;
        }

        public void Enqueue(T item)
        {
            if (array.Length == Count)
            {
                Resize(array.Length * 2);
            }

            array[endIndex] = item;
            endIndex = (endIndex + 1) % array.Length;
            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                int index = (startIndex + i) % array.Length;
                yield return array[index];
            }
        }

        public T Peek()
        {
            EnsureNotEmpty();
            T value = array[startIndex];

            return value;
        }

        public T[] ToArray()
        {
            T[] array = CopyArray(Count);

            return array;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void EnsureNotEmpty()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
        }

        private void Resize(int length)
        {
            array = CopyArray(length);
            startIndex = 0;
            endIndex = Count;
        }

        private T[] CopyArray(int length)
        {
            T[] newArray = new T[length];

            for (int i = 0; i < Count; i++)
            {
                int index = (startIndex + i) % array.Length;
                newArray[i] = array[index];
            }

            return newArray;
        }
    }

}
