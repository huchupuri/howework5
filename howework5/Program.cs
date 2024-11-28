using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
namespace tumakoff
{
    class Program
    {
        /// <summary>
        /// подсчет гласных и согласных букв
        /// </summary>
        static void CountLetters(char[] text, out int vowels, out int consonants)
        {
            char[] alphVowels = { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };
            char[] alphConsonants = { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ' };

            vowels = 0;
            consonants = 0;
            foreach (char letter in text)
            {
                if (alphVowels.Contains(letter))
                {
                    vowels++;
                }
                else if (alphConsonants.Contains(letter))
                {
                    consonants++;
                }
            }
        }

        /// <summary>
        /// подсчет гласных и согласных
        /// </summary>
        static void CountLettersWithList(List<char> text, out int vowels, out int consonants)
        {
            List<char> alphVowels = new List<char>() { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };
            List<char> alphConsonants = new List<char>() { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ' };

            vowels = 0;
            consonants = 0;
            foreach (char letter in text)
            {
                if (alphVowels.Contains(letter))
                {
                    vowels++;
                }
                else if (alphConsonants.Contains(letter))
                {
                    consonants++;
                }
            }
        }

        /// <summary>
        ///умножение матриц 
        /// </summary>
        static int[,] MatrixMult(int[,] matrixA, int[,] matrixB)
        {

            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int rowsB = matrixB.GetLength(0);
            int colsB = matrixB.GetLength(1);

            int[,] resultMatrix = new int[rowsA, rowsB];
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < rowsB; j++)
                {
                    for (int k = 0; k < colsA; k++)
                    {
                        resultMatrix[i, j] += matrixA[i, k] * matrixB[j, k];
                    }
                }
            }
            return resultMatrix;
        }

        /// <summary>
        /// умножение матриц linkedlist
        /// </summary>
        public static LinkedList<LinkedList<int>> MultiplyMatrices(LinkedList<LinkedList<int>> matrix1, LinkedList<LinkedList<int>> matrix2)
        {
            int rows1 = matrix1.Count;
            int cols1 = matrix1.First.Value.Count;
            int rows2 = matrix2.Count;
            int cols2 = matrix2.First.Value.Count;

            if (cols1 != rows2)
            {
                throw new InvalidOperationException("Количество столбцов первой матрицы должно быть равно количеству строк второй матрицы.");
            }

            var resultMatrix = new LinkedList<LinkedList<int>>();

            // Выполняем умножение
            for (int i = 0; i < rows1; i++)
            {
                var newRow = new LinkedList<int>();
                for (int j = 0; j < cols2; j++)
                {
                    int value = 0;
                    for (int k = 0; k < cols1; k++)
                    {
                        value += GetElement(matrix1, i, k) * GetElement(matrix2, k, j);
                    }
                    newRow.AddLast(value);
                }
                resultMatrix.AddLast(newRow);
            }
            return resultMatrix;
        }

        /// <summary>
        /// выовд матрицы
        /// </summary>
        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int n = 0; n < matrix.GetLength(1); n++)
                {
                    Console.Write(matrix[i, n] + " ");
                }
                Console.WriteLine("\n");
            }
        }

        /// <summary>
        /// вывод матрицы linkedLIst
        /// </summary>
        static void PrintMatrix(LinkedList<LinkedList<int>> matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var item in row)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// достать элемент
        /// </summary>
        private static int GetElement(LinkedList<LinkedList<int>> matrix, int row, int col)
        {
            var rowNode = matrix.First;
            for (int i = 0; i < row; i++)
            {
                rowNode = rowNode.Next;
            }

            var colNode = rowNode.Value.First;
            for (int j = 0; j < col; j++)
            {
                colNode = colNode.Next;
            }

            return colNode.Value;
        }

        /// <summary>
        /// средняя температура для двумерных массивов
        /// </summary>
        static double[] AverageTempr(int[,] matrix)
        {
            double[] temprInMonths = new double[12];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double tempMonth = 0;
                for (int n = 0; n < matrix.GetLength(1); n++)
                {
                    tempMonth += matrix[i, n];
                }
                temprInMonths[i] = tempMonth / 30;
            }
            Array.Sort(temprInMonths);
            return temprInMonths;
        }
        /// <summary>
        /// средняя температура для массива
        /// </summary>
        static double AverageTempr(int[] matrix)
        {
            double tempMonth = 0;
            for (int n = 0; n < 30; n++)
            {
                tempMonth += matrix[n];
            }
            double result = tempMonth / 30;
            return result;
        }

        //        Написать программу, которая вычисляет число гласных и согласных букв в
        //файле.Имя файла передавать как аргумент в функцию Main.Содержимое текстового файла
        //заносится в массив символов.Количество гласных и согласных букв определяется проходом
        //по массиву.Предусмотреть метод, входным параметром которого является массив символов.
        //Метод вычисляет количество гласных и согласных букв.
        static void Task1(string args)
        {
            Console.WriteLine("\nзадание 1\n");
            string path = ($"Resource1\\{args}");
            using StreamReader reader = new(path);
            char[] text = reader.ReadToEnd().ToLower().ToArray<char>();
            CountLetters(text, out int vowels, out int consonants);
            Console.WriteLine($"гласных: {vowels}, согласных: {consonants}");
        }

        //        Упражнение 6.2 Написать программу, реализующую умножению двух матриц, заданных в
        //виде двумерного массива.В программе предусмотреть два метода: метод печати матрицы,
        //метод умножения матриц(на вход две матрицы, возвращаемое значение – матрица).
        static void Task2()
        {
            Console.WriteLine("\nзадание 2(умножение матриц)\n");
            int[,] matrixA = new int[,] { { 5, 4, 6 }, { 4, 9, 6 } };
            int[,] matrixB = new int[,] { { 4, 4, 6 }, { 7, 0, 6 } };
            PrintMatrix(MatrixMult(matrixA, matrixB));
        }

        //        Упражнение 6.3 Написать программу, вычисляющую среднюю температуру за год.Создать
        //двумерный рандомный массив temperature[12, 30], в котором будет храниться температура
        //для каждого дня месяца (предполагается, что в каждом месяце 30 дней). Сгенерировать
        //значения температур случайным образом.Для каждого месяца распечатать среднюю
        //температуру.Для этого написать метод, который по массиву temperature [12, 30] для каждого
        //месяца вычисляет среднюю температуру в нем, и в качестве результата возвращает массив
        //средних температур. Полученный массив средних температур отсортировать по
        //возрастанию.
        static void Task3()
        {
            Console.WriteLine("\nзадание 3(средняя температура)\n");
            string[] months =
            {
                "Январь",
                "Февраль",
                "Март",
                "Апрель",
                "Май",
                "Июнь",
                "Июль",
                "Август",
                "Сентябрь",
                "Октябрь",
                "Ноябрь",
                "Декабрь"
            };
            int[,] daysTemperature = new int[12, 30];
            Random rnd = new Random();
            for (int i = 0; i < daysTemperature.GetLength(0); i++)
            {
                for (int j = 0; j < daysTemperature.GetLength(1); j++)
                {
                    daysTemperature[i, j] = rnd.Next(-15, 30);
                }
            }
            double[] AverageTemprInMonths = AverageTempr(daysTemperature);
            for (int i = 1; i < 13; i++)
            {
                Console.WriteLine(string.Format($"{months[i - 1]} средняя температура: {Math.Round(AverageTemprInMonths[i - 1], 1)}"));
            }

        }

        //Домашнее задание 6.1 Упражнение 6.1 выполнить с помощью коллекции List<T>.
        static void Task4(string path)
        {
            Console.WriteLine("\nзадание 4(повторение 1го с помощью List\n");
            using StreamReader reader = new(path);
            List<char> text = new List<char>(reader.ReadToEnd().ToLower().ToArray<char>());
            CountLettersWithList(text, out int vowels, out int consonants);

            Console.WriteLine($"гласных: {vowels}, согласных: {consonants}");
        }
        //        Домашнее задание 6.2 Упражнение 6.2 выполнить с помощью коллекций
        //LinkedList<LinkedList<T>>.
        static void Task5()
        {
            Console.WriteLine("\nзадание 5(повторение с linked)\n");
            LinkedList<LinkedList<int>> matrix1 = new LinkedList<LinkedList<int>>();
            matrix1.AddLast(new LinkedList<int>(new[] { 1, 2, 3 }));
            matrix1.AddLast(new LinkedList<int>(new[] { 3, 4, 1 }));

            LinkedList<LinkedList<int>> matrix2 = new LinkedList<LinkedList<int>>();
            matrix2.AddLast(new LinkedList<int>(new[] { 5, 6 }));
            matrix2.AddLast(new LinkedList<int>(new[] { 7, 8 }));
            matrix2.AddLast(new LinkedList<int>(new[] { 7, 11 }));

            LinkedList<LinkedList<int>> resultMatrix = MultiplyMatrices(matrix1, matrix2);

            Console.WriteLine("Результат умножения:");
            PrintMatrix(resultMatrix);
        }

        //        Домашнее задание 6.3 Написать программу для упражнения 6.3, использовав класс
        //Dictionary<TKey, TValue>.В качестве ключей выбрать строки – названия месяцев, а в
        //качестве значений – массив значений температур по дням.
        static void Task6()
        {
            Console.WriteLine("\nзадание 6 (повторение 3 с словарем)\n");
            string[] months =
                {
                    "Январь",
                    "Февраль",
                    "Март",
                    "Апрель",
                    "Май",
                    "Июнь",
                    "Июль",
                    "Август",
                    "Сентябрь",
                    "Октябрь",
                    "Ноябрь",
                    "Декабрь"
                };

            Dictionary<string, int[]> monthTempers = new Dictionary<string, int[]>();
            for (int i = 0; i < months.Length; i++)
            {
                monthTempers[months[i]] = new int[30];
            }
            Random rnd = new Random();
            foreach (int[] month in monthTempers.Values)
            {
                for (int j = 0; j < 30; j++)
                {
                    month[j] = rnd.Next(-15, 30);
                }
            }
            Dictionary<string, double> averageTemp = new Dictionary<string, double>();
            for (int i = 0; i < months.Length; i++)
            {
                averageTemp[months[i]] = Math.Round(AverageTempr(monthTempers[months[i]]), 1);
            }
            foreach (var pair in averageTemp
            .OrderByDescending(pair => pair.Value)
            .ToDictionary(pair => pair.Key, pair => pair.Value))
            {
                Console.WriteLine($"Месяц: {pair.Key}, Температура: {pair.Value}");
            }



        }
        static void Main(string[] args)
        {
            try
            {
                Task1(args[0]);
            }
            catch (System.IndexOutOfRangeException)
            {
                Console.Write("вы не ввели путь к файлу, повторите (text.txt):");
                string path = Console.ReadLine();
                if (path.Length != 0) Task1(path);

            }
            Task2();
            Task3();
            try
            {
                Task4(args[0]);
            }
            catch (System.IndexOutOfRangeException)
            {
                Console.Write("вы не ввели путь к файлу, повторите (text.txt):");
                string path2 = Console.ReadLine();
                if (path2.Length != 0) Task4(path2);
            }
            Task5();
            Task6();
        }
    }
}