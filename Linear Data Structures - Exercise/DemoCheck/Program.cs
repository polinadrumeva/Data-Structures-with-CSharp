using Problem03.ReversedList;

namespace DemoCheck
{
	public class Program
	{
		static void Main(string[] args)
		{
			var list = new ReversedList<int>();
			list.Add(1);
			list.Add(2);
			list.Add(3);
			list.Add(4);

            Console.WriteLine(string.Join(", ", list));

			list.Insert(2, 100);

			Console.WriteLine(string.Join(", ", list));
		}
	}
}