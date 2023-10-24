namespace _03.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T> 
                    where T : IComparable<T>
    {
		private List<T> elements;

		public MaxHeap()
        {
            this.elements = new List<T>();
        }
        public int Size => this.elements.Count; 


		public void Add(T element)
        {
            this.elements.Add(element);
            this.HeapifyUp(this.elements.Count - 1);
        }

		private void HeapifyUp(int index)
		{
			var parentIndex = (index - 1) / 2;
            
            while (index > 0 && this.IsGreater(index, parentIndex))
            {
				this.Swap(index, parentIndex);
				index = parentIndex;
				parentIndex = (index - 1) / 2;
			}
		}

		private void Swap(int index, int parentIndex)
		{
			var temp = this.elements[index];
            this.elements[index] = this.elements[parentIndex];
            this.elements[parentIndex] = temp;
		}

		private bool IsGreater(int index, int parentIndex)
		{
			return this.elements[index].CompareTo(this.elements[parentIndex]) > 0;
		}

		public T ExtractMax()
		{
			if (this.elements.Count == 0)
			{
				throw new InvalidOperationException();
			}

            var maxElement = this.elements[0];
			this.Swap(0, this.elements.Count - 1);
			this.elements.RemoveAt(this.elements.Count - 1);
			this.HeapifyDown(0);

			return maxElement;
		}

		private void HeapifyDown(int index)
		{
			var greaterChildIndex = this.GetGreaterChildIndex(index);

			while (greaterChildIndex < this.elements.Count && greaterChildIndex >= 0 && this.IsGreater(greaterChildIndex, index))
			{
				this.Swap(greaterChildIndex, index);
				index = greaterChildIndex;
				greaterChildIndex = this.GetGreaterChildIndex(index);
			}
		}

		private int GetGreaterChildIndex(int index)
		{
			var firstChildIndex = index * 2 + 1;	
			var secondChildIndex = index * 2 + 2;

			if (secondChildIndex < this.elements.Count)
			{
				if (this.IsGreater(firstChildIndex, secondChildIndex))
				{
					return firstChildIndex;
				}

				return secondChildIndex;
			}
			else if (firstChildIndex < this.elements.Count)
			{
				return firstChildIndex;
			}
			else
			{
				return -1;
			}
		}

		public T Peek()
        {
            if (this.elements.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.elements[0];
        }
    }
}
