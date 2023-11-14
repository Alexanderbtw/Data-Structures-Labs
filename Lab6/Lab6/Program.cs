namespace Lab6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Add(10);
            tree.Add(3);
            tree.Add(2);
            tree.Add(4);
            tree.Add(12);
            tree.Add(15);
            tree.Add(11);
            tree.Add(25);

            //tree.Remove(11);

            foreach (int i in tree)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine(tree.ToString());

            tree.Display();
        }
    }
}