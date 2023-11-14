using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class AVLTreeNode<TNode> : IComparable<TNode> where TNode : IComparable
    {
        #region Fields
        private AVLTree<TNode> tree;
        private AVLTreeNode<TNode>? left;
        private AVLTreeNode<TNode>? right;
        #endregion

        #region Constructor
        public AVLTreeNode(TNode val, AVLTreeNode<TNode>? parent, AVLTree<TNode> tree)
        {
            Value = val;
            Parent = parent;
            this.tree = tree;
        }
        #endregion

        #region Properties
        public AVLTreeNode<TNode>? Left
        {
            get
            {
                return left;
            }
            internal set
            {
                left = value;
                if (left != null)
                {
                    left.Parent = this;
                }
            }
        }

        public AVLTreeNode<TNode>? Right
        {
            get
            {
                return right;
            }
            internal set
            {
                right = value;
                if (right != null)
                {
                    right.Parent = this;
                }
            }
        }

        public AVLTreeNode<TNode>? Parent { get; internal set; }

        public TNode Value { get; internal set; }

        public int MaxHeight
        {
            get
            {
                return MaxChildHeight(this);
            }
        }
        #endregion

        #region Private Properties
        enum TreeState
        {
            Balanced,
            LeftHeavy,
            RightHeavy,
        }

        private int LeftHeight
        {
            get { return MaxChildHeight(Left); }
        }

        private int RightHeight
        {
            get { return MaxChildHeight(Right); }
        }

        private TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                    return TreeState.LeftHeavy;

                if (RightHeight - LeftHeight > 1)
                    return TreeState.RightHeavy;

                return TreeState.Balanced;
            }
        }

        private int BalanceFactor
        {
            get { return RightHeight - LeftHeight; }
        }
        #endregion

        #region Methods
        public int CompareTo(TNode? other)
        {
            return Value.CompareTo(other);
        }

        internal void Balance()
        {
            if (State == TreeState.RightHeavy)
            {
                if (Right != null && Right.BalanceFactor < 0)
                {
                    LeftRightRotation();
                }
                else
                {
                    LeftRotation();
                }
            }
            else if (State == TreeState.LeftHeavy)
            {
                if (Left != null && Left.BalanceFactor > 0)
                {
                    RightLeftRotation();
                }
                else
                {
                    RightRotation();
                }
            }
        }

        private int MaxChildHeight(AVLTreeNode<TNode>? node)
        {
            if (node != null)
            {
                return 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
            }

            return 0;
        }
        

        private void LeftRotation()
        {
            AVLTreeNode<TNode>? newRoot = Right;
            ReplaceRoot(newRoot);

            Right = newRoot?.Left;
            newRoot!.Left = this;
        }

        private void RightRotation()
        {
            AVLTreeNode<TNode>? newRoot = Left;
            ReplaceRoot(newRoot);

            Left = newRoot?.Right;
            newRoot!.Right = this;
        }

        private void LeftRightRotation()
        {
            Right?.RightRotation();
            LeftRotation();
        }

        private void RightLeftRotation()
        {
            Left?.LeftRotation();
            RightRotation();
        }

        private void ReplaceRoot(AVLTreeNode<TNode>? newRoot)
        {
            if (this.Parent != null)
            {
                if (this.Parent.Left == this)
                {
                    this.Parent.Left = newRoot;
                }
                else if (this.Parent.Right == this)
                {
                    this.Parent.Right = newRoot;
                }
            }
            else
            {
                tree.Head = newRoot;
            }

            newRoot!.Parent = this.Parent;
            this.Parent = newRoot;
        }
        #endregion
    }
}
