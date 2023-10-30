using Lab3.Enums;

namespace Lab3.Models
{
    public class Processor
    {
        private IEnumerator<bool>? progress;

        /// <param name="type"></param>
        public Processor(TaskType type)
        {
            // Установка типа задач, который может выполнять процессор
            Type = type;
        }

        public TaskType Type { get; init; }
        private WorkTask? CurrentTask { get; set; } = null;
        public bool IsFree { get { return progress == null; } }

        // Установка задачи
        public void SetTask(WorkTask task)
        {
            if (!IsFree)
                throw new InvalidOperationException("Task already set");

            if (task.Type != this.Type)
                throw new InvalidOperationException("Task type is not suitable");

            progress = this.GetExecutingProgress();
            CurrentTask = task;
            Console.WriteLine($"Processor {Type}: {CurrentTask?.Title} has been set");
        }

        // Получение задачи
        public WorkTask? GetTask()
        {
            return CurrentTask;
        }

        /* Выполнение задачи.
           Реализуется посредством итерирования по длительности выполнения задачи.
           Такой подход был выбран, 
           с целью не нарушать целостность задачи и делегировать всю ответственность за её выполнение процессору */
        public bool Execute()
        {
            if (progress == null)
                throw new InvalidOperationException("Task was not set");
            
            return progress.MoveNext();
        }

        // Получение итератора (грубо говоря счётчика) для отслеживания и вывода прогресса выполнения задачи.
        private IEnumerator<bool> GetExecutingProgress()
        {
            for (uint i = 1; i < CurrentTask?.Duration; i++)
            {
                Console.WriteLine($"Processor {Type}: Execute {CurrentTask?.Title}. Progress: {i}/{CurrentTask?.Duration}");
                yield return false;
            }

            Console.WriteLine($"Processor {Type}: {CurrentTask?.Title} has been executed");
            progress = null;
            yield return true;
        }
    }
}
