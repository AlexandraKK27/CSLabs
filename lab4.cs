using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class CollectionTasks
{
    private static Random rand = new Random();

    public static List<T> InsertAfterFirst<T>(List<T> L, T E)
    {
        int index = L.IndexOf(E);
        if (index != -1)
        {
            List<T> result = new List<T>();
            for (int i = 0; i <= index; i++)
            {
                result.Add(L[i]);
            }
            foreach (T item in L)
            {
                result.Add(item);
            }
            for (int i = index + 1; i < L.Count; i++)
            {
                result.Add(L[i]);
            }
            return result;
        }
        return new List<T>(L);
    }

    public static LinkedList<T> AddFirstAndLast<T>(LinkedList<T> L, T E)
    {
        LinkedList<T> result = new LinkedList<T>();
        result.AddFirst(E);
        foreach (T item in L)
        {
            result.AddLast(item);
        }
        result.AddLast(E);
        return result;
    }

    public static void AnalyzeBookReadings(Dictionary<string, HashSet<int>> bookReaders, int totalReaders)
    {
        HashSet<string> allBooks = new HashSet<string>(bookReaders.Keys);
        HashSet<string> readByAll = new HashSet<string>();
        HashSet<string> readByNone = new HashSet<string>();
        HashSet<string> readBySome = new HashSet<string>();

        foreach (var book in allBooks)
        {
            if (bookReaders[book].Count == totalReaders)
                readByAll.Add(book);
            else if (bookReaders[book].Count == 0)
                readByNone.Add(book);
            else
                readBySome.Add(book);
        }

        Console.WriteLine("Книги, прочитанные всеми:");
        foreach (var book in readByAll) Console.WriteLine($"- {book}");

        Console.WriteLine("\nКниги, прочитанные некоторыми:");
        foreach (var book in readBySome) Console.WriteLine($"- {book}");

        Console.WriteLine("\nКниги, не прочитанные никем:");
        foreach (var book in readByNone) Console.WriteLine($"- {book}");
    }

    public static HashSet<char> FindCharsNotInFirstButInOthers(string filename)
    {
        string text = File.ReadAllText(filename, Encoding.UTF8);
        string[] words = text.Split(new[] { ' ', '\n', '\r', '\t', '.', ',', '!', '?', ';', ':', '-' }, 
            StringSplitOptions.RemoveEmptyEntries);

        if (words.Length < 2) return new HashSet<char>();

        HashSet<char> firstWordChars = new HashSet<char>(words[0]);
        HashSet<char> result = new HashSet<char>();

        for (char c = 'А'; c <= 'я'; c++)
        {
            bool inAllOthers = true;
            for (int i = 1; i < words.Length; i++)
            {
                if (!words[i].Contains(c))
                {
                    inAllOthers = false;
                    break;
                }
            }
            if (inAllOthers && !firstWordChars.Contains(c))
                result.Add(c);
        }

        return result;
    }

    public static void CreateOlympiadDataFile(string filename, int n)
    {
        string[] surnames = { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Попов", "Васильев", "Павлов", "Семенов", "Федоров" };
        string[] names = { "Иван", "Петр", "Сидор", "Алексей", "Дмитрий", "Михаил", "Андрей", "Сергей", "Александр", "Владимир" };
        
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(n);
            for (int i = 0; i < n; i++)
            {
                string surname = surnames[rand.Next(surnames.Length)];
                string name = names[rand.Next(names.Length)];
                int grade = rand.Next(7, 12);
                int score = rand.Next(0, 71);
                writer.WriteLine($"{surname} {name} {grade} {score}");
            }
        }
    }

    public static void AnalyzeOlympiadResults(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        int n = int.Parse(lines[0]);
        
        List<(string Surname, string Name, int Grade, int Score)> participants = new List<(string, string, int, int)>();
        
        for (int i = 1; i <= n; i++)
        {
            string[] parts = lines[i].Split(' ');
            string surname = parts[0];
            string name = parts[1];
            int grade = int.Parse(parts[2]);
            int score = int.Parse(parts[3]);
            participants.Add((surname, name, grade, score));
        }

        var sortedParticipants = participants.OrderByDescending(p => p.Score).ToList();
        
        int top25PercentCount = (int)Math.Ceiling(n * 0.25);
        int minWinnerScore = sortedParticipants[top25PercentCount - 1].Score;
        
        if (minWinnerScore <= 35)
        {
            int count = 0;
            while (count < sortedParticipants.Count && sortedParticipants[count].Score > 35)
            {
                count++;
            }
            minWinnerScore = 36;
        }
        else
        {
            while (top25PercentCount < n && sortedParticipants[top25PercentCount].Score == minWinnerScore)
            {
                top25PercentCount++;
            }
        }

        int[] winnersByGrade = new int[12];
        for (int i = 0; i < top25PercentCount; i++)
        {
            if (sortedParticipants[i].Score >= minWinnerScore)
            {
                winnersByGrade[sortedParticipants[i].Grade]++;
            }
        }

        Console.WriteLine(minWinnerScore);
        for (int grade = 7; grade <= 11; grade++)
        {
            Console.Write(winnersByGrade[grade] + (grade < 11 ? " " : "\n"));
        }
    }
}

public class Money
{
    private uint rubles;
    private byte kopeks;

    public uint Rubles
    {
        get { return rubles; }
        set { rubles = value; }
    }

    public byte Kopeks
    {
        get { return kopeks; }
        set { kopeks = value; }
    }

    public Money()
    {
        rubles = 0;
        kopeks = 0;
    }

    public Money(uint rubles, byte kopeks)
    {
        Normalize(rubles, kopeks);
    }

    private void Normalize(uint r, byte k)
    {
        rubles = r + (uint)(k / 100);
        kopeks = (byte)(k % 100);
    }

    public Money Subtract(Money other)
    {
        long totalKopeks1 = (long)rubles * 100 + kopeks;
        long totalKopeks2 = (long)other.rubles * 100 + other.kopeks;
        long resultKopeks = totalKopeks1 - totalKopeks2;
        
        if (resultKopeks < 0) resultKopeks = 0;
        
        return new Money((uint)(resultKopeks / 100), (byte)(resultKopeks % 100));
    }

    public static Money operator -(Money m1, Money m2)
    {
        long totalKopeks1 = (long)m1.rubles * 100 + m1.kopeks;
        long totalKopeks2 = (long)m2.rubles * 100 + m2.kopeks;
        long resultKopeks = totalKopeks1 - totalKopeks2;
        
        if (resultKopeks < 0) resultKopeks = 0;
        
        return new Money((uint)(resultKopeks / 100), (byte)(resultKopeks % 100));
    }

    public static Money operator ++(Money m)
    {
        long totalKopeks = (long)m.rubles * 100 + m.kopeks + 1;
        return new Money((uint)(totalKopeks / 100), (byte)(totalKopeks % 100));
    }

    public static Money operator --(Money m)
    {
        long totalKopeks = (long)m.rubles * 100 + m.kopeks - 1;
        if (totalKopeks < 0) totalKopeks = 0;
        return new Money((uint)(totalKopeks / 100), (byte)(totalKopeks % 100));
    }

    public static implicit operator uint(Money m)
    {
        return m.rubles;
    }

    public static explicit operator double(Money m)
    {
        return (double)m.kopeks / 100;
    }

    public static Money operator -(Money m, uint number)
    {
        long totalKopeks = (long)m.rubles * 100 + m.kopeks - (long)number * 100;
        if (totalKopeks < 0) totalKopeks = 0;
        return new Money((uint)(totalKopeks / 100), (byte)(totalKopeks % 100));
    }

    public static Money operator -(uint number, Money m)
    {
        long totalKopeks = (long)number * 100 - ((long)m.rubles * 100 + m.kopeks);
        if (totalKopeks < 0) totalKopeks = 0;
        return new Money((uint)(totalKopeks / 100), (byte)(totalKopeks % 100));
    }

    public override string ToString()
    {
        return $"{rubles}.{kopeks:D2}";
    }
}

public class Program
{
    private static int ReadInt(string message, int min = int.MinValue, int max = int.MaxValue)
    {
        int result;
        do
        {
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("Ошибка! Введите целое число: ");
            }
            if (result < min || result > max)
                Console.WriteLine($"Число должно быть от {min} до {max}");
        } while (result < min || result > max);
        return result;
    }

    private static uint ReadUInt(string message)
    {
        uint result;
        Console.Write(message);
        while (!uint.TryParse(Console.ReadLine(), out result))
        {
            Console.Write("Ошибка! Введите целое неотрицательное число: ");
        }
        return result;
    }

    private static byte ReadByte(string message)
    {
        byte result;
        Console.Write(message);
        while (!byte.TryParse(Console.ReadLine(), out result) || result >= 100)
        {
            Console.Write("Ошибка! Введите число от 0 до 99: ");
        }
        return result;
    }

    public static void Main()
    {
        Console.WriteLine("Задание 1: List\n");
        List<int> list1 = new List<int> { 1, 2, 3, 2, 4, 5 };
        Console.WriteLine("Исходный список: " + string.Join(" ", list1));
        int e = 2;
        Console.WriteLine($"Элемент E = {e}");
        List<int> result1 = CollectionTasks.InsertAfterFirst(list1, e);
        Console.WriteLine("Результат: " + string.Join(" ", result1));

        Console.WriteLine("\nЗадание 2: LinkedList\n");
        LinkedList<int> linkedList = new LinkedList<int>(new[] { 1, 2, 3, 4, 5 });
        Console.WriteLine("Исходный список: " + string.Join(" ", linkedList));
        int newElement = 9;
        Console.WriteLine($"Новый элемент E = {newElement}");
        LinkedList<int> result2 = CollectionTasks.AddFirstAndLast(linkedList, newElement);
        Console.WriteLine("Результат: " + string.Join(" ", result2));

        Console.WriteLine("\nЗадание 3: HashSet\n");
        Dictionary<string, HashSet<int>> bookReaders = new Dictionary<string, HashSet<int>>
        {
            { "Война и мир", new HashSet<int> { 1, 2, 3, 4, 5 } },
            { "Преступление и наказание", new HashSet<int> { 1, 2, 3 } },
            { "Анна Каренина", new HashSet<int> {  } },
            { "Евгений Онегин", new HashSet<int> { 2, 4, 5 } }
        };
        CollectionTasks.AnalyzeBookReadings(bookReaders, 5);

        Console.WriteLine("\nЗадание 4: HashSet\n");
        File.WriteAllText("text4.txt", "Привет мир как дела программа", Encoding.UTF8);
        HashSet<char> result4 = CollectionTasks.FindCharsNotInFirstButInOthers("text4.txt");
        Console.WriteLine("Символы, которых нет в первом слове, но есть в каждом из других:");
        foreach (char c in result4)
        {
            Console.Write(c + " ");
        }
        Console.WriteLine();

        Console.WriteLine("\nЗадание 5: Dictionary/SortedList\n");
        int n = ReadInt("Введите количество участников (N): ", 1, 1000);
        CollectionTasks.CreateOlympiadDataFile("olympiad.txt", n);
        Console.WriteLine("Файл с данными создан");
        Console.WriteLine("\nРезультаты анализа:");
        CollectionTasks.AnalyzeOlympiadResults("olympiad.txt");

        Console.WriteLine("\nЗадания 6 и 7: Money\n");
        
        Console.WriteLine("Создание объектов Money:");
        uint rub1 = ReadUInt("Введите рубли для первого объекта: ");
        byte kop1 = ReadByte("Введите копейки для первого объекта: ");
        Money money1 = new Money(rub1, kop1);
        
        uint rub2 = ReadUInt("Введите рубли для второго объекта: ");
        byte kop2 = ReadByte("Введите копейки для второго объекта: ");
        Money money2 = new Money(rub2, kop2);
        
        uint rub3 = ReadUInt("Введите рубли для третьего объекта: ");
        byte kop3 = ReadByte("Введите копейки для третьего объекта: ");
        Money money3 = new Money(rub3, kop3);

        Console.WriteLine($"\nmoney1 = {money1}");
        Console.WriteLine($"money2 = {money2}");
        Console.WriteLine($"money3 = {money3}");

        Console.WriteLine("\nТестирование вычитания (метод Subtract):");
        Money subResult = money1.Subtract(money2);
        Console.WriteLine($"{money1} - {money2} = {subResult}");

        Console.WriteLine("\nТестирование перегруженного оператора - :");
        Money opSubResult = money1 - money2;
        Console.WriteLine($"{money1} - {money2} = {opSubResult}");

        Console.WriteLine("\nТестирование унарных операторов ++ и -- :");
        Money temp = new Money(money1.Rubles, money1.Kopeks);
        Console.WriteLine($"{temp}++ = {temp++}");
        temp = new Money(money1.Rubles, money1.Kopeks);
        Console.WriteLine($"++{temp} = {++temp}");
        
        temp = new Money(money1.Rubles, money1.Kopeks);
        Console.WriteLine($"{temp}-- = {temp--}");
        temp = new Money(money1.Rubles, money1.Kopeks);
        Console.WriteLine($"--{temp} = {--temp}");

        Console.WriteLine("\nТестирование операций приведения типа:");
        uint rublesPart = money1;
        double kopeksPart = (double)money1;
        Console.WriteLine($"{money1} -> uint (рубли): {rublesPart}");
        Console.WriteLine($"{money1} -> double (копейки в рублях): {kopeksPart:F2}");

        Console.WriteLine("\nТестирование бинарных операций с uint:");
        uint number = ReadUInt("Введите целое число: ");
        Money binResult1 = money1 - number;
        Money binResult2 = number - money1;
        Console.WriteLine($"{money1} - {number} = {binResult1}");
        Console.WriteLine($"{number} - {money1} = {binResult2}");

        Console.WriteLine("\nТестирование бинарной операции Money - Money:");
        Money moneyResult = money1 - money2;
        Console.WriteLine($"{money1} - {money2} = {moneyResult}");

        Console.WriteLine("\nТестирование с отрицательными результатами:");
        Money smallMoney = new Money(1, 0);
        Money bigMoney = new Money(2, 50);
        Console.WriteLine($"{smallMoney} - {bigMoney} = {smallMoney - bigMoney}");
        Console.WriteLine($"{smallMoney} - 10 = {smallMoney - 10}");
        Console.WriteLine($"10 - {smallMoney} = {10 - smallMoney}");
        Console.WriteLine($"{smallMoney}-- = {smallMoney--}");
    }
}