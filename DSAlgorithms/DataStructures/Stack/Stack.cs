using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    public class Stack<T>
    {
        public LinkedList<T> _list { get; set; }

        public void Push(T item)
        {
            _list.AddFirst(item);
        }

        public T Pop()
        {
            if (_list.Count <= 0)
            {
                throw new InvalidOperationException("The stack is emtpy");
            }
            T value = _list.First.Value;
            _list.RemoveFirst();
            return value;
        }

        public T Peek()
        {
            if (_list.Count <= 0)
            {
                throw new InvalidOperationException("The stack is empty");
            }

            return _list.First.Value;
        }

        public int Count {
            get
            {
                return _list.Count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
