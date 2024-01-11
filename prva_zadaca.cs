using System;
using System.Collections.Generic;
namespace Zadaca1
{

    public abstract class Osoba
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int ID;

        public Osoba(string ime, string prezime, int id)
        {
            Ime = ime;
            Prezime = prezime;
            ID = new Random().Next();
        }
    }

 
    public class Student : Osoba
    {
        public List<int> Ocjene { get; set; }
        public string Razred { get; set; }

        public Student(string ime, string prezime, int id, string razred) : base(ime, prezime, id)
        {
            Ocjene = new List<int>();
            Razred = razred;
        }

        public void DodajOcjenu(int ocjena)
        {
            Ocjene.Add(ocjena);
        }

        public void PrikaziOcjene()
        {
            Console.WriteLine($"Ocjene studenta {Ime} {Prezime}: {string.Join(", ", Ocjene)}");
        }
    }

    public class Ucitelj : Osoba
    {
        public List<string> Predmeti { get; set; }

        public Ucitelj(string ime, string prezime, int id, List<string> predmeti) : base(ime, prezime, id)
        {
            Predmeti = predmeti;
        }

        public void DodajOcjenuStudentu(Student student, int ocjena)
        {
            student.DodajOcjenu(ocjena);
        }

        public void PregledOcjenaUcenika(Student student)
        {
            student.PrikaziOcjene();
        }

        public void PregledOcjenaRazreda(List<Student> razred)
        {
            foreach (var student in razred)
            {
                PregledOcjenaUcenika(student);
            }
        }
    }

   public class Ravnatelj : Osoba
    {
        public List<Ucitelj> Ucitelji { get; set; }
        public List<Student> Studenti { get; set; }

        public Ravnatelj(string ime, string prezime, int id) : base(ime, prezime, id)
        {
            Ucitelji = new List<Ucitelj>();
            Studenti = new List<Student>();
        }

        public void DodajUcitelja(Ucitelj ucitelj)
        {
            Ucitelji.Add(ucitelj);
        }

        public void DodajStudenta(Student student)
        {
            Studenti.Add(student);
        }

        public void UkloniUcitelja(Ucitelj ucitelj)
        {
            Ucitelji.Remove(ucitelj);
        }

        public void UkloniStudenta(Student student)
        {
            Studenti.Remove(student);
        }

        public void PregledOpćihOcjenaSkole()
        {
            foreach (var student in Studenti)
            {
                student.PrikaziOcjene();
            }
        }
    }

    public class Razred
    {
        public string NazivRazreda { get; set; }
        public List<Student> Studenti { get; set; }

        public Razred(string nazivRazreda)
        {
            NazivRazreda = nazivRazreda;
            Studenti = new List<Student>();
        }

        public void DodajStudenta(Student student)
        {
            Studenti.Add(student);
        }

        public void UkloniStudenta(Student student)
        {
            Studenti.Remove(student);
        }

        public void PrikaziOcjeneRazreda()
        {
            foreach (var student in Studenti)
            {
                student.PrikaziOcjene();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Ravnatelj ravnatelj = new Ravnatelj("ImeRavnatelja", "PrezimeRavnatelja", 1);

            Ucitelj ucitelj = new Ucitelj("ImeUcitelja", "PrezimeUcitelja", 2, new List<string> { "Matematika", "Hrvatski" });
            ravnatelj.DodajUcitelja(ucitelj);

            Student student = new Student("ImeStudenta", "PrezimeStudenta", 3, "1A");
            ravnatelj.DodajStudenta(student);

            Razred razred1A = new Razred("1A");
            razred1A.DodajStudenta(student);

            ucitelj.DodajOcjenuStudentu(student, 5);

            ravnatelj.PregledOpćihOcjenaSkole();
            ucitelj.PregledOcjenaRazreda(razred1A.Studenti);
        }
    }
}