namespace Lab4
{
    public interface IFileGenerator<T> where T : IEquatable<T>
    {
        byte[] Generate(MyHashTable<T> hashTable);
    }
}
