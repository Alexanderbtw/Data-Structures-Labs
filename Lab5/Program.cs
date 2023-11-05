namespace Lab5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] test = FillArray(10);
            MyOrderedArray<int> array = new MyOrderedArray<int>(test);
            PrintArray(array);

            Console.WriteLine("\nSort results:");
            (int compare_counter, int replace_counter) = array.Sort();
            Console.WriteLine("Number of comparisons: {0}", compare_counter);
            Console.WriteLine("Number of permutation: {0}", replace_counter);
            PrintArray(array);

            Console.WriteLine("\nAdd 11 and 2(2):");
            array.Insert(11, 0);
            array.Append(2);
            array.Append(2);
            PrintArray(array);

            Console.WriteLine("\nSort:");
            array.Sort();
            PrintArray(array);

            Console.WriteLine("\nRemove first 2 and all 2:");
            array.FirstRemove(2);
            PrintArray(array);
            array.RemoveAll(2);
            PrintArray(array);

            Console.WriteLine("\nFind 2 and 11:");
            Console.WriteLine(array.Find(2));
            Console.WriteLine(array.Find(11));

            Console.WriteLine("\nContains 2 and 11:");
            Console.WriteLine(array.Contains(2));
            Console.WriteLine(array.Contains(11));

            Console.WriteLine("\nDublicates:");
            var count = array.FindDublicates(out var dublicates);
            Console.WriteLine(String.Format("Count: {0}", count));
            foreach (var item in dublicates)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }

        static int[] FillArray(int size)
        {
            Random rnd = new Random();
            int[] values = new int[size];
            for (int i = 0; i < size; i++)
            {
                values[i] = rnd.Next(1, size + 1);
            }
            return values;
        }

        static void PrintArray<T>(MyOrderedArray<T> array) where T : IComparable<T>, IEquatable<T>
        {
            foreach (T elem in array)
            {
                Console.Write(String.Format("{0} ", elem));
            }
            Console.WriteLine();
        }
    }
}