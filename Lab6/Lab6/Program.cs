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
            tree.Add(5);
            tree.Add(12);
            tree.Add(15);
            tree.Add(11);
            tree.Add(25);

            Console.WriteLine($"Дерево содержит 11: {tree.Contains(11)}");
            tree.Remove(11);
            Console.WriteLine($"Дерево содержит 11: {tree.Contains(11)}");

            var node = tree.Find(4, out int steps);
            Console.WriteLine($"\nЭлемент {node!.Value} присутствует в дереве. Был найден за {steps} шагов");

            Console.WriteLine("\nВывод дерева с помощью итератора:");
            foreach (int i in tree)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("\nВывод дерева с помощью ToString:");
            Console.WriteLine(tree.ToString());

            Console.WriteLine("\nВывод дерева с помощью Display:");
            tree.Display();
            Console.WriteLine($"\nКол-во элементов: {tree.Count}");

            tree.LevelOut();

            Console.WriteLine("\nВывод дерева с помощью Display после LevelOut (задания):");
            tree.Display();
            Console.WriteLine($"\nКол-во элементов: {tree.Count}");

            tree.Clear();
            Console.WriteLine($"\nДерево после очистки: {tree}");
        }
    }
}