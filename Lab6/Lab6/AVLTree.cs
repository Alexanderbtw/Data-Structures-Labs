using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab6
{
    public class AVLTree<T> : IEnumerable<T> where T : IComparable
    {
        public AVLTreeNode<T>? Head { get; internal set; }

        public int Count { get; private set; }

        public void Add(T value)
        {
            if (Head == null)
            {
                Head = new AVLTreeNode<T>(value, null, this);
            }
            else
            {
                AddTo(Head, value);
            }

            Count++;
        }

        private void AddTo(AVLTreeNode<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new AVLTreeNode<T>(value, node, this);
                }
                else
                {
                    AddTo(node.Left, value);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new AVLTreeNode<T> (value, node, this);
                }
                else
                {
                    AddTo(node.Right, value);
                }
            }
            node.Balance();
        }

        public bool Contains(T value)
        {
            return Find(value, out _) != null;
        }

        public AVLTreeNode<T>? Find(T value, out int steps)
        {
            steps = 0;
            AVLTreeNode<T>? current = Head;

            while (current != null)
            {
                int result = current.CompareTo(value);
                steps++;
                if (result > 0)
                {
                    current = current.Left;
                }
                else if (result < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            return current;
        }

        public bool Remove(T value)
        {
            AVLTreeNode<T>? current;
            current = Find(value, out _);

            if (current == null)
            {
                return false;
            }

            AVLTreeNode<T>? treeToBalance = current.Parent;
            Count--;

            if (current.Right == null)
            {
                if (current.Parent == null)
                {
                    Head = current.Left;

                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(value);

                    if (result > 0)
                    {
                        current.Parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        current.Parent.Right = current.Right;
                    }
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (current.Parent == null)
                {
                    Head = current.Right;

                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        current.Parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        current.Parent.Right = current.Right;
                    }
                }
            }
            else
            {
                AVLTreeNode<T> left_last = current.Right.Left;
                while (left_last.Left != null)
                {
                    left_last = left_last.Left;
                }
                left_last.Parent!.Left = left_last.Right;
                left_last.Left = current.Left;
                left_last.Right = current.Right;

                if (current.Parent == null)
                {
                    Head = left_last;

                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        current.Parent.Left = left_last;
                    }
                    else if (result < 0)
                    {
                        current.Parent.Right = left_last;
                    }
                }
            }
            if (treeToBalance != null)
            {
                treeToBalance.Balance();
            }
            else
            {
                if (Head != null)
                {
                    Head.Balance();
                }
            }

            return true;
        }

        public void LevelOut()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Head = null;
            Count = 0;
        }

        #region ToString
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (Head != null)
            {
                ToString(Head, ref stringBuilder);
            }
            return stringBuilder.ToString();
        }

        private void ToString(AVLTreeNode<T>? node, ref StringBuilder sb)
        {
            if (node == null) return;

            ToString(node.Left, ref sb);
            sb.Append(node.Value.ToString() + " ");
            ToString(node.Right, ref sb);
        }
        #endregion

        #region Display
        public void Display()
        {
            if (Head != null)
            {
                Display(Head);

                #region Non-Recursive Display
                //var stack = new Stack<AVLTreeNode<T>>();
                //stack.Push(Head);
                //int count;

                //while ((count = stack.Count) > 0)
                //{
                //    for (int i = 0; i < count; i++)
                //    {
                //        var curr = stack.Pop();
                //        Console.Write(string.Join("", Enumerable.Repeat(' ', Count - count)) + curr.Value.ToString());

                //        if (curr.Right != null)
                //            stack.Push(curr.Right);
                //        if (curr.Left != null)
                //            stack.Push(curr.Left);
                //    }
                //    Console.WriteLine();
                //}
                #endregion
            }
        }

        private void Display(AVLTreeNode<T>? root, int level = 0)
        {
            if (root == null) return;

            Display(root.Right, level + 1);

            if (level != 0)
            {
                for (int i = 0; i < level - 1; i++)
                    Console.Write("|\t");

                Console.WriteLine("|-------" + root.Value);
            }
            else
            {
                Console.WriteLine(root.Value);
            }

            Display(root.Left, level + 1);
        }
        #endregion

        #region Iterators

        public IEnumerator<T> InOrderTraversal()
        {
            if (Head != null)
            {
                var stack = new Stack<AVLTreeNode<T>>();
                var curr = Head;

                while (curr != null || stack.Count > 0)
                {
                    if (curr != null)
                    {
                        stack.Push(curr);
                        curr = curr.Left;
                    }
                    else
                    {
                        curr = stack.Pop();
                        yield return curr.Value;
                        curr = curr.Right;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
