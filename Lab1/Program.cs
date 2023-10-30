namespace Lab1
{
    internal class Program
    {
        static void Change(ref int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < 0)
                {
                    arr[i] = Math.Abs(arr[i]);
                }
            }
        }

        static int OddCount(ref int[] arr)
        {
            // Кол-во нечётных
            int count = 0;
            
            for (int i = 0; i < arr.Length; i++) 
            {
                if (arr[i] % 2 == 1)
                {
                    count++;
                }
            }
            return count;
        }

        static void Main(string[] args)
        {
            // Вывод подсказки пользоваетелю
            Console.WriteLine("Введите последовательность:");

            //Получаем от пользователя последовательность и сразу конвертируем её в массив целых чисел
            int[] arr = Array.ConvertAll(Console.ReadLine()!.Split(), item => int.Parse(item));

            //Вывод исходной последовательности
            Console.WriteLine($"Исходная последовательность: {string.Join(" ", arr) }");

            //Изменяем массив
            Change(ref arr);

            //Получаем кол-во нужных элементов
            int count = OddCount(ref arr);

            Console.WriteLine($"Изменённая последовательность: {string.Join(" ", arr)}");
            Console.WriteLine($"Кол-во элементов с нечётным значением: {count}");
        }
    }
}