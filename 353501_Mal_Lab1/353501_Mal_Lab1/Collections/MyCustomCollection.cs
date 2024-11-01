using System;
using System.Collections;
using _353501_Mal_Lab1.Interfaces;

namespace _353501_Mal_Lab1.Collections
{
    public class MyCustomCollection<T> : ICustomCollection<T>, IEnumerable<T> 
    {
        private class Node
        {
            public T Data { get; set; }
            public Node? Next { get; set; }

            public Node(T data)
            {
                Data = data;
            }
        }

        private Node? begin;
        private Node? end;
        private Node? pointer;
        private int count;

        public MyCustomCollection()
        {
            begin = null;
            end = null;
            pointer = null;
            count = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException();
                }

                Node? current = begin;
                for (int i = 0; i < index; i++)
                {
                    current = current?.Next;
                }

                return current != null ? current.Data : default!;
            }
            set
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException();
                }

                Node? current = begin;
                for (int i = 0; i < index; i++)
                {
                    current = current?.Next;
                }

                if (current != null)
                {
                    current.Data = value;
                }
            }
        }

        public int Count => count;

        public void Add(T item)
        {
            var newNode = new Node(item);
            if (end == null)
            {
                begin = newNode;
                end = newNode;
            }
            else
            {
                end.Next = newNode;
                end = newNode;
            }
            count++;
        }

        public void Remove(T item)
        {
            if (begin == null) throw new InvalidOperationException("Collection is empty.");

            if (begin.Data.Equals(item))
            {
                begin = begin.Next;
                count--;
                return;
            }

            Node? current = begin;
            while (current.Next != null)
            {
                if (current.Next.Data.Equals(item))
                {
                    current.Next = current.Next.Next;
                    if (current.Next == null) end = current;
                    count--;
                    return;
                }
                current = current.Next;
            }
            throw new ArgumentException("Item not found in the collection.");
        }

        public T RemoveCurrent()
        {
            if (pointer == null)
            {
                throw new InvalidOperationException("Pointer is not set.");
            }

            T data = pointer.Data;
            Remove(pointer.Data);
            return data;
        }

        public void Reset()
        {
            pointer = begin;
        }

        public void Next()
        {
            if (pointer != null)
            {
                pointer = pointer.Next;
            }
            else
            {
                throw new InvalidOperationException("Pointer is at the end of the collection.");
            }
        }

        public T Current()
        {
            if (pointer == null)
            {
                throw new InvalidOperationException("Pointer is not set.");
            }
            return pointer.Data;
        }

        public bool MoveNext()
        {
            if (pointer == null)
            {
                pointer = begin;
            }
            else
            {
                pointer = pointer.Next;
            }
            return pointer != null;
        }

        public decimal GetTotalCost(Func<T, decimal> priceSelector)
        {
            decimal totalCost = 0;
            Node? current = begin;
            while (current != null)
            {
                totalCost += priceSelector(current.Data);
                current = current.Next;
            }
            return totalCost;
        }

        public int CountServiceOrders(Func<T, string> serviceSelector, string serviceName)
        {
            int countOrders = 0;
            Node? current = begin;
            while (current != null)
            {
                string service = serviceSelector(current.Data);
                if (service.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                {
                    countOrders++;
                }
                current = current.Next;
            }
            return countOrders;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node? current = begin;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}