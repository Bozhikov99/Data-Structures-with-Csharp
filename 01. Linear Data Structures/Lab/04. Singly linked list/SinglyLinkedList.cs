namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        public class Node
        {
            public Node(T value)
            {
                Value = value;
            }

            public Node Next { get; set; }

            public T Value { get; set; }

            public bool HasNext()
            {
                return Next != null;
            }
        }

        private int count;

        public int Count => count;

        public Node Head { get; set; }

        public Node Tail { get; set; }

        public void AddFirst(T item)
        {
            Node node = new Node(item);

            if (Count == 0)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                node.Next = Head;
                Head = node;
            }

            count++;
        }

        public void AddLast(T item)
        {
            Node node = new Node(item);

            if (Count == 0)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                Node current = Head;

                while (current.HasNext())
                {
                    current = current.Next;
                }

                current.Next = node;
                Tail = node;
            }

            count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = Head;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        public T GetFirst()
        {
            EnsureNotNull();

            T value = Head.Value;

            return value;
        }

        public T GetLast()
        {
            EnsureNotNull();

            Node current = Tail;
            T value = current.Value;

            return value;
        }

        public T RemoveFirst()
        {
            EnsureNotNull();

            T value = Head.Value;
            Head = Head.Next;
            count--;

            return value;
        }

        public T RemoveLast()
        {
            EnsureNotNull();

            Node current = Head;

            if (!current.HasNext())
            {
                T headValue = Head.Value;

                Head = null;
                count--;

                return headValue;
            }

            while (current.Next != Tail)
            {
                current = current.Next;
            }

            T value = Tail.Value;
            Tail = current;
            Tail.Next = null;
            count--;

            return value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void EnsureNotNull()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}