using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

// Single-file demo implementing concise examples for Exercises 1-30.
// Build & run: open terminal in this folder and run `dotnet run` (requires .NET SDK installed).

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        Console.WriteLine("C# & ADO.NET Exercises — demo run");
        Console.WriteLine("Select example to run (1-30), or 0 to run a quick demo of multiple examples:");
        Console.WriteLine("0=quick, 1=Setup, 2=ValueVsReference, 3=PrimaryConstructors, ... 30=ADO.NET (template)");
        Console.Write("Choice: ");
        if (!int.TryParse(Console.ReadLine(), out var choice)) choice = 0;

        switch (choice)
        {
            case 0: await QuickDemo(); break;
            case 1: Exercise1_Setup(); break;
            case 2: Exercise2_ValueVsReference(); break;
            case 3: Exercise3_PrimaryConstructors(); break;
            case 4: Exercise4_TypeInference(); break;
            case 5: Exercise5_GradeCalculation(); break;
            case 6: Exercise6_Loops(); break;
            case 7: Exercise7_MethodOverloading(); break;
            case 8: Exercise8_RefOutIn(); break;
            case 9: Exercise9_LocalFunctions(); break;
            case 10: Exercise10_Constructors(); break;
            case 11: Exercise11_AccessModifiers(); break;
            case 12: Exercise12_AutoProps(); break;
            case 13: Exercise13_Records(); break;
            case 14: Exercise14_Inheritance(); break;
            case 15: Exercise15_AbstractInterface(); break;
            case 16: Exercise16_Nullables(); break;
            case 17: Exercise17_NullConditional(); break;
            case 18: Exercise18_RequiredModifier(); break;
            case 19: Exercise19_ListsAndDictionaries(); break;
            case 20: Exercise20_LINQ(); break;
            case 21: Exercise21_PatternMatching(); break;
            case 22: Exercise22_Tuples(); break;
            case 23: await Exercise23_AsyncUpload(); break;
            case 24: Exercise24_JSON(); break;
            case 25: Exercise25_FileStreams(); break;
            case 26: Exercise26_RaceCondition(); break;
            case 27: Exercise27_DeadlockDemo(); break;
            case 28: Exercise28_TraceLogging(); break;
            case 29: Exercise29_SanitizeInput(); break;
            case 30: Exercise30_ADONET_Template(); break;
            default: Console.WriteLine("Not implemented"); break;
        }

        Console.WriteLine("Done. Press Enter to exit.");
        Console.ReadLine();
        return 0;
    }

    static Task QuickDemo()
    {
        Console.WriteLine("Running quick demo: Value vs Reference and LINQ examples...");
        Exercise2_ValueVsReference();
        Exercise20_LINQ();
        return Task.CompletedTask;
    }

    // 1. Setup (instructions only): Validate by printing Hello World
    static void Exercise1_Setup()
    {
        Console.WriteLine("Hello World — environment setup validated.");
    }

    // 2. Value vs Reference
    static void Exercise2_ValueVsReference()
    {
        int a = 5;
        var s = "hello";
        var obj = new SampleClass { Value = 10 };
        Console.WriteLine($"Before: a={a}, s={s}, obj.Value={obj.Value}");
        ModifyValue(a);
        ModifyString(s);
        ModifyObject(obj);
        Console.WriteLine($"After: a={a}, s={s}, obj.Value={obj.Value}");

        static void ModifyValue(int x) { x = 99; }
        static void ModifyString(string t) { t = t + " world"; }
        static void ModifyObject(SampleClass o) { o.Value = 42; }
    }

    class SampleClass { public int Value { get; set; } }

    // 3. Primary constructors (C# 12 style is preview; emulate concise class)
    static void Exercise3_PrimaryConstructors()
    {
        var p = new Person("Sam", 29);
        p.Print();
    }

    record Person(string Name, int Age)
    {
        public void Print() => Console.WriteLine($"Person: {Name}, {Age}");
    }

    // 4. Type inference
    static void Exercise4_TypeInference()
    {
        var x = 5; // int
        var txt = "abc"; // string
        var c = new SampleClass(); // SampleClass
        Console.WriteLine($"x ({x.GetType()}), txt ({txt.GetType()}), c ({c.GetType()})");
    }

    // 5. Grade calculation with pattern matching
    static void Exercise5_GradeCalculation()
    {
        Console.Write("Enter score (0-100): ");
        if (!int.TryParse(Console.ReadLine(), out var score)) score = 0;
        string grade;
        if (score >= 90) grade = "A";
        else if (score >= 80) grade = "B";
        else if (score >= 70) grade = "C";
        else if (score >= 60) grade = "D";
        else grade = "F";
        Console.WriteLine($"If/else grade: {grade}");

        // switch with pattern matching
        var switchGrade = score switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
        };
        Console.WriteLine($"Switch grade: {switchGrade}");
    }

    // 6. Loops
    static void Exercise6_Loops()
    {
        int[] arr = { 1, 2, 3, 4, 5 };
        Console.WriteLine("for:"); for (int i = 0; i < arr.Length; i++) { Console.WriteLine(arr[i]); }
        Console.WriteLine("foreach:"); foreach (var v in arr) Console.WriteLine(v);
        Console.WriteLine("while:"); int j = 0; while (j < arr.Length) { Console.WriteLine(arr[j]); j++; }
        Console.WriteLine("do-while:"); int k = 0; do { Console.WriteLine(arr[k]); k++; } while (k < arr.Length);
    }

    // 7. Method overloading
    static void Exercise7_MethodOverloading()
    {
        Console.WriteLine(CalculateTotal(2, 3));
        Console.WriteLine(CalculateTotal(1.2, 3.4, 4.4));
        static int CalculateTotal(int a, int b) => a + b;
        static double CalculateTotal(double a, double b, double c) => a + b + c;
    }

    // 8. ref/out/in
    static void Exercise8_RefOutIn()
    {
        int a = 1; Console.WriteLine($"a before: {a}"); RefExample(ref a); Console.WriteLine($"a after ref: {a}");
        OutExample(out int b); Console.WriteLine($"b from out: {b}");
        void RefExample(ref int x) { x = 5; }
        void OutExample(out int x) { x = 10; }
    }

    // 9. Local functions (factorial)
    static void Exercise9_LocalFunctions()
    {
        Console.WriteLine(CalculateFactorial(5));
        static long CalculateFactorial(int n)
        {
            long Inner(int k) => k <= 1 ? 1 : k * Inner(k - 1);
            return Inner(n);
        }
    }

    // 10. Constructors
    static void Exercise10_Constructors()
    {
        var c1 = new Car();
        var c2 = new Car("Toyota", "Corolla", 2020);
        Console.WriteLine(c1); Console.WriteLine(c2);
    }

    class Car { public string Make { get; set; } = "Unknown"; public string Model { get; set; } = "Unknown"; public int Year { get; set; } = 0; public Car() { } public Car(string make, string model, int year) { Make = make; Model = model; Year = year; } public override string ToString() => $"{Make} {Model} ({Year})"; }

    // 11. Access modifiers
    static void Exercise11_AccessModifiers()
    {
        var d = new Derived(); d.PublicMethod(); // can access protected via derived
        var n = new NonDerived(); n.CallPublic();
        class BaseClass { public void PublicMethod() => Console.WriteLine("public"); private void PrivateMethod() => Console.WriteLine("private"); protected void ProtectedMethod() => Console.WriteLine("protected"); }
        class Derived : BaseClass { public void PublicMethod() { base.PublicMethod(); base.ProtectedMethod(); } }
        class NonDerived { public void CallPublic() { var b = new BaseClass(); b.PublicMethod(); } }
    }

    // 12. Auto-properties with backing field validation
    static void Exercise12_AutoProps()
    {
        var p = new Product { Name = "Widget" };
        p.Price = 25; Console.WriteLine(p.Name + " " + p.Price);
        try { p.Price = -5; } catch (ArgumentException ex) { Console.WriteLine("Validation: " + ex.Message); }
    }
    class Product { public string Name { get; set; } = ""; private decimal _price; public decimal Price { get => _price; set { if (value < 0) throw new ArgumentException("Price cannot be negative"); _price = value; } } }

    // 13. Records
    static void Exercise13_Records()
    {
        var e1 = new Employee("Emma", 101);
        var e2 = e1 with { EmployeeId = 102 };
        Console.WriteLine(e1); Console.WriteLine(e2);
    }
    record Employee(string Name, int EmployeeId);

    // 14. Inheritance and overriding
    static void Exercise14_Inheritance()
    {
        Shape s1 = new Circle(); Shape s2 = new Rectangle(); s1.Draw(); s2.Draw();
    }
    class Shape { public virtual void Draw() => Console.WriteLine("Drawing shape"); }
    class Circle : Shape { public override void Draw() => Console.WriteLine("Drawing circle"); }
    class Rectangle : Shape { public override void Draw() => Console.WriteLine("Drawing rectangle"); }

    // 15. Abstract class vs interface
    static void Exercise15_AbstractInterface()
    {
        Vehicle v = new CarVehicle(); v.Drive(); ((IDrivable)v).Start();
    }
    abstract class Vehicle { public abstract void Drive(); }
    interface IDrivable { void Start(); }
    class CarVehicle : Vehicle, IDrivable { public override void Drive() => Console.WriteLine("Driving"); public void Start() => Console.WriteLine("Starting"); }

    // 16. Null-safe handling
    static void Exercise16_Nullables()
    {
        PersonNullable? p = null;
        Console.WriteLine(p?.Name ?? "No person");
        p = new PersonNullable { Name = "Anna" };
        Console.WriteLine(p?.Name ?? "No person");
    }
    class PersonNullable { public string? Name { get; set; } }

    // 17. Null-conditional chaining
    static void Exercise17_NullConditional()
    {
        Contact? c = null;
        Console.WriteLine(c?.Name ?? "No contact");
        c = new Contact { Name = "Joe" };
        Console.WriteLine(c?.Name ?? "No contact");
    }
    class Contact { public string? Name { get; set; } public string? PhoneNumber { get; set; } }

    // 18. required modifier demo (C#12 feature); emulate with constructor requirement
    static void Exercise18_RequiredModifier()
    {
        var s = new Student("Sally"); Console.WriteLine(s.Name + " " + s.Roll);
    }
    class Student { public string Name { get; init; } = null!; public int Roll { get; init; } public Student(string name, int roll = 0) { Name = name; Roll = roll; } }

    // 19. Lists and Dictionaries
    static void Exercise19_ListsAndDictionaries()
    {
        var list = new List<string> { "a", "b" }; list.Add("c"); foreach (var s in list) Console.WriteLine(s);
        var dict = new Dictionary<int, string> { [1] = "one", [2] = "two" }; dict[3] = "three"; foreach (var kv in dict) Console.WriteLine(kv.Key + ":" + kv.Value);
    }

    // 20. LINQ
    static void Exercise20_LINQ()
    {
        var orders = new[] { new Order(1,"A",120), new Order(2,"B",50), new Order(3,"C",200) };
        var q = orders.Where(o => o.TotalAmount > 100).Select(o => new { o.OrderId, o.TotalAmount });
        foreach (var x in q) Console.WriteLine(x);
    }
    record Order(int OrderId, string CustomerName, decimal TotalAmount);

    // 21. Pattern matching
    static void Exercise21_PatternMatching()
    {
        object[] arr = { 1, "abc", 3.14, null };
        foreach (var o in arr) Describe(o);
        static void Describe(object? o) => Console.WriteLine(o switch { int i => $"int {i}", string s => $"string {s}", double d => $"double {d}", null => "null", _ => "unknown" });
    }

    // 22. Tuples
    static void Exercise22_Tuples()
    {
        var t = GetValues(); var (num, text) = t; Console.WriteLine(num + ", " + text);
        static (int, string) GetValues() => (42, "answer");
    }

    // 23. Async file upload simulation
    static async Task Exercise23_AsyncUpload()
    {
        try
        {
            Console.WriteLine("Uploading...");
            await Task.Delay(3000);
            Console.WriteLine("Upload complete");
        }
        catch (Exception ex) { Console.WriteLine("Upload failed: " + ex.Message); }
    }

    // 24. JSON serialize/deserialize
    static void Exercise24_JSON()
    {
        var user = new { Name = "Alice", Age = 30, Email = "a@x.com" };
        var s = JsonSerializer.Serialize(user);
        File.WriteAllText("user.json", s);
        var json = File.ReadAllText("user.json"); var obj = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        Console.WriteLine(string.Join(',', obj!.Select(kv => kv.Key + ":" + kv.Value)));
    }

    // 25. FileStream and MemoryStream
    static void Exercise25_FileStreams()
    {
        File.WriteAllText("sample.txt", "Hello from file");
        using var fs = File.OpenRead("sample.txt"); using var sr = new StreamReader(fs); Console.WriteLine(sr.ReadToEnd());
        using var ms = new MemoryStream(); var data = System.Text.Encoding.UTF8.GetBytes("memory data"); ms.Write(data, 0, data.Length); Console.WriteLine($"Bytes written: {ms.Length}");
    }

    // 26. Race condition and lock
    static void Exercise26_RaceCondition()
    {
        int counter = 0; var tasks = new List<Thread>(); object locker = new();
        for (int i = 0; i < 5; i++)
        {
            var t = new Thread(() => { for (int k = 0; k < 1000; k++) { lock (locker) { counter++; } } });
            tasks.Add(t); t.Start();
        }
        foreach (var t in tasks) t.Join();
        Console.WriteLine($"Counter={counter} (expected 5000)");
    }

    // 27. Deadlock simulation and resolution using Monitor.TryEnter
    static void Exercise27_DeadlockDemo()
    {
        object a = new(), b = new();
        var t1 = new Thread(() => { lock (a) { Thread.Sleep(100); lock (b) { Console.WriteLine("t1 acquired both"); } } });
        var t2 = new Thread(() => { lock (b) { Thread.Sleep(100); lock (a) { Console.WriteLine("t2 acquired both"); } } });
        t1.Start(); t2.Start(); t1.Join(2000); t2.Join(2000); Console.WriteLine("Simple deadlock demo executed (may hang if real deadlock). Using TryEnter is safer in production.");
    }

    // 28. Trace logging
    static void Exercise28_TraceLogging()
    {
        System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.TextWriterTraceListener(Console.Out));
        System.Diagnostics.Trace.AutoFlush = true;
        System.Diagnostics.Trace.WriteLine("Trace message: application started");
        var logFile = "trace.log";
        System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.TextWriterTraceListener(File.CreateText(logFile)));
        System.Diagnostics.Trace.WriteLine("Also logged to file");
    }

    // 29. Sanitize input (basic HTML encode)
    static void Exercise29_SanitizeInput()
    {
        string userInput = "<script>alert(1)</script> Hello";
        string encoded = System.Net.WebUtility.HtmlEncode(userInput);
        Console.WriteLine(encoded);
    }

    // 30. ADO.NET CRUD template (needs local SQL Server and connection string)
    static void Exercise30_ADONET_Template()
    {
        Console.WriteLine("ADO.NET template: adjust connection string and uncomment code to run against SQL Server.");
        Console.WriteLine("Sample code uses System.Data.SqlClient or Microsoft.Data.SqlClient. Add the package if needed.");

        // Placeholder code snippet (commented). Fill connection string and uncomment to use.
        /*
        var connString = "Server=.;Database=TestDb;Trusted_Connection=True;";
        using var conn = new System.Data.SqlClient.SqlConnection(connString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Employees (Id INT IDENTITY PRIMARY KEY, Name NVARCHAR(100))";
        cmd.ExecuteNonQuery();
        cmd.CommandText = "INSERT INTO Employees (Name) VALUES (@n)"; cmd.Parameters.AddWithValue("@n","John"); cmd.ExecuteNonQuery();
        cmd.CommandText = "SELECT Id, Name FROM Employees"; using var rdr = cmd.ExecuteReader(); while (rdr.Read()) Console.WriteLine(rdr.GetInt32(0) + " " + rdr.GetString(1));
        */
    }
}
