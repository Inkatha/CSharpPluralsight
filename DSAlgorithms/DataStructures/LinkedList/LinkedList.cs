using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList
{
    public class LinkedList<T> : System.Collections.Generic.ICollection<T>
    {
        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public LinkedListNode<T> Head { get; private set; }
        public LinkedListNode<T> Tail { get; private set; }


        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}