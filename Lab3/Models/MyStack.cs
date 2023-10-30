using System.Collections;
using System.Text;

namespace Lab3.Models
{
    public class MyStack<T> : IEnumerable<T>
    {
        private class Node
        {
            public T Value { get; set; }
            public Node? Next { get; set; }

            public Node(T value)
            {
                Value = value;
                Next = null;
            }

            public Node(T value, Node next)
            {
                Value = value;
                Next = next;
            }
        }

        private Node? head;
        private int count;

        public MyStack()
        {
            head = null;
            count = 0;
        }

        public int Count { get { return count; } }
        public bool IsEmpty { get { return head == null; } }

        // Просмотр верхнего элемента
        public T Peek()
        {
            if (head == null)
            {
                throw new InvalidOperationException("The stack was empty");
            }

            return head.Value;
        }

        // Удаление и возврат верхнего элемента
        public T Pop()
        {
            if (head == null)
            {
                throw new InvalidOperationException("The stack was empty");
            }

            T result = head.Value;
            head = head.Next;
            count--;
            return result;
        }

        // Вставка элемента
        public void Push(T value)
        {
            if (head == null)
            {
                head = new Node(value);
            }
            else
            {
                Node currNode = new Node(value, head);
                head = currNode;
            }
            count++;
        }

        /* Очистка стека. Происходит за счёт удаления ссылки на head,
           вследствии чего GC очистит из памяти все элемента,
           т.к. к ним больше нет доступа из программы. */
        public void Clear()
        {
            head = null;
            count = 0;
        }

        // Преобразование в строку
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            Node? currNode = head;
            while (currNode != null)
            {
                result.AppendLine(currNode.Value?.ToString());
                currNode = currNode.Next;
            }

            return result.ToString();
        }

        // Возврат итератора
        public IEnumerator<T> GetEnumerator()
        {
            Node? currNode = head;
            while (currNode != null)
            {
                T result = currNode.Value;
                currNode = currNode.Next;

                yield return result;
            }
        }

        // Обеспечение возможности получения итератора циклом
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
