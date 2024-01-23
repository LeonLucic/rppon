using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace Zadaca2

{//SRP

    public class Student
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int BrojIndeksa { get; set; }

        public void DodajStudentaUBazu(Student student)
        {
            // Logika za dodavanje studenta u bazu podataka
            Console.WriteLine("Dodan student u bazu.");
        }

        public double IzracunajProsjek(int[] ocjene)
        {
            // Logika za izračunavanje prosjeka ocjena
            double prosjek = 0;
            foreach (var ocjena in ocjene)
            {
                prosjek += ocjena;
            }
            return prosjek / ocjene.Length;
        }

        public void PosaljiEmailObavijest(string poruka)
        {
            // Logika za slanje email obavijesti studentu
            Console.WriteLine("Email obavijest poslana.");
        }
    }

    class Program
    {
        static void Main()
        {
            Student student = new Student
            {
                Ime = "Marko",
                Prezime = "Marković",
                BrojIndeksa = 12345
            };

            // Kršenje SRP-a
            student.DodajStudentaUBazu(student);
            int[] ocjene = { 4, 5, 3, 4 };
            double prosjek = student.IzracunajProsjek(ocjene);
            student.PosaljiEmailObavijest($"Vaš prosjek je {prosjek}");

            Console.ReadLine();
        }
    }
    //Ispravak bi bio razdvajanje tih odgovornosti u različite razrede. Na primjer, mogli bismo imati BazaPodatakaHandler,
    //ProsjekOcjenaCalculator i EmailHandler razrede koji će biti odgovorni za svoje specifične zadatke.


    //OCP

    public class Predmet
    {
        public string Naziv { get; set; }
        public int BrojECTSBodova { get; set; }
    }

    public class Student
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public void PrijaviIspit(Predmet predmet)
        {
            // Logika za prijavu ispita
            Console.WriteLine($"Student {Ime} {Prezime} prijavljuje ispit iz predmeta {predmet.Naziv}.");
        }
    }

    class Program
    {
        static void Main()
        {
            Student student = new Student
            {
                Ime = "Ana",
                Prezime = "Anić"
            };

            Predmet matematika = new Predmet
            {
                Naziv = "Matematika",
                BrojECTSBodova = 5
            };

            // Kršenje OCP-a
            student.PrijaviIspit(matematika);

            Console.ReadLine();
        }
    }
    //Ovo krši OCP jer klasa Student nije zatvorena za modifikacije. Ako želimo dodati novi tip predmeta, moramo mijenjati
    //postojeći kod klase Student, što može dovesti do neželjenih nuspojava i otežati održavanje sustava.
    //Ispravak bi bio korištenje OCP-a putem abstrakcije i polimorfizma, gdje bi se klasa Student oslonila na apstrakciju(npr., sučelje IPredmet)
    //umjesto na konkretan tip(Predmet). Tako bi se omogućilo proširenje ponašanja za različite vrste predmeta bez direktnog modificiranja koda klase Student.

    //LSP

    // Nadklasa koja predstavlja općeniti entitet na fakultetu
    public class FakultetskiEntitet
    {
        public virtual void IspisiInformacije()
        {
            Console.WriteLine("Informacije o fakultetskom entitetu");
        }
    }

    // Podklasa koja nasljeđuje nadklasu, ali narušava LSP
    public class Profesor : FakultetskiEntitet
    {
        public override void IspisiInformacije()
        {
            Console.WriteLine("Informacije o profesoru");
        }

        public void DrziPredavanje()
        {
            Console.WriteLine("Profesor drži predavanje");
        }
    }

    // Podklasa koja nasljeđuje nadklasu i ne narušava LSP
    public class Student : FakultetskiEntitet
    {
        public override void IspisiInformacije()
        {
            Console.WriteLine("Informacije o studentu");
        }

        public void PohađaPredavanje()
        {
            Console.WriteLine("Student pohađa predavanje");
        }
    }

    class Program
    {
        static void Main()
        {
            // Korištenje nadklase
            FakultetskiEntitet entitet1 = new FakultetskiEntitet();
            entitet1.IspisiInformacije();

            // Korištenje podklase koja narušava LSP
            FakultetskiEntitet entitet2 = new Profesor();
            entitet2.IspisiInformacije(); // Očekujemo "Informacije o profesoru"
                                          // Sljedeća linija će izazvati problem jer nadklasa ne prepoznaje metodu DrziPredavanje:
                                          // entitet2.DrziPredavanje();

            // Korištenje podklase koja ne narušava LSP
            FakultetskiEntitet entitet3 = new Student();
            entitet3.IspisiInformacije(); // Očekujemo "Informacije o studentu"
                                          // Sljedeća linija neće izazvati problem jer Student ne uvodi nove metode koje nadklasa ne prepoznaje:
                                          // entitet3.PohađaPredavanje();
        }
    }
    //U ovom primjeru, klasa Profesor narušava LSP jer uvodi novu metodu DrziPredavanje koja nije dio nadklase FakultetskiEntitet.Kada koristimo referencu tipa nadklase (FakultetskiEntitet) kako bismo stvorili objekt podklase (Profesor), dobivamo problem jer ne možemo pristupiti novoj metodi DrziPredavanje.To narušava očekivanje LSP-a da objekti podklase budu potpuno zamjenjivi za objekte nadklase.


    //ISP

    // Sučelje koje predstavlja aktivnosti na fakultetu
    interface IFakultetAktivnosti
    {
        void PohađajPredavanja();
        void SudjelujNaSeminaru();
        void ObaviIspit();
    }

    // Implementacija studenta koji sudjeluje u svim aktivnostima na fakultetu
    class Student : IFakultetAktivnosti
    {
        public void PohađajPredavanja()
        {
            Console.WriteLine("Student pohađa predavanja.");
        }

        public void SudjelujNaSeminaru()
        {
            Console.WriteLine("Student sudjeluje na seminaru.");
        }

        public void ObaviIspit()
        {
            Console.WriteLine("Student obavlja ispit.");
        }
    }

    // Implementacija profesora koji se bavi samo predavanjima
    class Profesor : IFakultetAktivnosti
    {
        public void PohađajPredavanja()
        {
            Console.WriteLine("Profesor predaje predavanja.");
        }

        // Profesor ne sudjeluje na seminarima i ne polaže ispite
        public void SudjelujNaSeminaru()
        {
            throw new NotImplementedException();
        }

        public void ObaviIspit()
        {
            throw new NotImplementedException();
        }
    }

    // Klijentski kod koji koristi sučelje
    class Fakultet
    {
        public static void GlavniProgram(IFakultetAktivnosti aktivnosti)
        {
            aktivnosti.PohađajPredavanja();
            aktivnosti.SudjelujNaSeminaru();
            aktivnosti.ObaviIspit();
        }
    }

    class Program
    {
        static void Main()
        {
            Student student = new Student();
            Fakultet.GlavniProgram(student);

            Profesor profesor = new Profesor();
            Fakultet.GlavniProgram(profesor);
        }
    }

    //Ovaj primjer krši ISP jer profesor ne treba sudjelovati na seminarima i polagati ispite, ali je prisiljen
    //implementirati te metode iz sučelja. To dovodi do nepotrebnog opterećenja za klasu Profesor i stvaranja nepotrebnih
    //metoda koje neće koristiti. Da bi se ispravilo, trebali bismo razdvojiti sučelje na manja sučelja koja predstavljaju specifične aktivnosti.

    //DIP
    
    // Visoka razina modula koji predstavlja neki fakultet
    public class Fakultet
    {
        public void PrikaziPodatkeStudenta()
        {
            // Ovisnost o niskoj razini modula (direktna ovisnost o konkretnoj implementaciji)
            Student student = new Student();
            Console.WriteLine("Podaci o studentu: " + student.DohvatiPodatke());
        }
    }

    // Niska razina modula koji predstavlja konkretnu implementaciju studenta
    public class Student
    {
        public string DohvatiPodatke()
        {
            return "Ime Prezime, JMBAG: 123456";
        }
    }

   //Ispravan način primjene Dependency Inversion Principle bio bi korištenje sučelja(apstrakcije) kako bi se visoka razina modula
   //odvojila od konkretnih implementacija.Evo ispravljenog primjera:



// Apstrakcija (sučelje) koje predstavlja studenta
public interface IStudent
    {
        string DohvatiPodatke();
    }

    // Visoka razina modula koji predstavlja neki fakultet
    public class Fakultet
    {
        private readonly IStudent student;

        // Konstruktor koji injektira ovisnost
        public Fakultet(IStudent student)
        {
            this.student = student;
        }

        public void PrikaziPodatkeStudenta()
        {
            // Korištenje apstrakcije umjesto konkretnog studenta
            Console.WriteLine("Podaci o studentu: " + student.DohvatiPodatke());
        }
    }

    // Niska razina modula koji implementira sučelje IStudent
    public class KonkretniStudent : IStudent
    {
        public string DohvatiPodatke()
        {
            return "Ime Prezime, JMBAG: 123456";
        }
    }