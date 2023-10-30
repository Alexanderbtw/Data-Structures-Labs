using System.Collections;
using System.Text;

namespace Lab3.Models
{
    public class MyQueue<T> : IEnumerable<T>
    {
        private int left = -1;
        private int right = -1;
        private int count = 0;
        private readonly int size;
        private readonly T[] array;

        public MyQueue(int _size)
        {
            size = _size;
            array = new T[_size];
        }

        public int Size { get { return size; } }
        public int Count { get { return count; } }

        public bool IsFull { get { return right == size - 1; } }

        public bool IsEmpty { get { return count == 0; } }

        // Вставка элемента
        public void Push(T item)
        {
            if (IsFull)
                throw new InvalidOperationException("The queue was overflow");

            array[++right] = item;
            count++;
        }

        // Просмотр первого элемента
        public T Peek()
        {
            if (IsEmpty)
                throw new Exception("Очередь не заполнена.");
            T value = array[left + 1];
            return value;
        }

        // Удаление и возврат первого элемента
        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("The queue was empty");

            T value = array[++left];
            count--;
            if (left == right)
            {
                left = -1;
                right = -1;
            }
            return value;
        }

        // Очищение списка. Реализуется за счёт сдвига левого и правого индексов в начало,
        // что ведёт к перезаписи данных при последующей вставке
        public void Clear()
        {
            left = -1;
            right = -1;
            count = 0;
        }

        // Преобразование в строку
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            for (int i = left + 1; i <= right; i++)
            {
                result.AppendLine(array[i]?.ToString());
            }

            return result.ToString();
        }

        // Возврат итератора
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = left + 1; i <= right; i++)
            {
                yield return array[i];
            }
        }

        // Обеспечение возможности получения итератора циклом
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
