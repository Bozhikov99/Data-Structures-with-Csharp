namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        public class Node
        {
            public Node(T value)
            {
                Value = value;
            }

            public T Value { get; set; }

            public Node Next { get; set; }

            public Node Previous { get; set; }
        }

        public int Count { get; private set; }

        public Node Head { get; private set; }

        public Node Tail { get; private set; }

        public void AddFirst(T item)
        {
            Node newNode = new Node(item);

            if (Count == 0)
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                Node oldHead = Head;
                Head = newNode;
                Head.Next = oldHead;
                oldHead.Previous = newNode;
            }

            Count++;
        }

        public void AddLast(T item)
        {
            Node newNode = new Node(item);

            if (Count == 0)
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                Node oldTail = Tail;
                Tail = newNode;
                Tail.Previous = oldTail;
                oldTail.Next = newNode;
            }

            Count++;
        }

        public T GetFirst()
        {
            EnsureNotEmpty();

            T value = Head.Value;

            return value;
        }

        public T GetLast()
        {
            EnsureNotEmpty();

            T value = Tail.Value;

            return value;
        }

        public T RemoveFirst()
        {
            EnsureNotEmpty();

            Node oldHead = Head;
            Head = Head.Next;

            if (Head != null)
            {
                oldHead.Previous = null;
            }

            T value = oldHead.Value;

            Count--;

            return value;
        }

        public T RemoveLast()
        {
            EnsureNotEmpty();

            Node oldTail = Tail;
            Tail = oldTail.Previous;

            if (Tail != null)
            {
                Tail.Next = null;
            }

            T value = oldTail.Value;

            Count--;

            return value;
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
    }
}