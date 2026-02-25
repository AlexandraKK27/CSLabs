using System;
using System.Collections.Generic;

public class Name
{
    private string surname;
    private string firstName;
    private string patronymic;

    public string Surname
    {
        get { return surname; }
        set { surname = value; }
    }

    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }

    public string Patronymic
    {
        get { return patronymic; }
        set { patronymic = value; }
    }

    public Name(string firstName)
    {
        this.firstName = firstName;
        this.surname = null;
        this.patronymic = null;
    }

    public Name(string firstName, string surname)
    {
        this.firstName = firstName;
        this.surname = surname;
        this.patronymic = null;
    }

    public Name(string firstName, string surname, string patronymic)
    {
        this.firstName = firstName;
        this.surname = surname;
        this.patronymic = patronymic;
    }

    public override string ToString()
    {
        string result = "";
        if (!string.IsNullOrEmpty(surname))
            result += surname + " ";
        if (!string.IsNullOrEmpty(firstName))
            result += firstName + " ";
        if (!string.IsNullOrEmpty(patronymic))
            result += patronymic;
        return result.Trim();
    }
}

public class House
{
    private int floors;

    public int Floors
    {
        get { return floors; }
        set { floors = value; }
    }

    public House(int floors)
    {
        this.floors = floors;
    }

    public override string ToString()
    {
        int lastDigit = floors % 10;
        int lastTwoDigits = floors % 100;

        if (lastTwoDigits >= 11 && lastTwoDigits <= 14)
            return $"дом с {floors} этажей";
        if (lastDigit == 1)
            return $"дом с {floors} этажом";
        if (lastDigit >= 2 && lastDigit <= 4)
            return $"дом с {floors} этажами";
        return $"дом с {floors} этажей";
    }
}

public class Department
{
    private string name;
    private Employee chief;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public Employee Chief
    {
        get { return chief; }
        set { chief = value; }
    }

    public Department(string name, Employee chief)
    {
        this.name = name;
        this.chief = chief;
    }

    public Department(string name)
    {
        this.name = name;
        this.chief = null;
    }
}

public class Employee
{
    private string name;
    private Department department;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public Department Department
    {
        get { return department; }
        set { department = value; }
    }

    public Employee(string name, Department department)
    {
        this.name = name;
        this.department = department;
    }

    public override string ToString()
    {
        if (department != null && department.Chief == this)
            return $"{name} начальник отдела {department.Name}";
        if (department != null && department.Chief != null)
            return $"{name} работает в отделе {department.Name}, начальник которого {department.Chief.Name}";
        return name;
    }
}

public class EmployeeWithList
{
    private string name;
    private DepartmentWithList department;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public DepartmentWithList Department
    {
        get { return department; }
        set { department = value; }
    }

    public EmployeeWithList(string name, DepartmentWithList department)
    {
        this.name = name;
        this.department = department;
        if (department != null)
            department.AddEmployee(this);
    }

    public List<EmployeeWithList> GetDepartmentEmployees()
    {
        if (department != null)
            return department.GetEmployees();
        return new List<EmployeeWithList>();
    }

    public override string ToString()
    {
        if (department != null && department.Chief == this)
            return $"{name} начальник отдела {department.Name}";
        if (department != null && department.Chief != null)
            return $"{name} работает в отделе {department.Name}, начальник которого {department.Chief.Name}";
        return name;
    }
}

public class DepartmentWithList
{
    private string name;
    private EmployeeWithList chief;
    private List<EmployeeWithList> employees;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public EmployeeWithList Chief
    {
        get { return chief; }
        set { chief = value; }
    }

    public DepartmentWithList(string name, EmployeeWithList chief)
    {
        this.name = name;
        this.chief = chief;
        this.employees = new List<EmployeeWithList>();
    }

    public DepartmentWithList(string name)
    {
        this.name = name;
        this.chief = null;
        this.employees = new List<EmployeeWithList>();
    }

    public void AddEmployee(EmployeeWithList emp)
    {
        if (!employees.Contains(emp))
            employees.Add(emp);
    }

    public List<EmployeeWithList> GetEmployees()
    {
        return new List<EmployeeWithList>(employees);
    }
}

public class Fraction
{
    private int numerator;
    private int denominator;

    public int Numerator
    {
        get { return numerator; }
        set { numerator = value; }
    }

    public int Denominator
    {
        get { return denominator; }
        set 
        { 
            if (value != 0)
                denominator = value; 
        }
    }

    public Fraction(int numerator, int denominator)
    {
        this.numerator = numerator;
        this.denominator = denominator != 0 ? denominator : 1;
    }

    public Fraction Sum(Fraction other)
    {
        int newNumerator = this.numerator * other.denominator + other.numerator * this.denominator;
        int newDenominator = this.denominator * other.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    public Fraction Sum(int number)
    {
        return Sum(new Fraction(number, 1));
    }

    public Fraction Minus(Fraction other)
    {
        int newNumerator = this.numerator * other.denominator - other.numerator * this.denominator;
        int newDenominator = this.denominator * other.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    public Fraction Minus(int number)
    {
        return Minus(new Fraction(number, 1));
    }

    public Fraction Multiply(Fraction other)
    {
        int newNumerator = this.numerator * other.numerator;
        int newDenominator = this.denominator * other.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    public Fraction Multiply(int number)
    {
        return Multiply(new Fraction(number, 1));
    }

    public Fraction Divide(Fraction other)
    {
        int newNumerator = this.numerator * other.denominator;
        int newDenominator = this.denominator * other.numerator;
        return new Fraction(newNumerator, newDenominator);
    }

    public Fraction Divide(int number)
    {
        return Divide(new Fraction(number, 1));
    }

    public override string ToString()
    {
        return $"{numerator}/{denominator}";
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
        Console.WriteLine("Задание 1: Имена");
        Name name1 = new Name("Клеопатра");
        Name name2 = new Name("Александр", "Пушкин", "Сергеевич");
        Name name3 = new Name("Владимир", "Маяковский");
        
        Console.WriteLine(name1);
        Console.WriteLine(name2);
        Console.WriteLine(name3);
        
        Console.WriteLine("\nЗадание 1: Дом");
        House house1 = new House(1);
        House house2 = new House(5);
        House house3 = new House(23);
        
        Console.WriteLine(house1);
        Console.WriteLine(house2);
        Console.WriteLine(house3);
        
        Console.WriteLine("\nЗадание 2: Сотрудники и отделы");
        Department itDepartment = new Department("IT");
        
        Employee petrov = new Employee("Петров", itDepartment);
        Employee kozlov = new Employee("Козлов", itDepartment);
        Employee sidorov = new Employee("Сидоров", itDepartment);
        
        itDepartment.Chief = kozlov;
        
        Console.WriteLine(petrov);
        Console.WriteLine(kozlov);
        Console.WriteLine(sidorov);
        
        Console.WriteLine("\nЗадание 3: Сотрудники и отделы со списком");
        DepartmentWithList itDepartmentWithList = new DepartmentWithList("IT");
        
        EmployeeWithList petrovWithList = new EmployeeWithList("Петров", itDepartmentWithList);
        EmployeeWithList kozlovWithList = new EmployeeWithList("Козлов", itDepartmentWithList);
        EmployeeWithList sidorovWithList = new EmployeeWithList("Сидоров", itDepartmentWithList);
        
        itDepartmentWithList.Chief = kozlovWithList;
        
        Console.WriteLine(petrovWithList);
        Console.WriteLine(kozlovWithList);
        Console.WriteLine(sidorovWithList);
        
        Console.WriteLine("\nСотрудники отдела IT:");
        List<EmployeeWithList> employees = kozlovWithList.GetDepartmentEmployees();
        foreach (var emp in employees)
        {
            Console.WriteLine($"- {emp.Name}");
        }
        
        Console.WriteLine("\nЗадание 4: Создаем Имена (новая версия)");
        Name newName1 = new Name("Клеопатра");
        Name newName2 = new Name("Александр", "Пушкин", "Сергеевич");
        Name newName3 = new Name("Владимир", "Маяковский");
        Name newName4 = new Name("Христофор", "Бонифатьевич");
        
        Console.WriteLine($"1. {newName1}");
        Console.WriteLine($"2. {newName2}");
        Console.WriteLine($"3. {newName3}");
        Console.WriteLine($"4. {newName4}");
        
        Console.WriteLine("\nЗадание 5: Дроби");
        Fraction f1 = new Fraction(1, 2);
        Fraction f2 = new Fraction(2, 3);
        Fraction f3 = new Fraction(3, 4);
        
        Console.WriteLine($"f1 = {f1}");
        Console.WriteLine($"f2 = {f2}");
        Console.WriteLine($"f3 = {f3}");
        
        Console.WriteLine($"\n{f1} + {f2} = {f1.Sum(f2)}");
        Console.WriteLine($"{f1} - {f2} = {f1.Minus(f2)}");
        Console.WriteLine($"{f1} * {f2} = {f1.Multiply(f2)}");
        Console.WriteLine($"{f1} / {f2} = {f1.Divide(f2)}");
        
        Console.WriteLine($"\n{f1} + 5 = {f1.Sum(5)}");
        Console.WriteLine($"{f1} - 3 = {f1.Minus(3)}");
        Console.WriteLine($"{f1} * 4 = {f1.Multiply(4)}");
        Console.WriteLine($"{f1} / 2 = {f1.Divide(2)}");
        
        Console.WriteLine($"\n{f1} * {f2} = {f1.Multiply(f2)}");
        
        Console.WriteLine("\nЗадание 5: Сложное выражение");
        Fraction result = f1.Sum(f2).Divide(f3).Minus(5);
        Console.WriteLine($"({f1} + {f2}) / {f3} - 5 = {result}");
        
        Console.WriteLine("\nДемонстрация всех созданных объектов");
        Console.WriteLine("Имена:");
        Console.WriteLine($"- {name1}");
        Console.WriteLine($"- {name2}");
        Console.WriteLine($"- {name3}");
        
        Console.WriteLine("\nДома:");
        Console.WriteLine($"- {house1}");
        Console.WriteLine($"- {house2}");
        Console.WriteLine($"- {house3}");
        
        Console.WriteLine("\nСотрудники (задание 2):");
        Console.WriteLine($"- {petrov}");
        Console.WriteLine($"- {kozlov}");
        Console.WriteLine($"- {sidorov}");
        
        Console.WriteLine("\nСотрудники со списком (задание 3):");
        Console.WriteLine($"- {petrovWithList}");
        Console.WriteLine($"- {kozlovWithList}");
        Console.WriteLine($"- {sidorovWithList}");
        
        Console.WriteLine("\nНовые имена (задание 4):");
        Console.WriteLine($"- {newName1}");
        Console.WriteLine($"- {newName2}");
        Console.WriteLine($"- {newName3}");
        Console.WriteLine($"- {newName4}");
        
        Console.WriteLine("\nДроби (задание 5):");
        Console.WriteLine($"- {f1}");
        Console.WriteLine($"- {f2}");
        Console.WriteLine($"- {f3}");
    }
}