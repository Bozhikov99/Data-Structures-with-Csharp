namespace Problem03.ReversedList
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this.items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return items[Count - index - 1];
            }
            set
            {
                ValidateIndex(index);
                items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (items.Length == Count)
            {
                Grow();
            }

            items[Count++] = item;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (items[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (items[Count - i - 1].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            ValidateIndex(index);

            if (Count == items.Length)
            {
                Grow();
            }

            ShiftRight(Count - index - 1);
            items[Count - index] = item;

            Count++;
        }

        public bool Remove(T item)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (items[i].Equals(item))
                {
                    ShiftLeft(i);
                    Count--;

                    return true;
                }
            }

            return false;
        }

        private void ShiftLeft(int index)
        {
            for (int i = index; i <= Count; i++)
            {
                items[i] = items[i + 1];
            }
        }

        private void ShiftRight(int index)
        {
            for (int i = items.Length - 1; i > index; i--)
            {
                items[i] = items[i - 1];
            }
        }

        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            ShiftLeft(Count - index - 1);
            Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ValidateIndex(int index)
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if (index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void Grow()
        {
            T[] newArray = new T[Count* 2];
            items.CopyTo(newArray, 0);

            items = newArray;
        }
    }
}