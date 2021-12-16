using IsoMetrixTest;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsoMetrixAppUnitTest
{
    [TestFixture]

    public class IsoMetrixLinkedListTest
    {
        IsoMetrixLinkedList<int> _isoMetrixLinkedList;

        [SetUp]
        public void Setup()
        {
            _isoMetrixLinkedList = new IsoMetrixLinkedList<int>();
        }

        [Test]
        public void Add_First()
        {
            _isoMetrixLinkedList.AddFirst(1);
            _isoMetrixLinkedList.AddFirst(3);
            var head = _isoMetrixLinkedList.Head;
            Assert.AreEqual(head.Value, 3);
        }

        [Test]
        public void Add_Last_WhenHasHeadValue()
        {
            _isoMetrixLinkedList.AddFirst(1);
            _isoMetrixLinkedList.AddFirst(3);
            _isoMetrixLinkedList.AddFirst(4);
            _isoMetrixLinkedList.AddLast(5);

            var lastNode = _isoMetrixLinkedList.Head.Next.Next.Next;
            var expected = lastNode.Value;
            Assert.AreEqual(expected, 5);
            Assert.IsNull(lastNode.Next);
        }

        [Test]
        public void Add_Last_WhenNoHeadValue()
        {
            _isoMetrixLinkedList.AddLast(5);

            var lastNode = _isoMetrixLinkedList.Head;
            var expected = lastNode.Value;

            Assert.AreEqual(expected, 5);
            Assert.IsNull(lastNode.Next);
        }

        [Test]
        public void Print_List()
        {
            _isoMetrixLinkedList.AddFirst(1);
            _isoMetrixLinkedList.AddFirst(3);
            _isoMetrixLinkedList.AddFirst(4);

            var node3 = _isoMetrixLinkedList.Head.Next.Next;
            var node2 = _isoMetrixLinkedList.Head.Next;
            var node1 = _isoMetrixLinkedList.Head;

            _isoMetrixLinkedList.PrintList();

            Assert.AreEqual(node3.Value, 1);
            Assert.AreEqual(node2.Value, 3);
            Assert.AreEqual(node1.Value, 4);
        }


        [Test]
        public void Delete_WhenNoHeadValue()
        {
            Node<int> node = new Node<int>();
            _isoMetrixLinkedList.Delete(node);
            var head = _isoMetrixLinkedList.Head;

            Assert.IsNull(head);
        }

        [Test]
        public void Delete_WhenHasHeadValue()
        {
            var node = _isoMetrixLinkedList.AddFirst(4);
            var deletedValue = _isoMetrixLinkedList.Delete(node);
            var head = _isoMetrixLinkedList.Head;

            Assert.IsNull(head);
            Assert.AreEqual(deletedValue, 4);
        }

        [Test]
        public void Delete_WhenMoreThanOneNode()
        {
            _isoMetrixLinkedList.AddFirst(1);
            _isoMetrixLinkedList.AddFirst(3);
            var node = _isoMetrixLinkedList.AddFirst(4);
            _isoMetrixLinkedList.AddFirst(5);

            var deletedValue = _isoMetrixLinkedList.Delete(node);
            var node3 = _isoMetrixLinkedList.Head.Next.Next;


            Assert.AreEqual(deletedValue, 4);
            Assert.AreEqual(node3.Value, 1);
            Assert.IsNull(node3.Next);
        }

        [Test]
        public void Insert_BeforeLast()
        {
            _isoMetrixLinkedList.AddFirst(1);
            _isoMetrixLinkedList.AddFirst(3);
            _isoMetrixLinkedList.AddFirst(5);

            _isoMetrixLinkedList.Insert(new Node<int> { Value = 7 },2);
            var node2 = _isoMetrixLinkedList.Head.Next.Next;

            Assert.AreEqual(node2.Value, 7);
            Assert.IsNotNull(node2.Next);
        }
        [Test]
        public void Insert_LastPosition()
        {
            _isoMetrixLinkedList.AddFirst(1);
            _isoMetrixLinkedList.AddFirst(3);
            _isoMetrixLinkedList.AddFirst(5);

            _isoMetrixLinkedList.Insert(new Node<int> { Value = 7 }, 3);
            var node3 = _isoMetrixLinkedList.Head.Next.Next.Next;

            Assert.AreEqual(node3.Value, 7);
            Assert.IsNull(node3.Next);
        }
    }
}
