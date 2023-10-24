namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
            if (leftChild != null)
            {
                this.LeftChild.Parent = this;
            }

            if (rightChild != null)
            {
                this.RightChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            var firstNode = this.FindNodeBFS(first, this);
            var secondNode = this.FindNodeBFS(second, this);

            if (firstNode == null || secondNode == null)
            {
				throw new InvalidOperationException();
			}

            var firstNodeAncestors = this.GetAncestors(firstNode);
			var secondNodeAncestors = this.GetAncestors(secondNode);

			var firstNodeAncestorsSet = new HashSet<T>(firstNodeAncestors);
			var secondNodeAncestorsSet = new HashSet<T>(secondNodeAncestors);

			return firstNodeAncestorsSet
				.Intersect(secondNodeAncestorsSet)
				.FirstOrDefault();

        }

		private Queue<T> GetAncestors(BinaryTree<T> firstNode)
		{
			var ancestors = new Queue<T>();
            var current = firstNode;

            while (current != null) 
            {
                ancestors.Enqueue(current.Value);
                current = current.Parent;
            }

            return ancestors;

		}

		private BinaryTree<T> FindNodeBFS(T first, BinaryTree<T> binaryTree)
		{
			var queue = new Queue<BinaryTree<T>>();

            queue.Enqueue(binaryTree);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (first.Equals(node.Value))
                {
                    return node;
                }

                if (node.LeftChild != null)
                {
					queue.Enqueue(node.LeftChild);
				}

                if (node.RightChild != null)
                {
                    queue.Enqueue(node.RightChild);
                }
            }

            return null;
		}
	}
}
