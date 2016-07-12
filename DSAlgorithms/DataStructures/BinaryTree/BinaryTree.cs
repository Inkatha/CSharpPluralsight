using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private BinaryTreeNode<T> _head;
        private int _count;

        #region Add

        /// <summary>
        /// Adds the provided value to the binary tree.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void Add(T value)
        {
            // Case 1: The tree is empty - allocate the head
            if (_head == null)
            {
                _head = new BinaryTreeNode<T>(value);
            }
            // Case 2 : The tree is not empty so find the right location to insert
            else
            {
                AddTo(_head, value);
            }

        }

        // Recursive Add Algorithm
        private void AddTo(BinaryTreeNode<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<T>(value);
                }
                else
                {
                    AddTo(node.Left, value);
                }
            }
            else if (value.CompareTo(node.Value) >= 0)
            {
                if (node.Right == null)
                {
                    node.Right = new BinaryTreeNode<T>(value);
                }
                else
                {
                    AddTo(node.Right, value);
                }
            }
        }
        #endregion

        /// <summary>
        /// Determines if the specified value exists in the binary tree.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>True if the tree contains the value, false otherwise</returns>
        public bool Contains(T value)
        {
            // defer to the node search helper function
            BinaryTreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }

        /// <summary>
        /// Finds and returns the first node containing the specified value, 
        /// if the value is not found, returns null. Also returns the parent of the found node (or null)
        /// which is used in remove.
        /// </summary>
        /// <param name="value">The value to search for</param>
        /// <param name="parent">The parent of the found node (or null)</param>
        /// <returns></returns>
        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        {
            // Now, try to find data in the tree
            BinaryTreeNode<T> current = _head;
            parent = null;

            while (current != null)
            {
                int result = current.CompareTo(value);
                if (result > 0)
                {
                    // if the value is less than current, go left.
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // if the value is greater than current, go right.
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    // We have a match!
                    break;
                }
            }
            return current;
        }

        #region Remove
        /// <summary>
        /// Removes the first occurance of the specified value from the tree.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>True if the value was removed, false otherwise.</returns>
        public bool Remove(T value)
        {
            BinaryTreeNode<T> current;
            BinaryTreeNode<T> parent;

            current = FindWithParent(value, out parent);

            if (current == null)
            {
                return false;
            }

            _count--;

            // Case 1: If the current has no right child, then current's left replaces current
            if (current.Right == null)
            {
                _head = current.Left;
            }
            else
            {
                int result = parent.CompareTo(current.Value);
                if (result > 0)
                {
                    // if parent value is greater than current value
                    // make the current left child a left child of parent
                    parent.Left = current.Left;
                }
                else if (result < 0)
                {

                }
            }
        }

        #endregion
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
