using System.Collections;
namespace Lab4
{
    public class Bucket<T> : IEnumerable<T> where T : IEquatable<T>
    {
        public int Key { get; init; }
        private List<T> nodes;
        public int Count { get { return nodes.Count; } }

        public Bucket(int key)
        {
            Key = key;
            nodes = new List<T>();
        }

        public void Add(T item)
        {
            nodes.Add(item);
        }

        public bool Contains(T item)
        {
            return nodes.Any(it => it.Equals(item));
        }

        public T? Find(T item)
        {
            return nodes.Find(it => it.Equals(item));
        }

        public bool Remove(T item)
        {
            if (nodes.RemoveAll(it => it.Equals(item)) > 0)
                return true;
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var node in this.nodes)
            {
                yield return node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return $"{Key}: {Count}";
        }
    }
}
