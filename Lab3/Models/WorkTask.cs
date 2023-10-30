using Lab3.Enums;

namespace Lab3.Models
{
    public readonly struct WorkTask
    {
        public string Title { get; init; }

        public uint Duration { get; init; }

        public TaskType Type { get; init; }

        public WorkTask(string title, uint duration, TaskType _type)
        {
            Title = title;
            Duration = duration;
            Type = _type;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Duration: {Duration}, Type: {Type}";
        }
    }
}
