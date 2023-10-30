using System.Collections; // Для IEnumerable

namespace Lab2
{
    public class MyLinkedList : IEnumerable
    {
        public class Node
        {
            public int Value { get; set; }
            public Node? Next { get; set; }

            public Node(int value)
            {
                Value = value;
            }
        }

        private Node? head;
        private Node? tail;
        private int size;

        public int Size { get { return size; } }
        public bool IsEmpty { get { return size == 0; } }
        public int Min { 
            get
            {
                Node? current = head;
                if (current == null)
                    throw new Exception("Список был пуст");

                int min = current.Value;
                for (int i = 0; i < size; i++)
                {
                    current = current.Next;
                    if (current!.Value < min)
                        min = current.Value;
                }
                return min;

            } 
        }

        public void Add(int value)
        {
            Node node = new Node(value);

            if (head == null)
                head = node;
            else
                tail!.Next = node;
            tail = node;
            tail.Next = head;
            size++;
        }

        public void AppendFirst(int value)
        {
            Node node = new Node(value);

            node.Next = head;
            head = node;
            if (size == 0) 
                tail = node;
            tail!.Next = head;
            size++;
        }

        public bool Remove(int value)
        {
            if (head == null) 
                return false;

            else if (head.Value!.Equals(value))
            {
                head = head?.Next;
                if (head == null) 
                    tail = null;
                size--;
                return true;
            }

            Node? current = head;

            while (current.Next != null)
            {
                if (current.Next.Value!.Equals(value))
                {
                    current.Next = current.Next.Next;
                    if (current.Next == null) 
                        tail = current;
                    size--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            size = 0;
        }

        public bool Contains(int value)
        {
            Node? current = head;
            while (current != null)
            {
                if (current.Value.Equals(value)) 
                    return true;
                current = current.Next;
            }
            return false;
        }

        public MyLinkedList GetSequence()
        {
            MyLinkedList result = new MyLinkedList
            {
                Min
            };

            Node? current = head;
            for (int i = 2; i < size / 2 + 1; i++)
            {
                int num = FindNearest(current!.Value * i);
                result.Add(num);
                current = current.Next;
            }

            return result;

        }

        public int FindNearest(int value)
        {
            if (head == null)
                throw new Exception("Список был пуст");

            int nearest = head.Value;
            int diff = Math.Abs(value - nearest);
            Node? current = head.Next;
            for (int i = 1; i < size; i++)
            {
                if (Math.Abs(current!.Value - value) < diff)
                {
                    diff = Math.Abs(current.Value - value);
                    nearest = current.Value;
                }
                current = current.Next;
            }
            return nearest;
        }

        public IEnumerator GetEnumerator()
        {
            Node? current = head;
            for (int i = 0; i < size; i++)
            {
                yield return current!.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            MyLinkedList linkedList = new MyLinkedList
            {
                1, 2, 3, 4, 5, 6, 7
            };

            Console.WriteLine("Иссходный список");
            foreach (var item in linkedList) Console.Write(item + " ");

            linkedList.Remove(6);
            Console.WriteLine("\n\nПосле удаления 6");
            foreach (var item in linkedList) Console.Write(item + " ");

            Console.WriteLine("\n\nЕсть ли число 5?");
            bool isPresent = linkedList.Contains(5);
            Console.WriteLine(isPresent ? "5 присутствует" : "5 отсутствует");
      
            linkedList.AppendFirst(9);
            Console.WriteLine("\nПосле добавления в начало 9");
            foreach (var item in linkedList) Console.Write(item + " ");

            Console.WriteLine("\n\nМинимальное число: " + linkedList.Min);

            Console.WriteLine("\nБлижайшее число к 10: " + linkedList.FindNearest(10));

            linkedList.Add(12);
            Console.WriteLine("\nПосле добавления 12");
            foreach (var item in linkedList) Console.Write(item + " ");

            Console.WriteLine("\n\nРазмер списка: " + linkedList.Size);

            Console.WriteLine("\nПоследовательность из задания");
            MyLinkedList changedList = linkedList.GetSequence();
            foreach (var item in changedList) Console.Write(item + " ");

            linkedList.Clear();
            Console.WriteLine("\n\nЕсть ли в списке элементы после очищения?");
            Console.WriteLine(linkedList.IsEmpty ? "Список пуст" : "В списке есть элементы");
        }
    }
}