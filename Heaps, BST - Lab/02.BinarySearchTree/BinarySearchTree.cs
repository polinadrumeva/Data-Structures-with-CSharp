namespace _02.BinarySearchTree
{
    using System;
	using System.Xml;

	public class BinarySearchTree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
			public Node(T value)
            {
				this.Value = value;
			}

			public T Value { get; private set; }

			public Node Left { get; set; }

			public Node Right { get; set; }
		}

        private Node root;

		public BinarySearchTree()
		{

		}
		private BinarySearchTree(Node node) 
        {
            this.PreOrderCopy(node); 
        }

		private void PreOrderCopy(Node node)
		{
            if (node== null)
            {
                return;
            }

			this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
		}

		public bool Contains(T element)
        {
            return this.FindNode(element) != null;
        }

		private Node FindNode(T element)
		{
			var node = this.root;

            while (node != null)
            {
                if (element.CompareTo(node.Value) < 0)
                {
                    node = node.Left;
                }
                else if (element.CompareTo(node.Value) > 0)
                {
                    node = node.Right;
                }
                else 
                {
                    break;
                }
            }

            return node;
		}

		public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

		private void EachInOrder(Node root, Action<T> action)
		{
            if (root == null)
            {
                return;
            }

            this.EachInOrder(root.Left, action);
            action(root.Value);
            this.EachInOrder(root.Right, action);
		}

		public void Insert(T element)
        {
            this.root = this.Insert(this.root, element);
        }

		private Node Insert(Node root, T element)
		{
			if (root == null)
            {
				root = new Node(element);
				
			}
            else if (element.CompareTo(root.Value) < 0)
            {
                root.Left = this.Insert(root.Left, element);
            }
            else if (element.CompareTo(root.Value) > 0)
            {
				root.Right = this.Insert(root.Right, element);
            }

			return root;
		}

		public IBinarySearchTree<T> Search(T element)
        {
            var node = this.FindNode(element);

            if (node == null)
            {
				return null;
			}

            return new BinarySearchTree<T>(node);
        }
    }
}
