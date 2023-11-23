using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab5_Sophie_WF
{
    public class List<T> : IEnumerable<T>, IComparer<T> where T : IComparable
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }
        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                return GetElement(index).Value;

            }
            set
            {
                GetElement(index).Value = value;
            }
        }

        public List()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        private Node<T> GetElement(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException($"Index {index} out of range {Count}");


            Node<T> temp = Head;
            int count = 0;

            while (temp != null)
            {
                if (count == index)
                {
                    break;
                }
                temp = temp.Next;
                count++;
            }

            return temp;
        }

        public void Add(T value)
        {
            Node<T> newNode = new Node<T>(value);
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
                Count++;
                return;
            }
            else if (Head == Tail)
            {
                Head.Next = newNode;
            }
            else
            {
                Tail.Next = newNode;
            }
            newNode.Prev = Tail;
            Tail = newNode;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException($"Index {index} out of range {Count}");

            if (Count == 1)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                if (index == 0)
                {
                    Head = Head.Next;
                    Head.Prev = null;
                }
                else if (index == Count - 1)
                {
                    Tail = Tail.Prev;
                    Tail.Next = null;
                }
                else
                {
                    GetElement(index + 1).Prev = GetElement(index - 1);
                    GetElement(index - 1).Next = GetElement(index + 1);
                }
            }


            Count--;
        }

        public int Compare(T x, T y)
        {
            return x.CompareTo(y);
        }

        public int Compare(T x, double y)
        {
            return x.CompareTo(y);
        }

        public void Sort()
        {
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    if (GetElement(i).Value.CompareTo(GetElement(j)) > 0)
                    {
                        T temp = GetElement(i).Value;
                        GetElement(i).Value = GetElement(j).Value;
                        GetElement(j).Value = temp;
                    }
                }
            }
        }

        public void Sort(T compare)
        {
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    if (GetElement(i).Value.CompareTo(compare) > 0)
                    {
                        T temp = GetElement(i).Value;
                        GetElement(i).Value = GetElement(j).Value;
                        GetElement(j).Value = temp;
                    }
                }
            }
        }

        public void Sort(double compare)
        {
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    if (GetElement(i).Value.CompareTo(compare) > 0)
                    {
                        T temp = GetElement(i).Value;
                        GetElement(i).Value = GetElement(j).Value;
                        GetElement(j).Value = temp;
                    }
                }
            }
        }

        public List<T> Where(T compare)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < Count; i++)
            {
                if (GetElement(i).Value.CompareTo(compare) > 0)
                {
                    result.Add(this[i]);
                }
            }

            return result;
        }

        public List<T> Where(double compare)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < Count; i++)
            {
                if (GetElement(i).Value.CompareTo(Math.Round(compare, 3)) == 0)
                {
                    result.Add(this[i]);
                }
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = Head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void SaveToFile(string fileName)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            serializerSettings.TypeNameHandling = TypeNameHandling.All;
            T[] array = new T[Count];

            for (int i = 0; i < Count; i++)
            {
                array[i] = this[i];
            }

            string json = JsonConvert.SerializeObject(array, serializerSettings);

            using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.Write(json);
                }
            }
        }

        //public static List<T> LoadFromFile(string fileName)
        //{
        //    string json = "";

        //    using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        //    {
        //        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        //        {
        //            while (!reader.EndOfStream)
        //            {
        //                json += reader.ReadLine();
        //            }
        //        }
        //    }

        //    return JsonConvert.DeserializeObject<List<T>>(json);
        //}
    }
}
