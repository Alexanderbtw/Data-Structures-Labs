namespace Lab4
{
    public class Item<T> : IEquatable<Item<T>>
    {
        public string Key { get; init; }
        public T? Value { get; set; }

        public Item(string key, T? value)
        {
            Key = key;
            Value = value;
        }

        public bool Equals(Item<T>? other)
        {
            return this.Key == other?.Key;
        }

        public override string ToString()
        {
            return $"{Key}: {Value}";
        }
    }
}
