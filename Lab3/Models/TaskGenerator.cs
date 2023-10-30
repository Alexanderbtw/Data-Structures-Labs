using Lab3.Enums;

namespace Lab3.Models
{
    // Класс, описывающий функциональность наследников
    public abstract class TaskGenerator
    {
        protected WorkTask[]? tasks;
        protected uint generatedCount = 0;
        protected Array taskTypes = Enum.GetValues(typeof(TaskType));

        public abstract WorkTask[] Generate();
    }

    // Класс, реализующий функциональность абстрактного в виде рандомной генерации задач
    public class RandomTaskGenerator : TaskGenerator
    {
        private readonly Random rnd = new Random();

        // Генерация и возврат случайного списка задач
        public override WorkTask[] Generate()
        {
            int size = rnd.Next(1, 3);
            tasks = new WorkTask[size];
            for (int i = 0; i < size; i++)
            {
                tasks[i] = new WorkTask(
                    title: $"ITask{++generatedCount}", 
                    duration: (uint)rnd.Next(1, 5), 
                    _type: (TaskType)taskTypes.GetValue(rnd.Next(taskTypes.Length))!);
            }

            return tasks;
        }
    }

    // Класс, реализующий функциональность абстрактного в виде ручной генерации задач
    public class HandleTaskGenerator : TaskGenerator
    {
        // Предоставление пользователю возможности генерировать задачи самому
        public override WorkTask[] Generate()
        {
            Console.Write("Enter amount of tasks: ");
            int size = int.Parse(Console.ReadLine() ?? "1");
            tasks = new WorkTask[size];
            for (int i = 0; i < size; i++)
            {
                Console.Write("Enter task's title: ");
                string title = Console.ReadLine() ?? $"ITask{++generatedCount}";

                Console.Write("Enter task's duration: ");
                uint duration = uint.Parse(Console.ReadLine() ?? "2");

                Console.WriteLine("Select a type of task");
                int count = 0;
                foreach (var name in Enum.GetNames(typeof(TaskType)))
                {
                    Console.WriteLine($"{count++} - {name}");
                }
                TaskType type = (TaskType)taskTypes.GetValue(int.Parse(Console.ReadLine() ?? "0"))!;

                tasks[i] = new WorkTask(title, duration, type);
            }

            return tasks;
        }
    }
}
