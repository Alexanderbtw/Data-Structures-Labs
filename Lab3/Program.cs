using Lab3.Enums;
using Lab3.Models;

// Создание структур
MyQueue<WorkTask> task_queue = new MyQueue<WorkTask>(10);
MyStack<WorkTask> stack = new MyStack<WorkTask>();

// Создание и инициализация списка процессоров
Processor[] processors = {
    new Processor(TaskType.T0),
    new Processor(TaskType.T1),
    new Processor(TaskType.T2)
};

Console.WriteLine("1 - Generate tasks automatically");
Console.WriteLine("2 - Generate tasks handle");

// Получение той или иной реализации класса, в зависимости от выбора пользователя
TaskGenerator taskGenerator = Console.ReadLine() switch
{
    "1" => new RandomTaskGenerator(),
    _ => new HandleTaskGenerator()
};

// Счётчик тактов (итераций цикла)
int clock_counter = 0;

while (true)
{
    // Заполнение очереди до 10-го такта (что-бы избежать бесконечного цикла при генерации задач случайно)
    if (clock_counter < 10)
    {
        foreach (WorkTask task in taskGenerator.Generate())
        {
            if (task_queue.IsFull)
            {
                break;
            }
            task_queue.Push(task);
        }
    }

    // Берём задачу из очереди или, если первая пуста, из стека, иначе выходим из цикла
    WorkTask curr_task;
    if (!task_queue.IsEmpty)
    {
        curr_task = task_queue.Pop();
    }
    else if (!stack.IsEmpty)
    {
        curr_task = stack.Pop();
    }
    else
    {
        break;
    }

    Console.WriteLine($"Clock: {++clock_counter}");

    bool taskSet = false;

    // Выполнение процессорами задач
    foreach (Processor processor in processors)
    {
        if (!processor.IsFree)
        {
            processor.Execute();
        }
        else if (processor.Type == curr_task.Type)
        {
            processor.SetTask(curr_task);
            taskSet = true;
        }
        else
        {
            Console.WriteLine($"Processor {processor.Type}: Free");
        }
    }
    // Если взятую ранее задачу не удалось установить в процессор, отправляем её в стек
    if (!taskSet)
    {
        stack.Push(curr_task);
    }

    // Вывод содержимого коллекций
    Console.WriteLine("Queue:");
    Console.Write(task_queue.ToString());

    Console.WriteLine("Stack:");
    Console.Write(stack.ToString());

    Console.WriteLine();

    Thread.Sleep(1000);
}

// Заканчиваем выполнение оставшихся в процессорах задач
do
{
    Console.WriteLine($"Clock: {++clock_counter}");

    foreach (Processor processor in processors)
    {
        if (processor.IsFree)
        {
            Console.WriteLine($"Processor {processor.Type}: Free");
        }
        else
        {
            processor.Execute();
        }
    }

    Console.WriteLine();
} while (processors.Any(processor => !processor.IsFree));