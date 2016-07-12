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
                if (parent == null)
                {
                    _head = current.Left;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // if the parent value is greater than the current value
                        // make the current left child a left child of parent.
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        // If parent value is less than current value
                        // make the current left child a right child of parent.
                        parent.Right = current.Left;
                    }
                }
            }
            // Case 2: If current's right child has no left child, then current's right child replaces current
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (parent == null)
                {
                    _head = current.Right;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // if parent value is greater than current value
                        // make the current right child a left child of parent
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        // if the parent value is less than current value
                        // make the current right child a right child of parent
                        parent.Right = current.Right;
                    }
                }
            }
            // Case 3: If current's right child has a left child, 
            // replace current with current's right child's left most-most child
            else
            {
                // find the right's left-most child
                BinaryTreeNode<T> leftMost = current.Right.Left;
                BinaryTreeNode<T> leftMostParent = current.Right;

                while (leftMost.Left != null)
                {
                    leftMostParent = leftMost;
                    leftMost = leftMost.Left;
                }
                // the parent's left subtree becomes the leftMost's right subtree
                leftMostParent.Left = leftMost.Right;

                // Assign leftMost's left and right to current's left and right children
                leftMost.Left = current.Left;
                leftMost.Right = current.Right;

                if (parent == null)
                {
                    _head = leftMost;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // If parent value si greater than current value
                        // make leftMost the parent's left child
                        parent.Left = leftMost;
                    }
                    else if (result < 0)
                    {
                        // If parent value is less than current vlaue
                        // make leftMost the parent's right child
                        parent.Right = leftMost;
                    }
                }
            }
            return true;
        }
        #endregion

        #region Pre-Order Traversal
        /// <summary>
        /// Performs the provided action on each binary tree value in pre-order traversal order.
        /// </summary>
        /// <param name="action">the action to perform</param>
        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversal(action, _head);
        }

        private void PreOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrderTraversal(action, node.Left);
                PreOrderTraversal(action, node.Right);
            }
        }
        #endregion

        #region In-Order Traversal
        /// <summary>
        /// Performs the provided action on each binary tree value in In-Order Traversal order.
        /// </summary>
        /// <param name="action">the action to perform</param>
        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(action, _head);
        }
        private void InOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                InOrderTraversal(action, node.Left);
                action(node.Value);
                InOrderTraversal(action, node.Right);
            }
        }
        #endregion

        #region Post-Order Traversal
        /// <summary>
        /// Performs the provided action on each binary tree value in Post-Order Traversal order.
        /// </summary>
        /// <param name="action">the action to perform</param>
        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversal(action, _head);
        }
        public void PostOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                PostOrderTraversal(action, node.Left);
                PostOrderTraversal(action, node.Right);
                action(node.Value);
            }
        }
        #endregion

        public IEnumerator<T> InOrderTraversal()
        {
            // This is a non-recusive algorithm using a stack to demonstrate removing
            // recusion to make using the yield syntax easier
            if (_head != null)
            {
                // Store the nodes we've skipped in this stack (avoids recursion)
                var stack = new Stack<BinaryTreeNode<T>>();

                BinaryTreeNode<T> current = _head;

                // When removing recursion we need to keep track of whether or not
                // we should be going to the left node or the right nodes next.
                bool goLeftNext = true;

                // Start by pushing head onto the stack
                stack.Push(current);

                while (stack.Count > 0)
                {
                    //Were heading left...
                    if (goLeftNext)
                    {
                        // Push everything but the left-mdoe node to the stack
                        // We'll yeild the left-most after this block
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    // in-order is left->yield->right
                    yield return current.Value;

                    if (current.Right != null)
                    {
                        current = current.Right;

                        // Once we've right right once, we need to start going left again.
                        goLeftNext = true;
                    }
                    else
                    {
                        // If we can't go right then we need to pop off the parent node
                        // so we can process it and then go to ti's right node
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

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
