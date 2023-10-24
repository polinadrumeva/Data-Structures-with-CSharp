namespace _02.BinarySearchTree
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class BinarySearchTree<T> : IBinarySearchTree<T> 
                             where T : IComparable
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node Root { get; set; }
        }

        private Node root;

        private BinarySearchTree(Node node)
        {
            this.PreOrderCopy(node);
        }

        public BinarySearchTree()
        {
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
        }

        public bool Contains(T element)
        {
            Node current = this.FindElement(element);

            return current != null;
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        public IBinarySearchTree<T> Search(T element)
        {
            Node current = this.FindElement(element);

            return new BinarySearchTree<T>(current);
        }

        public void Delete(T element)
        {
            if (this.root == null)
            {
				throw new InvalidOperationException();
			}

			this.root = this.Delete(element, this.root);
        }

		private Node Delete(T element, Node root)
		{
            if (root == null)
            {
				return null;
			}

			var compare = element.CompareTo(root.Value);

			if (compare < 0)
            {
				root.Left = this.Delete(element, root.Left);
			}
			else if (compare > 0)
            {
				root.Right = this.Delete(element, root.Right);
			}
			else
            {
				if (root.Left == null)
                {
					return root.Right;
				}
				else if (root.Right == null)
                {
					return root.Left;
				}

				var temp = this.FindMin(root.Right);
				root = temp;
				root.Right = this.Delete(temp.Value, root.Right);
			}

			return root;
		}

		private Node FindMin(Node node)
        {
			while (node.Left != null)
            {
				node = node.Left;
			}

			return node;
		}


		public void DeleteMax()
        {
			if (this.root == null)
			{
				throw new InvalidOperationException();
			}

			this.root = this.DeleteMax(this.root);
		}

		private Node DeleteMax(Node node)
		{
            if (node.Right == null)
            {
                return node.Left;
            }
            
            node.Right = this.DeleteMax(node.Right);
            return node;
		}

		public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMin(this.root);
        }

		private Node DeleteMin(Node node)
		{
            if (node.Left == null)
            {
                return node.Right;
            }

            node.Left = this.DeleteMin(node.Left);
            return node;
		}

		public int Count()
        {
           return this.Count(this.root);
        }

		private int Count(Node node)
		{
            if (node == null)
            {
                return 0;
            }

            return 1 + this.Count(node.Left) + this.Count(node.Right);  
		}

		public int Rank(T element)
        {
			return this.Rank(element, this.root);
		}

		private int Rank(T element, Node node)
		{
            if (node == null)
            {
                return 0;
            }

            if (element.CompareTo(node.Value) < 0)
            {
                return this.Rank(element, node.Left);
            }

            if (element.CompareTo(node.Value) > 0)
            {
				return 1 + this.Count(node.Left) + this.Rank(element, node.Right);
			}

            return this.Count(node.Left);
		}

		public T Select(int rank)
        {
            var node = this.Select(this.root, rank);

            if (node == null)
            {
                throw new InvalidOperationException();
            }

            return node.Value;
        }

		private Node Select(Node node, int rank)
		{
            if (node == null)
            {
                return null;
            }

            var leftCount = this.Count(node.Left);
            if (leftCount == rank)
            {
                return node;
            }

            if (leftCount > rank)
            {
				return this.Select(node.Left, rank);
			}

            return this.Select(node.Right, rank - (leftCount + 1));
		}

		public T Ceiling(T element)
        {
            return this.Select(this.Rank(element) + 1);
        }

        public T Floor(T element)
        {
            return this.Select(this.Rank(element) - 1);
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            var collection = new Queue<T>();
            this.Range(this.root, collection, startRange, endRange);

            return collection;
        }

		private void Range(Node node, Queue<T> collection, T startRange, T endRange)
		{
            if (node == null)
            {
                return;
            }

            var compareLow = startRange.CompareTo(node.Value) < 0;
            var compareHigh = endRange.CompareTo(node.Value) > 0;

            if (compareLow)
            {
				this.Range(node.Left, collection, startRange, endRange);
			}

            if (startRange.CompareTo(node.Value) <= 0 && endRange.CompareTo(node.Value) >= 0)
            {
				collection.Enqueue(node.Value);
			}

			if (compareHigh)
            {
				this.Range(node.Right, collection, startRange, endRange);
			}
		}

		private Node FindElement(T element)
        {
            Node current = this.root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
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

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                node = new Node(element);
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                node.Left = this.Insert(element, node.Left);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = this.Insert(element, node.Right);
            }

            return node;
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }
    }
}
