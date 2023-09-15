namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private class Node
        {
            public Node(T value)
            {
                Value = value;
            }

            public Node Next { get; set; }

            public T Value { get; set; }
        }

        private Node head;

        private int count;

        public int Count => count;

        public void Enqueue(T item)
        {
            Node newNode = new Node(item);

            if (count == 0)
            {
                head = newNode;
            }
            else
            {
                Node current = head;

                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = newNode;
            }

            count++;
        }

        public T Dequeue()
        {
            EnsureNotEmpty();

            Node current = head;
            head = head.Next;

            count--;

            return current.Value;
        }

        public T Peek()
        {
            EnsureNotEmpty();
            T value = head.Value;

            return value;
        }

        public bool Contains(T item)
        {
            Node current = head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void EnsureNotEmpty()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}