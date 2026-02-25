using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;

public class Matrix
{
    private int[,] data;

    public int[,] Data
    {
        get { return data; }
        set { data = value; }
    }

    public Matrix(int n, int m, bool fillFromKeyboard)
    {
        data = new int[n, m];
        if (fillFromKeyboard)
        {
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    Console.Write($"Введите элемент [{i},{j}]: ");
                    while (!int.TryParse(Console.ReadLine(), out data[i, j]))
                    {
                        Console.Write("Ошибка! Введите целое число: ");
                    }
                }
            }
        }
    }

    public Matrix(int n, bool randomWithIncreasingDigits)
    {
        data = new int[n, n];
        Random rand = new Random();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int num;
                do
                {
                    num = rand.Next(100, 1000);
                } while (!IsIncreasingDigits(num));
                data[i, j] = num;
            }
        }
    }

    public Matrix(int n)
    {
        data = new int[n, n];
        int[,] pattern = new int[5, 5] {
            { 1, 2, 1, 4, 1 },
            { 1, 2, 3, 2, 4 },
            { 3, 2, 3, 3, 1 },
            { 1, 4, 2, 2, 2 },
            { 5, 1, 3, 1, 1 }
        };

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                data[i, j] = pattern[i % 5, j % 5];
            }
        }
    }

    public Matrix(int[] arr)
    {
        int n = arr.Length;
        data = new int[n, n];
        
        for (int j = 0; j < n; j++)
        {
            int sum = 0;
            for (int i = 0; i < n - 1; i++)
            {
                data[i, j] = 1;
                sum += 1;
            }
            data[n - 1, j] = arr[j] - sum;
        }
    }

    private bool IsIncreasingDigits(int num)
    {
        int digit1 = num / 100;
        int digit2 = (num / 10) % 10;
        int digit3 = num % 10;
        return digit1 < digit2 && digit2 < digit3;
    }

    public static Matrix operator +(Matrix a, Matrix b)
    {
        int n = a.data.GetLength(0);
        int m = a.data.GetLength(1);
        Matrix result = new Matrix(n, m, false);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                result.data[i, j] = a.data[i, j] + b.data[i, j];
            }
        }
        return result;
    }

    public static Matrix operator -(Matrix a, Matrix b)
    {
        int n = a.data.GetLength(0);
        int m = a.data.GetLength(1);
        Matrix result = new Matrix(n, m, false);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                result.data[i, j] = a.data[i, j] - b.data[i, j];
            }
        }
        return result;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        int n = a.data.GetLength(0);
        int m = b.data.GetLength(1);
        int k = a.data.GetLength(1);
        Matrix result = new Matrix(n, m, false);
        
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                int sum = 0;
                for (int t = 0; t < k; t++)
                {
                    sum += a.data[i, t] * b.data[t, j];
                }
                result.data[i, j] = sum;
            }
        }
        return result;
    }

    public static Matrix operator *(int scalar, Matrix a)
    {
        int n = a.data.GetLength(0);
        int m = a.data.GetLength(1);
        Matrix result = new Matrix(n, m, false);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                result.data[i, j] = scalar * a.data[i, j];
            }
        }
        return result;
    }

    public Matrix Transpose()
    {
        int n = data.GetLength(0);
        int m = data.GetLength(1);
        Matrix result = new Matrix(m, n, false);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                result.data[j, i] = data[i, j];
            }
        }
        return result;
    }

    public override string ToString()
    {
        string result = "";
        int n = data.GetLength(0);
        int m = data.GetLength(1);
        
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                result += data[i, j].ToString().PadLeft(4);
            }
            result += "\n";
        }
        return result;
    }
}

[Serializable]
public struct Toy
{
    public string Name { get; set; }
    public int Price { get; set; }
    public int MinAge { get; set; }
    public int MaxAge { get; set; }

    public Toy(string name, int price, int minAge, int maxAge)
    {
        Name = name;
        Price = price;
        MinAge = minAge;
        MaxAge = maxAge;
    }
}

public static class FileTasks
{
    private static Random rand = new Random();

    public static void CreateBinaryFile(string filename, int count)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(rand.Next(1, 100));
            }
        }
    }

    public static void ProcessBinaryFile(string inputFile, string outputFile)
    {
        List<int> numbers = new List<int>();
        using (BinaryReader reader = new BinaryReader(File.Open(inputFile, FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                numbers.Add(reader.ReadInt32());
            }
        }

        using (BinaryWriter writer = new BinaryWriter(File.Open(outputFile, FileMode.Create)))
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    writer.Write(numbers[j]);
                }
            }
        }
    }

    public static void CreateToysFile(string filename)
    {
        List<Toy> toys = new List<Toy>
        {
            new Toy("Кукла", 500, 3, 7),
            new Toy("Машинка", 300, 2, 5),
            new Toy("Конструктор", 1000, 5, 12),
            new Toy("Мяч", 200, 1, 10),
            new Toy("Пазл", 400, 4, 8),
            new Toy("Робот", 1500, 8, 15),
            new Toy("Кубики", 250, 1, 4)
        };

        XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
        using (FileStream fs = new FileStream(filename, FileMode.Create))
        {
            serializer.Serialize(fs, toys);
        }
    }

    public static List<string> FindToysForAges(string filename)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
        List<Toy> toys;
        using (FileStream fs = new FileStream(filename, FileMode.Open))
        {
            toys = (List<Toy>)serializer.Deserialize(fs);
        }

        List<string> result = new List<string>();
        foreach (Toy toy in toys)
        {
            if (toy.MinAge <= 4 && toy.MaxAge >= 4 && toy.MinAge <= 10 && toy.MaxAge >= 10)
            {
                result.Add(toy.Name);
            }
        }
        return result;
    }

    public static void CreateTextFileOnePerLine(string filename, int count)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(rand.Next(-50, 50));
            }
        }
    }

    public static int ProductMinMax(string filename)
    {
        List<int> numbers = new List<int>();
        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (int.TryParse(line, out int num))
                {
                    numbers.Add(num);
                }
            }
        }

        if (numbers.Count == 0) return 0;
        return numbers.Max() * numbers.Min();
    }

    public static void CreateTextFileMultiplePerLine(string filename, int rows, int cols)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    writer.Write(rand.Next(-20, 20));
                    if (j < cols - 1) writer.Write(" ");
                }
                writer.WriteLine();
            }
        }
    }

    public static int CountOddNumbers(string filename)
    {
        int count = 0;
        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int num) && num % 2 != 0)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    public static void CreateLengthFile(string inputFile, string outputFile)
    {
        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            using (StreamReader reader = new StreamReader(inputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    writer.WriteLine(line.Length);
                }
            }
        }
    }

    public static void CreateTextFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine("Hello world");
            writer.WriteLine("This is a test file");
            writer.WriteLine("C# programming");
            writer.WriteLine("Binary and text files");
            writer.WriteLine("Laboratory work");
        }
    }
}

public class Program
{
    private static int ReadInt(string message)
    {
        int result;
        Console.Write(message);
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.Write("Ошибка! Введите целое число: ");
        }
        return result;
    }

    public static void Main()
    {
        Console.WriteLine("Задание 1: Заполнение двумерных массивов\n");

        Console.WriteLine("Первый массив (заполнение с клавиатуры по столбцам):");
        int n1 = ReadInt("Введите количество строк: ");
        int m1 = ReadInt("Введите количество столбцов: ");
        Matrix matrix1 = new Matrix(n1, m1, true);
        Console.WriteLine("\nПолученный массив:");
        Console.WriteLine(matrix1);

        Console.WriteLine("\nВторой массив (трехзначные числа с возрастающими цифрами):");
        int n2 = ReadInt("Введите размер квадратного массива: ");
        Matrix matrix2 = new Matrix(n2, true);
        Console.WriteLine(matrix2);

        Console.WriteLine("\nТретий массив (по шаблону):");
        int n3 = ReadInt("Введите размер квадратного массива: ");
        Matrix matrix3 = new Matrix(n3);
        Console.WriteLine(matrix3);

        Console.WriteLine("\nЗадание 2: Работа с двумерными массивами\n");
        Console.WriteLine("Введите одномерный массив:");
        int size = ReadInt("Введите размер массива: ");
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = ReadInt($"Элемент [{i}]: ");
        }
        Matrix matrix4 = new Matrix(arr);
        Console.WriteLine("\nПолученная матрица:");
        Console.WriteLine(matrix4);

        Console.WriteLine("\nЗадание 3: Работа с матрицами (А+4*В)-Ст\n");
        
        Console.WriteLine("Введите матрицу A:");
        int nA = ReadInt("Введите количество строк: ");
        int mA = ReadInt("Введите количество столбцов: ");
        Matrix A = new Matrix(nA, mA, true);
        
        Console.WriteLine("\nВведите матрицу B:");
        Matrix B = new Matrix(nA, mA, true);
        
        Console.WriteLine("\nВведите матрицу C:");
        Matrix C = new Matrix(nA, mA, true);

        Console.WriteLine("\nМатрица A:");
        Console.WriteLine(A);
        Console.WriteLine("Матрица B:");
        Console.WriteLine(B);
        Console.WriteLine("Матрица C:");
        Console.WriteLine(C);

        Matrix result = (A + 4 * B) - C.Transpose();
        
        Console.WriteLine("\nРезультат (А + 4*В) - С^T:");
        Console.WriteLine(result);

        Console.WriteLine("\nЗадание 4: Бинарные файлы\n");
        FileTasks.CreateBinaryFile("input4.bin", 10);
        Console.WriteLine("Создан исходный бинарный файл с 10 случайными числами");
        FileTasks.ProcessBinaryFile("input4.bin", "output4.bin");
        Console.WriteLine("Создан обработанный файл");

        Console.WriteLine("\nЗадание 5: Бинарные файлы и структуры\n");
        FileTasks.CreateToysFile("toys.xml");
        Console.WriteLine("Создан файл с игрушками");
        List<string> toys = FileTasks.FindToysForAges("toys.xml");
        Console.WriteLine("Игрушки, подходящие детям 4 и 10 лет:");
        foreach (string toy in toys)
        {
            Console.WriteLine($"- {toy}");
        }

        Console.WriteLine("\nЗадание 6: Текстовые файлы (по одному числу в строке)\n");
        FileTasks.CreateTextFileOnePerLine("input6.txt", 15);
        Console.WriteLine("Создан текстовый файл с 15 числами");
        int product = FileTasks.ProductMinMax("input6.txt");
        Console.WriteLine($"Произведение максимального и минимального: {product}");

        Console.WriteLine("\nЗадание 7: Текстовые файлы (несколько чисел в строке)\n");
        FileTasks.CreateTextFileMultiplePerLine("input7.txt", 5, 4);
        Console.WriteLine("Создан текстовый файл с 5 строками по 4 числа");
        int oddCount = FileTasks.CountOddNumbers("input7.txt");
        Console.WriteLine($"Количество нечётных элементов: {oddCount}");

        Console.WriteLine("\nЗадание 8: Текстовый файл (длины строк)\n");
        FileTasks.CreateTextFile("input8.txt");
        Console.WriteLine("Создан исходный текстовый файл");
        FileTasks.CreateLengthFile("input8.txt", "output8.txt");
        Console.WriteLine("Создан файл с длинами строк");
        
        Console.WriteLine("\nСодержимое исходного файла:");
        using (StreamReader reader = new StreamReader("input8.txt"))
        {
            string line;
            int lineNum = 1;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine($"Строка {lineNum}: '{line}' (длина: {line.Length})");
                lineNum++;
            }
        }
        
        Console.WriteLine("\nСодержимое файла с длинами:");
        using (StreamReader reader = new StreamReader("output8.txt"))
        {
            string line;
            int lineNum = 1;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine($"Строка {lineNum}: {line}");
                lineNum++;
            }
        }
    }
}