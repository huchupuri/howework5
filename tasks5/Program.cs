using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Security.AccessControl;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace tasks
{
    /// <summary>
    /// структура студента
    /// </summary>
    public struct Student
    {
        public string lastName;
        public string firstName;
        public DateTime yearOfBirth;
        public string exam;
        public sbyte score;
        public Student(string lastName, string firstName, DateTime yearOfBirth, string exam, sbyte score)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.yearOfBirth = yearOfBirth;
            this.exam = exam;
            this.score = score;
        }
        /// <summary>
        /// вывод информации по студенту
        /// </summary>
        public void PrintStudent()
        {
            Console.WriteLine($"{firstName} {lastName}, год рождения: {yearOfBirth.ToString("yyyy-MM-dd")}, сданный экзамен: {exam} {score} балл(ов)");
        }
    }

    /// <summary>
    /// структура бабки
    /// </summary>
    public struct Hag
    {
        public string firstName;
        public sbyte age;
        public Dictionary<string, string> healthIssues;
        public Hag(string firstName, sbyte age, Dictionary<string, string> healthIssues)
        {
            this.firstName = firstName;
            this.age = age;
            this.healthIssues = healthIssues;
        }
        public void PrintInfo()
        {
            Console.WriteLine($"имя: {firstName}; возраст: {age}; список болезней: {string.Join(", ", healthIssues.Keys)}; список лекарств:{string.Join(", ", healthIssues.Values)}");
        }
    }

    /// <summary>
    /// класс больниц
    /// </summary>
    public class Hospital
    {
        public string title;
        public List<string> hospDiseases;
        public uint capacity;
        public uint visitors { get; private set; }
        public Hospital(string title, List<string> hospDiseases, uint capacity)
        {
            this.title = title;
            this.hospDiseases = hospDiseases;
            this.capacity = capacity;
            this.visitors = new int();
        }
        /// <summary>
        /// вывод информации по больнице
        /// </summary>
        public void PrintInfo()
        {
            Console.WriteLine($"больница: {title}; поддерживаемые болезни {string.Join(", ", hospDiseases)}; " +
                $"вместимость: {capacity};\nчисло больных: {visitors}; % заполенность: {Math.Round(((double)visitors / (double)capacity) * 100, 2)}%");
        }

        /// <summary>
        /// проверка ан наличие свободных мест в больнице
        /// </summary>
        public bool PlaceForHag(Hag hag)
        {
            if (visitors == capacity)
            {
                return false;
            }
            else if (hag.healthIssues.Count() == 0)
            {
                visitors++;
                return true;
            }
            else
            {
                double commonDiseases = 0;
                foreach (string disease in hag.healthIssues.Keys)
                {
                    if (hospDiseases.Contains(disease)) commonDiseases++;
                }
                if (commonDiseases / (double)hag.healthIssues.Count() > 0.5)
                {
                    visitors++;
                    return true;
                }
                else return false;
            }
        }
    }

    /// <summary>
    /// класс графов
    /// </summary>
    public class Graph
    {
        private int vertices;
        private List<int>[] _adjacencyList;

        /// <summary>
        ///созадание графа 
        /// </summary>
        public Graph(int vertices)
        {
            this.vertices = vertices;
            _adjacencyList = new List<int>[vertices];
            for (int i = 0; i < vertices; i++)
                _adjacencyList[i] = new List<int>();
        }


        /// <summary>
        /// добавление смежных вершин
        /// </summary>
        public void AddEdge(int v, int w)
        {
            _adjacencyList[v].Add(w);
        }


        /// <summary>
        /// вывод графа
        /// </summary>
        public void PrintGraph()
        {
            for (int i = 0; i < this.vertices; i++)
            {
                Console.Write("Vertex " + i + ": ");
                foreach (int vertex in _adjacencyList[i])
                    Console.Write(vertex + " ");
                Console.WriteLine();
            }
        }


        /// <summary>
        /// нахождение кратчайшего пути алгоритмом BFS
        /// </summary>
        public List<int> FindShortestPathBFS(int start, int target)
        {
            HashSet<int> visited = new HashSet<int>();
            Queue<List<int>> queue = new Queue<List<int>>();

            queue.Enqueue(new List<int> { start });
            visited.Add(start);

            while (queue.Count > 0)
            {
                List<int> path = queue.Dequeue();
                int currentNode = path[path.Count - 1];
                if (currentNode == target)
                {
                    return path;
                }
                foreach (int neighbor in _adjacencyList[currentNode])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(new List<int>(path) { neighbor });
                    }
                }
            }
            return new List<int>();
        }
    }
    class Program
    {
        static void Task1()
        {
            //Задание 1. Создать List на 64 элемента, скачать из интернета 32 пары картинок (любых). В List
            //должно содержаться по 2 одинаковых картинки. Необходимо перемешать List с
            //картинками. Вывести в консоль перемешанные номера (изначальный List и полученный
            //List). Перемешать любым способом.
            List<Image> images = new List<Image>(); //скачать nuGet пакеты System.Drawing.Common
            for (int i = 0; i < 32; i++)
            {
                images.Add(Image.FromFile($"Resource2\\img_1 — копия ({i}).jpg"));
                images.Add(Image.FromFile($"Resource2\\img_1 — копия ({i}).jpg"));
            }
            List<int> originalIndices = Enumerable.Range(0, images.Count).ToList();
            Random rand = new Random();
            List<Image> shuffledList = images.OrderBy(x => rand.Next()).ToList();
            Console.WriteLine("Исходный список индексов:");
            Console.WriteLine(string.Join(" ", originalIndices));

            Console.WriteLine("\nПеремешанный список индексов:");
            for (int i = 0; i < shuffledList.Count; i++)
            {
                int originalIndex = images.IndexOf(shuffledList[i]);
                Console.Write(originalIndex + " ");
            }


        }

        //        2.Создать студента из вашей группы(фамилия, имя, год рождения, с каким экзаменом
        //поступил, баллы). Создать словарь для студентов из вашей группы(10 человек).
        //Необходимо прочитать информацию о студентах с файла.
        static void Task2()
        {
            Console.WriteLine("задание 2");

            Dictionary<int, Student> students = new Dictionary<int, Student>();
            string path = "Resource2\\students.txt";
            string[] lines = File.ReadAllLines(path);

            sbyte i = 1;
            foreach (string line in lines)//добавление студентов в словарь
            {
                string[] options = line.Split(new char[] { ' ' });
                students.Add(i, new Student(options[0], options[1], Convert.ToDateTime(options[2]), options[3], sbyte.Parse(options[4])));
                i++;
            }
            foreach (Student student in students.Values) //вывод студентов
            {
                student.PrintStudent();
            }
            Console.Write("Что выполнить ('новый студент', 'удалить', 'сортировать'): ");
            switch (Console.ReadLine().ToLower())
            {
                case ("новый студент"):
                    {
                        Console.WriteLine("введите данные через пробел(фамилия, имя, дата рождения, сдваемый предмет, баллы)");
                        string[] data = Console.ReadLine().Split(new char[] { ' ' });
                        try
                        {
                            students.Add(i++, new Student(data[0], data[1], Convert.ToDateTime(data[2]), data[3], sbyte.Parse(data[4])));
                            students[i++].PrintStudent();
                        }
                        catch (System.IndexOutOfRangeException)
                        {
                            Console.WriteLine("неправильный ввод");
                        }
                        break;
                    }
                case ("удалить"):
                    {
                        Console.Write("введите имя и фимилию: ");
                        string[] id = Console.ReadLine().Split(new char[] { ' ' });
                        if (id.Length != 2)
                        {
                            Console.WriteLine("неправильный ввод");
                            break;
                        }
                        foreach (KeyValuePair<int, Student> student in students)// так как запретили использовать var
                        {
                            if (student.Value.lastName.ToLower() == id[1].ToLower() && student.Value.firstName.ToLower() == id[0].ToLower())
                            {
                                students.Remove(student.Key);
                                break;
                            }
                        }
                        foreach (Student student in students.Values) //вывод студентов
                        {
                            student.PrintStudent();
                        }
                        break;
                    }
                case ("сортировать"):
                    {
                        Dictionary<int, Student> sortedStudents = students.OrderByDescending(pair => pair.Value.score).ToDictionary(pair => pair.Key, pair => pair.Value); ;
                        foreach (Student student in sortedStudents.Values)
                        {
                            student.PrintStudent();
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("такой команды нет");
                        break;
                    }

            }
        }
        //            3. Создать бабулю. У бабули есть Имя, возраст, болезнь и лекарство от этой болезни,
        //которое она принимает(болезней может быть у бабули несколько).Реализуйте список
        //бабуль.Также есть больница(у больницы есть название, список болезней, которые они
        //лечат и вместимость).Больниц также, как и бабуль несколько. Бабули по очереди
        //поступают(реализовать ввод с клавиатуры) и дальше идут в больницу, в зависимости от
        //заполненности больницы и списка болезней, которые лечатся в данной больнице,
        //реализовать функционал, который будет распределять бабулю в нужную больницу.
        static void Task3()
        {

            Console.WriteLine("задание 3");
            Queue<Hag> hags = new Queue<Hag>(); //созадени очереди из экземпляров Hag

            hags.Enqueue(new Hag("Alice", 85, new Dictionary<string, string>
            {{ "Arthritis", "Pain Relievers" },{ "Diabetes", "Metformin" }}));
            hags.Enqueue(new Hag("Betty", 78, new Dictionary<string, string>
            {{ "Hypertension", "Lisinopril" },  {"Anemia", "Iron Supplements" }}));
            hags.Enqueue(new Hag("Catherine", 92, new Dictionary<string, string> { }));
            hags.Enqueue(new Hag("Diana", 69, new Dictionary<string, string>
            {{ "Chronic Pain", "Ibuprofen" }}));
            hags.Enqueue(new Hag("Eleanor", 75, new Dictionary<string, string>
            {{ "Dementia", "Donepezil" },{ "Depression", "Sertraline" }}));
            hags.Enqueue(new Hag("Fiona", 88, new Dictionary<string, string>
            {{ "COPD", "Bronchodilators" }, {"Diabetes", "Sugar" } }));
            hags.Enqueue(new Hag("Grace", 72, new Dictionary<string, string>
            {{ "Anemia", "Iron Supplements" },{ "Fatigue", "Vitamin B12" }}));
            foreach (Hag hag in hags) { hag.PrintInfo(); }
            Console.Write("напишите, сколько еще бабуль вы хотите добавить:");
            uint num;
            sbyte age;
            while (!uint.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("вы ввели не число");
            }
            for (int i = 0; i < num; i++)
            {
                Console.Write("введите имя: ");
                string name = Console.ReadLine();
                Console.Write("введите возраст: ");
                while (!sbyte.TryParse(Console.ReadLine(), out age))
                {
                    Console.WriteLine("вы ввели не число");
                }
                Console.Write("Введите болезни (через ,): ");
                string[] diseases = Console.ReadLine().Split(',');
                Console.Write("Введите лекарства (через ,): ");
                string[] drugs = Console.ReadLine().Split(',');
                if (diseases.Length != drugs.Length)
                {
                    Console.WriteLine("число болезней должно соотвествовать число лекарст, повторите ввод");
                    i--;
                }
                Dictionary<string, string> illness = new Dictionary<string, string>();
                for (int n = 0; n < diseases.Length; n++)
                {
                    illness[diseases[n]] = drugs[n];
                }
                hags.Enqueue(new Hag(name, age, illness));

            }

            Stack<Hospital> hospitals = new Stack<Hospital>(); //созадние стэка из экземляров HOspital
            hospitals.Push(new Hospital("first Hospital", new List<string> { "Flu", "Diabetes", "Hypertension" }, 3));
            hospitals.Push(new Hospital("second Hospital", new List<string> { "Arthritis", "Asthma", "Heart Disease", "COPD" }, 5));
            hospitals.Push(new Hospital("third Hospital", new List<string> { "Cataracts", "Bronchodilators", "COPD", "Diabetes" }, 2));
            while (hags.Count > 0)
            {
                Hag hag = hags.Dequeue();
                bool flag = false;
                string inHospital;
                foreach (Hospital hospital in hospitals.Reverse())
                {
                    if (hospital.PlaceForHag(hag))
                    {
                        flag = true;
                        Console.WriteLine($"{hag.firstName} in {hospital.title}");
                        break;
                    }
                }
                if (!flag) Console.WriteLine($"{hag.firstName} is crying on the street");
            }
            foreach (Hospital hospital in hospitals) { hospital.PrintInfo(); }
        }

        //Написать метод для обхода графа в глубину или ширину - вывести на экран кратчайший
        //путь.
        static void Task4()
        {
            Console.WriteLine("задание 4");
            Graph graph = new Graph(5);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(4, 5);
            Console.WriteLine(string.Join(" ", graph.FindShortestPathBFS(1, 5)));
        }

        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
        }
    }
}