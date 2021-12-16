using System;
using System.Collections.Generic;
using System.Text;

namespace IsoMetrixTest
{
    public class IsoMetrixLinkedList<T>
    {
        public Node<T> Head { get; private set; }



        public void PrintList()
        {
            Node<T> latestNode = Head;

            while (latestNode != null)
            {
                Console.WriteLine(latestNode.Value);
                latestNode = latestNode.Next;
            }
        }

        public Node<T> AddFirst(T newValue)
        {
            Node<T> newNode = new Node<T>();
            newNode.Value = newValue;
            newNode.Next = Head;

            Head = newNode;
            return newNode;
        }

        public Node<T> AddLast(T newValue)
        {
            if (Head == null)
            {
                var newNode = new Node<T>();
                Head = newNode;
                Head.Value = newValue;
                Head.Next = null;

                return newNode;
            }
            else
            {
                Node<T> newNode = new Node<T>();
                newNode.Value = newValue;

                Node<T> latestNode = Head;

                while (latestNode.Next != null)
                {
                    latestNode = latestNode.Next;
                }

                latestNode.Next = newNode;

                return latestNode.Next;
            }
        }

        public void Insert(Node<T> newNode, int position)
        {
            Node<T> tempNode = Head;
            Node<T> previousNode = null;

            int counter = 0;
            while (tempNode != null && counter < position)
            {
                previousNode = tempNode;
                tempNode = tempNode.Next;
                counter++;
            }

            if (tempNode == null)
            {
                if (counter == position)
                {
                    previousNode.Next = newNode;
                    newNode.Next = null;
                    return;
                }

                return;
            }

            newNode.Next = tempNode;
            previousNode.Next = newNode;
        }

        public T Delete(Node<T> toBeRemovedNode)
        {
            if (Head == null)
            {
                return toBeRemovedNode.Value;
            }

            if (Head == toBeRemovedNode)
            {
                Head = Head.Next;
                toBeRemovedNode.Next = null;

                return toBeRemovedNode.Value;
            }

            var latestNode = Head;

            while (latestNode.Next != null)
            {
                if (latestNode.Next == toBeRemovedNode)
                {
                    latestNode.Next = toBeRemovedNode.Next;
                    return toBeRemovedNode.Value;
                }

                latestNode = latestNode.Next;
            }

            return toBeRemovedNode.Value;
        }
    }

    public class Node<T>
    {
        public Node<T> Next;
        public T Value;
    }
}
