using System.Text;

namespace Lab4
{
    internal class Program
    {
        const string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        static void Main(string[] args)
        {
            MyHashTable<int> hashmap = new MyHashTable<int>();

            for (int i = 0; i < 10000; i++)
            {
                string key = GenerateKey();
                if (!hashmap.Contains(key))
                    hashmap.Add(key, i);
            }

            foreach (var item in hashmap)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(hashmap.MaxCount);
            Console.WriteLine(hashmap.MinCount);
            Console.WriteLine(hashmap.Factor);
            Console.WriteLine(hashmap.AllCount);

            IFileGenerator<int> fileGenerator = new ExcelGenerator<int>();
            byte[] data = hashmap.ExportToFile(fileGenerator);
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HashMap.xlsx");
            File.WriteAllBytes(path, data);
        }

        public static string GenerateKey()
        {
            Random rnd = new Random();
            StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append(alpha[rnd.Next(alpha.Length)]);
            keyBuilder.Append(alpha[rnd.Next(alpha.Length)]);
            keyBuilder.Append(rnd.Next(10));
            keyBuilder.Append(rnd.Next(10));
            keyBuilder.Append(alpha[rnd.Next(alpha.Length)]);
            keyBuilder.Append(alpha[rnd.Next(alpha.Length)]);
            return keyBuilder.ToString();
        }
    }
}