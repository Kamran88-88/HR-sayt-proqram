using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {

            Controller.WorkOrEmploy();
            Console.Clear();
            while (true)
            {
                
                Console.WriteLine("1. Sign In");
                Console.WriteLine("2. Sign Up");
                Console.WriteLine("3. Exit");
                string serch = Console.ReadLine();

                switch (serch)
                {
                    case "1": Controller.SignIn(); break;
                    case "2": Controller.SignUp(); break;
                    case "3": Controller.Exit(); break;
                    default: Console.WriteLine("Error! Try again"); break;


                }
            }

        }

    }

    class Person
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
    }

    class Worker : Person
    {
        public CV CV { get; set; }

    }
    class Employer : Person
    {


    }

    class CV
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Education { get; set; }
        public int Age { get; set; }
    }

    static class Controller
    {
        static string input = null;
        static Person person = null;
        static List<Person> people = new List<Person>();


      

        static public void WorkOrEmploy()
        {
            do
            {
                Console.WriteLine("1. Worker");
                Console.WriteLine("2. Employer");

                input = Console.ReadLine();
                switch (input)
                {
                    case "1": person = new Worker(); break;
                    case "2": person = new Employer(); break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Sehv kod. Tekrar daxil et.");
                        Console.WriteLine();
                        break;
                }
            } while (input != "1" && input != "2");

            
        }

        static public void SignIn()
        {
            Person person2 = null;
            switch (input)
            {
                case "1": person2 = new Worker(); break;
                case "2": person2 = new Employer(); break;
            }

            Console.WriteLine("User name:");
            person2.Username = Console.ReadLine();
            Console.WriteLine("Email:");
            person2.Email = Console.ReadLine();
            Console.WriteLine("Password:");
            person2.Password = Console.ReadLine();
            person2.Status = input;

            foreach (Person item in people)
            {
                if(item!=person2)
                {
                    Console.WriteLine("False");
                    continue;
                }
                else
                {
                    Console.WriteLine("False");
                    break;
                }
            }
        }

        static public void Readfile()
        {
            switch (input)
            {

                case "1":
                    person = new Worker();
                    using (StreamReader read = new StreamReader("Workers.json"))
                    {
                        var fromWorkfile = JsonConvert.DeserializeObject<List<Person>>(read.ReadToEnd());
                        people = fromWorkfile;
                    }
                    break;
                case "2":
                    person = new Employer();
                    using (StreamReader read1 = new StreamReader("Employers.json"))
                    {
                        var fromEmployFile = JsonConvert.DeserializeObject<List<Person>>(read1.ReadToEnd());
                        people = fromEmployFile;
                    }
                    break;
            }
        }

        static public void SignUp()
        {

            Readfile();

            Console.WriteLine("User name:");
            person.Username = Console.ReadLine();
            Console.WriteLine("Email:");
            var mailformat = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            bool Trueform;
            do
            {
                person.Email = Console.ReadLine();
                Trueform = Regex.IsMatch(person.Email, mailformat);
                if (!Trueform)
                {
                    Console.WriteLine("Duzgun format daxil edin. Misal: muradheyderov@gmail.com");
                }
            } while (!Trueform);

            Console.WriteLine("Password:");

           string passwordForm= @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
            do
            {
                person.Password = Console.ReadLine();
                Trueform = Regex.IsMatch(person.Password, passwordForm);
                if (!Trueform)
                {
                    Console.WriteLine("Sehv format!");
                }
            } while (!Trueform);

            person.Status = input;

                people.Add(person);

        }

        static public void Exit()
        {
            foreach (var item in people)
            {
                Console.WriteLine(item.Username);
            }

           // if (people == null)
            //{
                var Workerjson = JsonConvert.SerializeObject(people);

                switch (input)
                {
                    case "1":
                        using (StreamWriter writer1 = new StreamWriter("Workers.json"))
                        {
                            writer1.WriteLine(Workerjson);
                        }
                break;
                   case "2":
                        using (StreamWriter writer = new StreamWriter("Employers.json"))
                        {
                            writer.WriteLine(Workerjson);
                        }
                        break;
                }

            //}

        }


    }

}






//Bu proqram iscilerle is veren arasindaki elaqeni qurmaq ucundur.

// 1. Proqram sayesinde hem isciler hem de is verenler qeydiyyatdan kecir.Proqram acilan kimi sorusur Sign in, Sign up or Exit.Eger Sign up secilse asagidaki emeliyyatlar olur.

// 1.1 Isciler ve ya is verenler qeydiyyatdan kecdikleri zaman baslangic olaraq 
// - Username
// - Email (emailin formati regex le yoxlanilmalidir, format sehvdirse yeniden duzgun daxil etmesini istemelidir, Duzgun: muradheyderov @gmail.com)
// - Status: 1. Isci 2. Isveren
// - Sifre(
// -en azi bir boyuk herf olmalidir,
// -bir reqem, bir simvol (_+-/. ve s.) olamlidir, 
// -maksimum uzunluq 15 simvol, Duzgun: Murad_894
// )

// - tekrar password(yuxaridaki ile eyniliyi yoxlamaq ucun)
// - 4 simvoldan(reqem ve herf) ibaret random kod(bu kod random olaraq
//avtomatik yaradilacaq ve userden bu kodun eynisinin yazilmasini teleb edecek, Duzgun: w3Kp, 5Gq7)


// melumatlarini daxil edirler.

// 2. Eger isci kimi qeydiyyatdan kecibse esas menyuda bunlar gorsenecek

// 2.1. CV yerlesdir(Bu bolme secilen zaman asagidaki melumatlar elave olunmalidir)*****

// - Ad
// - Soyad
// - Cins(Kisi, Qadin)
// - Yas 
// - Tehsil(orta, natamam ali, ali)
// - Is tecrubesi
//{
// 1 ilden asagi,
// 1 ilden - 3 ile qeder
// 3 ilden - 5 ile qeder
// 5 ilden daha cox
//}
// - Kateqoriya(Evvelceden teyin olunur.Meselen, Hekim, Jurnalist, IT mutexessis, Tercumeci ve s.)
// - Seher(Baki, Gence, Seki ve s.)
// - Minimum emek haqqi 
// - Mobil telefon(+994 50/51/55/70/77 5555555(7) bu formati desteklemelidir)

// 3 Is axtar(CV melumatlarina gore)*********

// 3.1. Isci bu bolmeni secdiyi zaman onun cv melumatlarina en cox uygun is elanlarini cixartmalidir.Eger isci elandaki sertleri odeyirse o elan gorsenmelidir.

// 3.2. Search

// 2.3.1. Yuxarida qeyd olunmus melumatlarin her hansi birine gore axtaris (Kateqoriya, Tehsil, Seher, Emek haqqi, Is tecrubesi)

// 2.4. Melumatlari goster

// - CV de daxil eledikleri melumatlari seliqeli sekilde gostermelidir.

// 2.5. Butun elanlari goster*********

// 2.5.1. Elanlarin adini gostermelidir.Meselen,

// {
// 1.Hekim

// 2.Jurnalist

// 3.Tercumeci

// ve s.


// Elanin reqemini secdiyimiz anda hemen is elaninin detallarini gostermelidir.

// }

//Secilen elanin sonunda

// - Muraciet et(y/n)

// olmalidir eger y secse elana muraciet etmelidir n secidiyi zaman ise yeniden butun elanlari gostermelidir.

// 2.6. Log out. (User in cixis edib birinci menyuya qayitmagi ucun)

// 2.7. Muraciet olunmus elanlar. (Muraciet elediyin butun elanlarin siyahisi ve statusu) // Inactive*******

// 2.8. Teklifler // Inactive

// 3. Eger is veren kimi qeydiyyatdan kecibse esas menyuda bunlar gorsenecek

// 3.1. Elan yerlesdir(Is veren bir nece elan yerlesdire biler) ******

// - Is elanin adi
// - Sirketin adi
// - Kateqoriya
// - Is barede melumat
// - Seher
// - Maas 
// - Yas
// - Tehsil(orta, natamam ali, ali)
// - Is tecrubesi
// - Mobil telefon(+994 50/51/55/70/77 5555555(7) bu formati desteklemelidir)

// 3.2 Isci axtar(CV melumatlarina gore) //Inactive*****

// 3.3 Search(Yuxarida qeyd olunmus melumatlarin her hansi birine gore axtaris (Kateqoriya, Tehsil, Seher, Emek haqqi, Is tecrubesi)) // Inactive

// 3.3. Muracietler(Bu bolmede is elanina edilen muracietler gorsenmelidir (Is elanin adi, Muraciet eden isci ve onun melumatlari))****

// 3.4. Log out. (User in cixis edib birinci menyuya qayitmagi ucun)

// 4. Proqram sonunda Exit secilen zaman butun isci ve is verenin melumatlini serialise ederek json ve ya xml formatinda fayla yazmaq lazimdir.Iscileri ayri fayla is vereni ise ayri fayla. Proqram ise dusen zaman da hemen melumatlari oxuyub proqrami qaldigi yerden davam etdirmek lazimdir.


    //311 sayli mekteb yeni sabunchu  077/565-28-98