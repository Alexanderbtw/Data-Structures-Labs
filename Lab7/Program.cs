namespace Lab7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyGraph<string> graph =
            [
                new("A", "B", 1),
                new("A", "C", 2),
                new("A", "D", 3),
                new("D", "B", 4),
                new("E", "B", 5),
                new("F", "E", 6),
                new("C", "E", 3)
            ];

            graph.Add(new("E", "F", 8));

            Console.WriteLine((graph.Find("B", "F")?.ToString() ?? "Not") + '\n');

            Console.WriteLine(graph.ToString() + '\n');

            foreach (var item in graph.FindMinEdgesCover())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine('\n');

            Console.WriteLine(graph.FindMinSpanningTree().ToString() + '\n');

            Console.WriteLine(graph.ToString() + '\n');
        }
    }
}
