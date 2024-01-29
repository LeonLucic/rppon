using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Lanac odgovornosti
class Request
{
    public string Type { get; set; }

    public Request(string type)
    {
        Type = type;
    }
}

abstract class Handler
{
    protected Handler successor;

    public void SetSuccessor(Handler successor)
    {
        this.successor = successor;
    }

    public abstract void HandleRequest(Request request);
}

class ProfessorHandler : Handler
{
    public override void HandleRequest(Request request)
    {
        if (request.Type == "GradeApproval")
        {
            Console.WriteLine("Profesor je odobrio ocjenu.");
        }
        else if (successor != null)
        {
            successor.HandleRequest(request);
        }
        else
        {
            Console.WriteLine("Zahtjev nije obrađen.");
        }
    }
}

class DeanHandler : Handler
{
    public override void HandleRequest(Request request)
    {
        if (request.Type == "ExamApproval")
        {
            Console.WriteLine("Dekan je odobrio ispit.");
        }
        else if (successor != null)
        {
            successor.HandleRequest(request);
        }
        else
        {
            Console.WriteLine("Zahtjev nije obrađen.");
        }
    }
}

class Program
{
    static void Main()
    {
        Handler professor = new ProfessorHandler();
        Handler dean = new DeanHandler();

        professor.SetSuccessor(dean);

        Request gradeApprovalRequest = new Request("GradeApproval");
        Request examApprovalRequest = new Request("ExamApproval");
        Request thesisApprovalRequest = new Request("ThesisApproval");

        professor.HandleRequest(gradeApprovalRequest);
        professor.HandleRequest(examApprovalRequest);
        professor.HandleRequest(thesisApprovalRequest);
    }
}


//Iterator:

using System;
using System.Collections;

class Course
{
    public string Name { get; set; }

    public Course(string name)
    {
        Name = name;
    }
}

class FacultyCourses : IEnumerable
{
    private Course[] courses;
    private int count;

    public FacultyCourses(int capacity)
    {
        courses = new Course[capacity];
        count = 0;
    }

    public void AddCourse(Course course)
    {
        if (count < courses.Length)
        {
            courses[count] = course;
            count++;
        }
    }

    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return courses[i];
        }
    }
}

class Program
{
    static void Main()
    {
        FacultyCourses facultyCourses = new FacultyCourses(3);
        facultyCourses.AddCourse(new Course("Programming"));
        facultyCourses.AddCourse(new Course("Database Design"));
        facultyCourses.AddCourse(new Course("Machine Learning"));

        Console.WriteLine("Faculty Courses:");

        foreach (Course course in facultyCourses)
        {
            Console.WriteLine(course.Name);
        }
    }
}
//U primjerima Lanac odgovornosti, profesor i dekan obrađuju zahtjeve za odobrenje ocjene i ispita.
//U primjerima Iterator, prikazana je kolekcija fakultetskih predmeta koja se može iterirati.