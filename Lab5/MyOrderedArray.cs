using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class MyOrderedArray<T> : IEnumerable<T>, ICloneable where T : IComparable<T>, IEquatable<T>
    {
        public int Size { get; private set; }
        private int real_size = 2;
        private T[] values;

        public MyOrderedArray()
        {
            Size = 0;
            values = new T[real_size];
        }

        public MyOrderedArray(T[] _values)
        {
            Size = _values.Length;
            real_size = Size;
            values = new T[real_size];
            _values.CopyTo(values, 0);
        }

        public void Append(T item)
        {
            if (Size == real_size)
            {
                real_size *= 2;
                Array.Resize(ref values, real_size);
            }

            values[Size] = item;
            Size++;
        }

        public void Insert(T item, int index)
        {
            if (index >= Size || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (Size == real_size)
            {
                real_size *= 2;
                Array.Resize(ref values, real_size);
            }

            for (int i = Size; i > index; i--)
            {
                values[i] = values[i - 1];
            }
            values[index] = item;
            Size++;
        }

        public bool FirstRemove(T item) 
        {
            for (int i = 0; i < Size; i++)
            {
                if (values[i].Equals(item))
                {
                    for (int j = i; j < Size - 1; j++)
                    {
                        values[j] = values[j + 1];
                    }
                    Size--;
                    return true;
                }
            }

            return false;
        }

        public int RemoveAll(T item) 
        {
            int count = 0;

            int i = 0;
            while (i < Size)
            {
                if (values[i].Equals(item))
                {
                    for (int j = i; j < Size - 1; j++)
                    {
                        values[j] = values[j + 1];
                    }
                    Size--;
                    count++;
                }
                else
                {
                    i++;
                }
            }
            return count;
        }

        public (int compare, int replace) Sort()
        {
            int compare_counter = 0;
            int replace_counter = 0;

            int left = 0;
            int right = Size - 1;
            int flag = 1;
            while (left < right && flag > 0) 
            {
                flag = 0;
                for (int i = left;  i < right; i++, compare_counter++)
                {
                    if (values[i].CompareTo(values[i + 1]) > 0)
                    {
                        T temp = values[i];
                        values[i] = values[i + 1];
                        values[i + 1] = temp;
                        flag = 1;
                        replace_counter++;
                    }
                }
                right--;
                for (int i = right; i > left; i--, compare_counter++)
                {
                    if (values[i - 1].CompareTo(values[i]) > 0)
                    {
                        T temp = values[i];
                        values[i] = values[i - 1];
                        values[i - 1] = temp;
                        flag = 1;
                        replace_counter++;
                    }
                }
                left++;
            }

            return (compare_counter, replace_counter);
        }

        public int FindDublicates(out Dictionary<T, int> dublicates)
        {
            dublicates = new Dictionary<T, int>();
            var count = 0;
            foreach (var value in this)
            {
                dublicates[value] = dublicates.GetValueOrDefault(value, -1) + 1;
                count++;
            }
            dublicates = dublicates.Where(item => item.Value > 0).ToDictionary(t => t.Key, t => t.Value);
            return dublicates.Sum(t => t.Value);
        }

        public int Find(T item)
        {
            for (int i = 0; i < Size; i++)
            {
                if (values[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Size; i++)
            {
                if (values[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Size)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return values[index]!;
                }
            }

            set
            {
                if (index < 0 || index >= Size)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    values[index] = value;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                yield return values[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public object Clone()
        {
            T[] vals = (T[])values.Clone();
            return new MyOrderedArray<T>(vals);   
        }
    }
}
