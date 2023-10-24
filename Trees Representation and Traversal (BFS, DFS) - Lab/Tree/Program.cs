namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {

           var tree = new Tree<int>(50);
            tree.AddChild(50, new Tree<int>(2));


        }
    }
}
