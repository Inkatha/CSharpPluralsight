﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeChains
{
    class Program
    {
        public class Node
        {
            public int Value { get; set; }
            public Node Next { get; set; }
        }
        static void Main(string[] args)
        {
            Node first = new Node { Value = 3 };

            Node middle = new Node { Value = 5 };
            first.Next = middle;

            Node last = new Node { Value = 7 };
            middle.Next = last;

            PrintList(first);
        }

        private static void PrintList(Node node)
        {
            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }
            Console.ReadLine();
        }
    }
}
