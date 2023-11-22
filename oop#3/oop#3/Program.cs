using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace oop_3;

public enum University { Subdivisions, Centers, Faculty }


public class Chair
{
    public string ChairName { get; set; }
    public string StudyPrograms { get; set; }
    public DateTime CreationDate { get; set; }
    public University University { get; set; }
    public Employees[] Employees { get; set; }


    public Chair(string chairName, string studyPrograms, DateTime creationDate, University university, Employees[] employees)
    {
        ChairName = chairName;
        StudyPrograms = studyPrograms;
        CreationDate = creationDate;
        University = university;
        Employees = employees;

    }

    public Chair()
    {
        ChairName = "Default Chair";
        StudyPrograms = "Default Programs";
        CreationDate = DateTime.Now;
    }


    public virtual string ShowName()
    {
        return $"Chair name: {ChairName}";
    }


}

public class OfficeChair : Chair
{
    public override string ShowName()
    {
        return $"Chair name: {ChairName}";
    }
}

    public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }



    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public Person()
    {
        FirstName = "Buffalo";
        LastName = "Bill";
    }

    public override string ToString()
    {
        return $"FirstName: {FirstName}, LastName: {LastName}";
    }
}


public class Employees
{
    private Person person;
    private University university;
    public int age;
    public Chair[] chairs;


    public Employees(Person person, University university, int age, params Chair[] newChairs)
    {
        this.person = person;
        this.university = university;
        this.age = age;
        this.chairs = newChairs;
    }

    private Employees()
    {
        person = new Person();
        university = University.Faculty;
        age = 0;
        chairs = new Chair[0];
    }



    // get set 

    public Person Person
    {
        get { return person; }
        set { person = value; }
    }

    public University University
    {
        get { return university; }
        set { university = value; }
    }

    public int Age
    {
        get { return age; }
        set { age = value; }
    }

    public Chair[] Chairs
    {
        get { return chairs; }
        set { chairs = value; }
    }
    // -----


    public bool this[University university]
    {
        get
        {
            foreach (Chair chair in chairs)
            {
                if (chair.University == university) return true;
            }
            return false;
        }
    }


    public void AddChairs(params Chair[] newChairs)
    {
        if (newChairs != null)
            chairs = chairs.Concat(newChairs).ToArray();

    }


    public override string ToString()
    {
        StringBuilder strBldr = new StringBuilder();
        strBldr.Append("Person: \n").Append(person.ToString()).Append(Environment.NewLine);
        strBldr.Append("University: ").Append(university).Append(Environment.NewLine);
        strBldr.Append("Age: ").Append(age).Append(Environment.NewLine);
        strBldr.Append("Chairs:").Append(Environment.NewLine);
        foreach (Chair chair in chairs)
        {
            strBldr.Append(chair.ToString()).Append(Environment.NewLine);
        }
        return strBldr.ToString();
    }


    public virtual string ToShortString() // TODO Debug
    {
        return $"Person: {person.ToString()}, University: {university},  Average Age: {CalculateAverageAge()}";
    }


    public double CalculateAverageAge()
    {
        if (chairs.Length == 0)
        {
            return 0.0;
        }

        double totalAge = 0;
        int employeeCount = 0;

        foreach (Chair chair in chairs)
        {
            if (chair.Employees != null)
            {
                foreach (Employees employee in chair.Employees)
                {
                    totalAge += employee.Age;
                    employeeCount++;
                }
            }
        }

        return employeeCount > 0 ? totalAge / employeeCount : 0.0;
    }


    public virtual string ShowName()
    {
        if (chairs.Length > 0)
        {
            return $"Employee: {person.FirstName} {person.LastName}, Chairs: {string.Join(", ", chairs.Select(c => c.ChairName))}";
        }
        else
        {
            return $"Employee: {person.FirstName} {person.LastName}, No chairs assigned";
        }
    }

}


internal class Program
{
    static void Main(string[] args)
    {
        /*    Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine("// --------1-------- //");
            Chair chair1 = new Chair("Mathematics", "Math Programs", new DateTime(1999, 09, 1), University.Faculty, new Employees[0]);
            Chair chair2 = new Chair("Physics", "Physics Programs", new DateTime(2011, 10, 7), University.Subdivisions, new Employees[0]);

            Employees employees1 = new Employees(new Person("John", "Doe"), University.Faculty, 30, chair1, chair2);

            string shortString1 = employees1.ToShortString();


            Chair chair11 = new Chair("Math", "Math Programs", new DateTime(1987, 09, 1), University.Faculty, new Employees[0]);
            Chair chair22 = new Chair("Softwere", "Physics Programs", new DateTime(2021, 10, 7), University.Subdivisions, new Employees[0]);

            Employees employees22 = new Employees(new Person("Luis", "Kerol"), University.Faculty, 30, chair1, chair2);

            string shortString22 = employees22.ToShortString();
            Console.WriteLine(shortString1);
            Console.WriteLine(shortString22);

            Console.WriteLine();
            Console.WriteLine();

            // _______________________________________ //

            Console.WriteLine("// --------2-------- //");

            Employees employees2 = new Employees(new Person("John", "Doe"), University.Faculty, 30);

            Chair chair3 = new Chair("Chemistry", "Chemistry Programs", DateTime.Now, University.Centers, new Employees[0]);
            Chair chair4 = new Chair("Mathematics", "Math Programs", DateTime.Now, University.Faculty, new Employees[0]);
            Chair chair5 = new Chair("Physics", "Physics Programs", DateTime.Now, University.Subdivisions, new Employees[0]);

            employees2.AddChairs( chair3, chair4, chair5);


            University[] universitiesToCheck = { University.Subdivisions, University.Centers, University.Faculty };

            foreach (University university in universitiesToCheck)
            {
                bool hasChair = employees2[university];
                Console.WriteLine($"University {university}: {hasChair}");
            }

            Console.WriteLine();
            Console.WriteLine();

            // ______________________________________ //

            Console.WriteLine("// --------3-------- //");

            Employees employees3 = new Employees(new Person("Ivan", "Shysen"), University.Faculty, 40); 

            Chair chair6 = new Chair("Mathematics", "Math Programs",new DateTime(1984, 8 ,14), University.Faculty, new Employees[0]);
            Chair chair7 = new Chair("Physics", "Physics Programs", new DateTime(1962, 11, 30), University.Subdivisions, new Employees[0]);

            employees3.AddChairs(chair6, chair7);

            string employeeInfo = employees3.ToString();
            Console.WriteLine(employeeInfo);

            Console.WriteLine();
            Console.WriteLine();

            // ______________________________________ //

            Console.WriteLine("// --------4-------- //");
            Employees employees4 = new Employees(new Person("Victor", "Tak"), University.Centers, 40);

            Chair chair8 = new Chair("Mathematics", "Math Programs", new DateTime(1984, 8, 14), University.Faculty, new Employees[0]);
            Chair chair9 = new Chair("Physics", "Physics Programs", new DateTime(1962, 11, 30), University.Subdivisions, new Employees[0]);

            employees4.AddChairs(chair8, chair9);

            string empInfo = employees4.ToString();
            Console.WriteLine(empInfo);

            Console.WriteLine();
            Console.WriteLine();
            // ______________________________________ //

            //Console.WriteLine("// --------5-------- //");

            //int size = 1000; // Розмір масиву (змініть на бажаний розмір)
            //
            //// Створення одновимірного масиву Chair
            //Chair[] oneDimensionalArray = new Chair[size];
            //FillArray(oneDimensionalArray);
            //
            //// Створення двовимірного прямокутного масиву Chair
            //Chair[,] twoDimensionalRectangularArray = new Chair[size, size];
            //FillArray(twoDimensionalRectangularArray);
            //
            //// Створення двовимірного ступінчастого масиву Chair
            //Chair[][] jaggedArray = new Chair[size][];
            //for (int i = 0; i < size; i++)
            //{
            //    jaggedArray[i] = new Chair[size];
            //    FillArray(jaggedArray[i]);
            //}
            //
            //// Порівняння часу виконання операцій з масивами
            //MeasureTime(() => AccessElements(oneDimensionalArray), "One-Dimensional Array");
            //MeasureTime(() => AccessElements(twoDimensionalRectangularArray), "Two-Dimensional Rectangular Array");
            //MeasureTime(() => AccessElements(jaggedArray), "Jagged Array");
        }

        /*static void FillArray(Chair[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new Chair("ChairName", "StudyPrograms", DateTime.Now, University.Faculty, new Employees[0]);
            }
        }

        static void FillArray(Chair[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = new Chair("ChairName", "StudyPrograms", DateTime.Now, University.Faculty, new Employees[0]);
                }
            }
        }

        static void FillArray(Chair[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new Chair[array.Length];
                for (int j = 0; j < array[i].Length; j++)
                {
                    array[i][j] = new Chair("ChairName", "StudyPrograms", DateTime.Now, University.Faculty, new Employees[0]);
                }
            }
        }

        // Метод для доступу до елементів масиву
        static void AccessElements(Chair[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Chair chair = array[i];
            }
        }

        static void AccessElements(Chair[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Chair chair = array[i, j];
                }
            }
        }

        static void AccessElements(Chair[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    Chair chair = array[i][j];
                }
            }
        }

        // Метод для вимірювання часу виконання певної операції
        static void MeasureTime(Action action, string arrayType)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            Console.WriteLine($"{arrayType} Execution Time: {stopwatch.ElapsedMilliseconds} ms");
        }*/


        Chair chair1 = new Chair("Mathematics", "Math Programs", new DateTime(1999, 09, 1), University.Faculty, new Employees[0]);
        OfficeChair officeChair1 = new OfficeChair { ChairName = "Executive Chair" };
        Employees employees1 = new Employees(new Person("Andrew", "Moldavchuk"), University.Faculty, 30, chair1);

        Console.WriteLine(chair1.ShowName());         
        Console.WriteLine(officeChair1.ShowName());     
        Console.WriteLine(employees1.ShowName());
    }
}