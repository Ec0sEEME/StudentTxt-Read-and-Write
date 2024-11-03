using System;
using System.Collections.Generic;
using System.IO;


public class Student
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Mark { get; set; }

    public Student(string name, string surname, int mark)
    {
        Name = name;
        Surname = surname;
        Mark = mark;
    }

    public override string ToString()
    {
        return $"Name: {Name}\nSurname: {Surname}\nMark: {Mark}%\n----------------------------------";
    }
}


public class FileHandler
{
    private string filePath;

    public FileHandler(string filePath)
    {
        this.filePath = filePath;
    }

    
    public List<Student> ReadStudents()
    {
        var students = new List<Student>();

        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 3)
                {
                    var name = parts[0];
                    var surname = parts[1];
                    var mark = int.Parse(parts[2]);
                    students.Add(new Student(name, surname, mark));
                }
            }
        }

        return students;
    }

    
    public void WriteStudent(Student student)
    {
        using (StreamWriter sw = File.AppendText(filePath))
        {
            sw.WriteLine($"{student.Name},{student.Surname},{student.Mark}");
        }
    }
}


public class Display
{
    private FileHandler fileHandler;

    public Display(FileHandler fileHandler)
    {
        this.fileHandler = fileHandler;
    }

    
    public void DisplayAllStudents()
    {
        var students = fileHandler.ReadStudents();
        Console.WriteLine("========== All Students ===========");
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }

    
    public void CaptureAndSaveStudent()
    {
        Console.WriteLine(">>>> Capture a Student:");
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Surname: ");
        string surname = Console.ReadLine();

        Console.Write("Mark: ");
        int mark = int.Parse(Console.ReadLine());

        var newStudent = new Student(name, surname, mark);
        fileHandler.WriteStudent(newStudent);

        Console.WriteLine("\nStudent added successfully!\n");
    }
}


class Program
{
    static void Main(string[] args)
    {
        
        string filePath = "MyTextFile.txt";

      
        var fileHandler = new FileHandler(filePath);
        var display = new Display(fileHandler);

        
        display.DisplayAllStudents();

      
        display.CaptureAndSaveStudent();

     
        display.DisplayAllStudents();
    }
}
