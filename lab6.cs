using System;
using System.Collections;

namespace LaboratoryWork
{
    public interface IMeowable
    {
        void Meow();
    }

    public class Cat : IMeowable
    {
        private string _name;
        private int _meowCount;

        public Cat(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя кота не может быть пустым");
            _name = name;
            _meowCount = 0;
        }

        public override string ToString()
        {
            return $"кот: {_name}";
        }

        public void Meow()
        {
            _meowCount++;
            Console.WriteLine($"{_name}: мяу!");
        }

        public void Meow(int n)
        {
            if (n <= 0)
                throw new ArgumentException("Количество мяуканий должно быть положительным");
            
            _meowCount += n;
            Console.Write($"{_name}: ");
            for (int i = 0; i < n; i++)
            {
                Console.Write("мяу");
                if (i < n - 1)
                    Console.Write("-");
            }
            Console.WriteLine("!");
        }

        public int GetMeowCount()
        {
            return _meowCount;
        }
    }

    public class Dog : IMeowable
    {
        private string _name;

        public Dog(string name)
        {
            _name = name;
        }

        public void Meow()
        {
            Console.WriteLine($"{_name}: гав?");
        }
    }

    public static class MeowableExtensions
    {
        public static void MakeMeowAll(this IEnumerable meowables)
        {
            foreach (object obj in meowables)
            {
                if (obj is IMeowable meowable)
                {
                    meowable.Meow();
                }
            }
        }
    }

    public class Fraction : ICloneable, IFractionOperations
    {
        private int _numerator;
        private int _denominator;
        private double? _cachedValue;

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Знаменатель не может быть равен нулю");
            
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }
            
            _numerator = numerator;
            _denominator = denominator;
            _cachedValue = null;
        }

        public int Numerator => _numerator;
        public int Denominator => _denominator;

        public override string ToString()
        {
            return $"{_numerator}/{_denominator}";
        }

        public Fraction Add(Fraction other)
        {
            int newNumerator = this._numerator * other._denominator + other._numerator * this._denominator;
            int newDenominator = this._denominator * other._denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        public Fraction Add(int number)
        {
            return Add(new Fraction(number, 1));
        }

        public Fraction Subtract(Fraction other)
        {
            int newNumerator = this._numerator * other._denominator - other._numerator * this._denominator;
            int newDenominator = this._denominator * other._denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        public Fraction Subtract(int number)
        {
            return Subtract(new Fraction(number, 1));
        }

        public Fraction Multiply(Fraction other)
        {
            int newNumerator = this._numerator * other._numerator;
            int newDenominator = this._denominator * other._denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        public Fraction Multiply(int number)
        {
            return Multiply(new Fraction(number, 1));
        }

        public Fraction Divide(Fraction other)
        {
            if (other._numerator == 0)
                throw new DivideByZeroException("Деление на ноль");
            
            int newNumerator = this._numerator * other._denominator;
            int newDenominator = this._denominator * other._numerator;
            return new Fraction(newNumerator, newDenominator);
        }

        public Fraction Divide(int number)
        {
            return Divide(new Fraction(number, 1));
        }

        public override bool Equals(object obj)
        {
            if (obj is Fraction other)
            {
                return this._numerator == other._numerator && this._denominator == other._denominator;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_numerator, _denominator);
        }

        public object Clone()
        {
            return new Fraction(this._numerator, this._denominator);
        }

        public double GetDoubleValue()
        {
            if (!_cachedValue.HasValue)
            {
                _cachedValue = (double)_numerator / _denominator;
            }
            return _cachedValue.Value;
        }

        public void SetNumerator(int numerator)
        {
            _numerator = numerator;
            _cachedValue = null;
        }

        public void SetDenominator(int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Знаменатель не может быть равен нулю");
            
            if (denominator < 0)
            {
                _numerator = -_numerator;
                _denominator = -denominator;
            }
            else
            {
                _denominator = denominator;
            }
            _cachedValue = null;
        }
    }

    public interface IFractionOperations
    {
        double GetDoubleValue();
        void SetNumerator(int numerator);
        void SetDenominator(int denominator);
    }

    class Program
    {
        static void MakeMeow(IMeowable meowable)
        {
            meowable.Meow();
        }

        static int ReadInt(string prompt)
        {
            int value;
            do
            {
                Console.Write(prompt);
            } while (!int.TryParse(Console.ReadLine(), out value));
            return value;
        }

        static string ReadString(string prompt)
        {
            string value;
            do
            {
                Console.Write(prompt);
                value = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Ошибка: значение не может быть пустым");
                }
            } while (string.IsNullOrWhiteSpace(value));
            return value;
        }

        static int ReadPositiveInt(string prompt)
        {
            int value;
            do
            {
                value = ReadInt(prompt);
                if (value <= 0)
                {
                    Console.WriteLine("Ошибка: число должно быть положительным");
                }
            } while (value <= 0);
            return value;
        }

        static int ReadNonZeroInt(string prompt)
        {
            int value;
            do
            {
                value = ReadInt(prompt);
                if (value == 0)
                {
                    Console.WriteLine("Ошибка: число не может быть равно нулю");
                }
            } while (value == 0);
            return value;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Задание 1. Кот\n");
            
            string catName = ReadString("Введите имя кота: ");
            Cat userCat = new Cat(catName);
            Console.WriteLine($"Создан {userCat}");
            
            Console.WriteLine("\nМяуканье 1 раз:");
            userCat.Meow();
            
            int meowCount = ReadPositiveInt("Сколько раз помяукать? ");
            Console.WriteLine($"\nМяуканье {meowCount} раз(а):");
            userCat.Meow(meowCount);
            
            Console.WriteLine("\nИнтерфейс Мяуканье\n");
            
            Console.WriteLine("И еще несколько животных:");
            string cat2Name = ReadString("Введите имя второго кота: ");
            Cat cat2 = new Cat(cat2Name);
            
            string cat3Name = ReadString("Введите имя третьего кота: ");
            Cat cat3 = new Cat(cat3Name);
            
            string dogName = ReadString("Введите имя собаки: ");
            Dog dog = new Dog(dogName);
            
            Console.WriteLine("\nВсе животные мяукают:");
            IMeowable[] meowables = { userCat, cat2, cat3, dog };
            MeowableExtensions.MakeMeowAll(meowables);
            
            Console.WriteLine("\nКоличество мяуканий\n");
            
            Console.WriteLine($"Кот {userCat}");
            Console.WriteLine($"Мяуканий до вызова: {userCat.GetMeowCount()}");
            
            int extraMeows = ReadPositiveInt("Сколько раз ещё помяукать? ");
            for (int i = 0; i < extraMeows; i++)
            {
                MakeMeow(userCat);
            }
            
            Console.WriteLine($"Мяуканий после вызова: {userCat.GetMeowCount()}");
            
            Console.WriteLine("\nЗадание 2. Дроби\n");
            
            Console.WriteLine("Создадим три дроби:");
            
            Console.WriteLine("\nДробь 1:");
            int num1 = ReadInt("Введите числитель: ");
            int den1 = ReadNonZeroInt("Введите знаменатель (не равен 0): ");
            Fraction f1 = new Fraction(num1, den1);
            
            Console.WriteLine("\nДробь 2:");
            int num2 = ReadInt("Введите числитель: ");
            int den2 = ReadNonZeroInt("Введите знаменатель (не равен 0): ");
            Fraction f2 = new Fraction(num2, den2);
            
            Console.WriteLine("\nДробь 3:");
            int num3 = ReadInt("Введите числитель: ");
            int den3 = ReadNonZeroInt("Введите знаменатель (не равен 0): ");
            Fraction f3 = new Fraction(num3, den3);
            
            Console.WriteLine($"\nf1 = {f1}");
            Console.WriteLine($"f2 = {f2}");
            Console.WriteLine($"f3 = {f3}");
            
            Console.WriteLine($"\n{f1} + {f2} = {f1.Add(f2)}");
            Console.WriteLine($"{f1} - {f2} = {f1.Subtract(f2)}");
            Console.WriteLine($"{f1} * {f2} = {f1.Multiply(f2)}");
            Console.WriteLine($"{f1} / {f2} = {f1.Divide(f2)}");
            
            int number = ReadInt("\nВведите целое число для операций с дробью: ");
            Console.WriteLine($"{f1} + {number} = {f1.Add(number)}");
            Console.WriteLine($"{f1} - {number} = {f1.Subtract(number)}");
            Console.WriteLine($"{f1} * {number} = {f1.Multiply(number)}");
            Console.WriteLine($"{f1} / {number} = {f1.Divide(number)}");
            
            Fraction result = f1.Add(f2).Divide(f3).Subtract(5);
            Console.WriteLine($"\nf1.sum(f2).div(f3).minus(5) = {result}");
            
            Console.WriteLine($"\n{f1} equals {new Fraction(num1, den1)}: {f1.Equals(new Fraction(num1, den1))}");
            Console.WriteLine($"{f1} equals {f2}: {f1.Equals(f2)}");
            
            Fraction f1Copy = (Fraction)f1.Clone();
            Console.WriteLine($"\nОригинал: {f1}, Копия: {f1Copy}");
            Console.WriteLine($"Оригинал equals Копия: {f1.Equals(f1Copy)}");
            
            Console.WriteLine($"\nВещественное значение {f1}: {f1.GetDoubleValue():F3}");
            
            int newNum = ReadInt("Введите новый числитель: ");
            f1.SetNumerator(newNum);
            
            int newDen = ReadNonZeroInt("Введите новый знаменатель: ");
            f1.SetDenominator(newDen);
            
            Console.WriteLine($"Новое вещественное значение: {f1.GetDoubleValue():F3}");
        }
    }
}