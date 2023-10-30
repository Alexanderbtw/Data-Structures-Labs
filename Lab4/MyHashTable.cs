using System.Collections;
using System.Text.RegularExpressions;

namespace Lab4
{
    public partial class MyHashTable<TValue> : IEnumerable where TValue : IEquatable<TValue>
    {
        private Bucket<Item<TValue>>[] buckets;
        private int max_size = 3000;

        private int all_count = 0;
        public int AllCount { get { return all_count; } }

        public MyHashTable()
        {
            buckets = new Bucket<Item<TValue>>[max_size];

            for (int i = 0; i < max_size; i++)
            {
                buckets[i] = new Bucket<Item<TValue>>(i);
            }
        }

        public int MaxCount 
        { 
            get 
            {
                return buckets.Max(b => b.Count);
            } 
        }

        public int MinCount
        {
            get
            {
                return buckets.Min(b => b.Count);
            }
        }

        public int Factor
        {
            get
            {
                return AllCount / max_size;
            }
        }

        private int GetHash(string key)
        {
            if (!KeyRegExp().IsMatch(key))
                throw new ArgumentException($"The key {key} must be in the format LLddLL, where L is a capital letter and d is a number");

            int hash = 0;
            for (int i = 0; i < key.Length; i++)
            {
                hash += key[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return Math.Abs(hash % max_size);
        }

        public void Add(Item<TValue> item)
        {
            if (Contains(item))
                throw new ArgumentException($"Item with key {item.Key} already exists");

            int hash = GetHash(item.Key);

            int qp = 0, temp_hash;
            do
            {
                temp_hash = (hash + qp * qp) % max_size;
                qp++;
            } while (qp < max_size && buckets[temp_hash].Count > Factor);

            buckets[temp_hash].Add(item);
            all_count++;
        }

        public void Add(string key, TValue value)
        {
            Add(new Item<TValue>(key, value));
        }

        public int Find(Item<TValue> item, out Item<TValue>? res_item)
        {
            int hash = GetHash(item.Key);

            int qp = 0, temp_hash;
            res_item = null;
            do
            {
                temp_hash = (hash + qp * qp) % max_size;
                res_item = buckets[temp_hash].Find(item);
                qp++;
            } while (qp < max_size && res_item == null);

            return buckets[temp_hash].Key;
        }

        public int Find(string key, out Item<TValue>? res_item)
        {
            return Find(new Item<TValue>(key, default), out res_item);
        }

        public bool Contains(Item<TValue> item)
        {
            int hash = GetHash(item.Key);

            int qp = 0, temp_hash;
            while (qp++ < max_size)
            {
                temp_hash = (hash + qp * qp) % max_size;
                if (buckets[temp_hash].Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Contains(string key)
        {
            return Contains(new Item<TValue>(key, default));
        }

        public bool Remove(Item<TValue> item)
        {
            int hash = GetHash(item.Key);

            int qp = 0, temp_hash;
            while (qp++ < max_size)
            {
                temp_hash = (hash + qp * qp) % max_size;
                if (buckets[temp_hash].Remove(item))
                {
                    all_count--;
                    return true;
                }
            }

            return false;
        }

        public bool Remove(string key)
        {
            return Remove(new Item<TValue>(key, default));
        }

        public IEnumerator<Bucket<Item<TValue>>> GetBucketEnumerator()
        {
            foreach (var bucket in buckets)
            {
                yield return bucket;
            }
        }

        public IEnumerator<Item<TValue>> GetItemEnumerator()
        {
            foreach (var bucket in buckets)
            {
                foreach (var node in bucket)
                {
                    yield return node;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetItemEnumerator();
        }

        public byte[] ExportToFile(IFileGenerator<TValue> generator)
        {
            return generator.Generate(this);
        }

        [GeneratedRegex("[A-Z]{2}\\d{2}[A-Z]{2}")]
        private static partial Regex KeyRegExp();
    }
}
