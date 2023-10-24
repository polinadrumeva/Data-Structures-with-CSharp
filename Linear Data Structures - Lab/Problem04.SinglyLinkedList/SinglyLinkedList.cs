namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public T Element { get; set; }
            public Node Next { get; set; }

            public Node(T element)
            {
                this.Element = element;
            }
        }

        private Node head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var node = new Node(item);
            var oldHead = this.head;
            this.head = node;
            this.head.Next = oldHead;
            this.Count++;

        }

        public void AddLast(T item)
        {
            if (this.head == null)
            {
                this.head = new Node(item);
                this.Count++;
                return;
            }

            var node = this.head;
            while (node.Next != null)
            {
                node = node.Next;
            }

            node.Next = new Node(item);
            this.Count++;
        }


        public T GetFirst()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.head.Element;
        }

        public T GetLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var node = this.head;

            while (node.Next != null)
            {
                node = node.Next;
            }

            return node.Element;
        }

        public T RemoveFirst()
        {

            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var oldNode = this.head;
            this.head = oldNode.Next;

            this.Count--;
            return oldNode.Element;
        }

        public T RemoveLast()
        {

            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            if (this.Count == 1)
            {
                this.Count--;
                return this.head.Element;
            }

            var node = this.head;

            while (node.Next.Next != null)
            {
                node = node.Next;
            }

            node.Next = null;
            this.Count--;
            return node.Element;
        }
        public IEnumerator<T> GetEnumerator()
        {
            var node = this.head;
            while (node != null)
            {
                yield return node.Element;
                node = node.Next;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}