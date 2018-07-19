

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller.SignSerch();
            Console.Clear();
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
        public CV cv { get; set; }

    }

    class Employer : Person
    {
        public List<Advertisement> adverlist = new List<Advertisement>();
        public int ID { get; set; }

        public Employer(int ID)
        {
            this.ID = ID;
        }

    }

    class Advertisement
    {
        public string JobName { get; set; }
        public string CompanyName { get; set; }
        public int Category { get; set; }
        public string JobDescription { get; set; }
        public int City { get; set; }
        public int Salary { get; set; }
        public int Age { get; set; }
        public int Education { get; set; }
        public int Experience { get; set; }
        public string Phone { get; set; }
        public List<Worker> Requests { get; set; }
        public int Employ_ID { get; set; }

    }

    class CV
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Gender { get; set; }
        public int Education { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public int Category { get; set; }
        public int City { get; set; }
        public int Salary { get; set; }
        public string Phone { get; set; }
    }

    static class Controller
    {
        static JsonSerializerSettings jsonSetting = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects };

        static string input = null;
        static Person person = null;
        static bool Trueform = false;
        static List<Person> workers = new List<Person>();
        static List<Person> employers = new List<Person>();
        static CV Cv = new CV();
        static Worker workcast = null;
        static Employer employcast = null;
        static Advertisement advertisement = new Advertisement();
        static List<Employer> castemployList = null;


        static public void WorkOrEmploy()
        {

            do
            {
                Console.WriteLine("1. Worker");
                Console.WriteLine("2. Employer");

                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        person = new Worker();
                        break;
                    case "2":
                        person = new Employer(employers.Count);






                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Sehv kod. Tekrar daxil et.");
                        Console.WriteLine();
                        break;
                }
            } while (input != "1" && input != "2");

        }

        static public void SignSerch()
        {
            string serch = null;

            FileInfo WfileInfo = new FileInfo("Workers.json"); //file-nin olub olmadigini yoxlayir. eger yoxdursa ashagidaki if shertinin ichindeki kimi onu yaradir
            FileInfo EfileInfo = new FileInfo("Employers.json");//file-nin olub olmadigini yoxlayir. eger yoxdursa ashagidaki if shertinin ichindeki kimi onu yaradir

            if (!WfileInfo.Exists)//fayl yoxdursa onu yeniden yaradir
            {
                var Workerjson = JsonConvert.SerializeObject(workers, jsonSetting);
                using (StreamWriter writer1 = new StreamWriter("Workers.json"))
                {
                    writer1.WriteLine(Workerjson);
                }
            }

            if (!EfileInfo.Exists)//fayl yoxdursa onu yeniden yaradir
            {
                var Employersjson = JsonConvert.SerializeObject(employers, jsonSetting);
                using (StreamWriter writer = new StreamWriter("Employers.json"))
                {
                    writer.WriteLine(Employersjson);
                }

            }

            Readfile(); //fayl yaranir ve ya sifirdan teyin olunduqdan sonra her bir halda fayli oxuyur. fayl proqram erzinde cemi bir defe oxunur bununla. ve yekunda exit edende edilmish deyishiklikler fayla yailir

            while (serch != "3")
            {
                Console.WriteLine("1. Sign In");
                Console.WriteLine("2. Sign Up");

                Console.WriteLine("3. Exit");

                serch = Console.ReadLine();

                switch (serch)
                {
                    case "1":
                        WorkOrEmploy();
                        SignIn(); break;
                    case "2":
                        WorkOrEmploy();
                        SignUp(); break;
                    case "3":
                        Exit();
                        Environment.Exit(0);
                        break;
                    default: Console.Clear(); Console.WriteLine("Error! Try again"); break;
                }
            }
        }

        static public void SignIn()
        {

            Person person2 = null;
            switch (input)
            {
                case "1": person2 = new Worker(); break;
                case "2": person2 = new Employer(employers.Count); break;
            }
            Console.WriteLine("Email:");
            person2.Email = Console.ReadLine();
            Console.WriteLine("Password:");
            person2.Password = Console.ReadLine();
            Person a = null;

            switch (input)
            {
                case "1":
                    try
                    {
                        a = workers.SingleOrDefault(x => x.Email == person2.Email && x.Password == person2.Password);

                        workcast = (Worker)a;
                        Console.Clear();
                        Console.WriteLine($"Xosh gelmisiz {a.Username}");

                        WorkerMenu(); break;
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine("Bele istifadechi yoxdur"); break;
                    }

                case "2":
                    try
                    {
                        a = employers.SingleOrDefault(x => x.Email == person2.Email && x.Password == person2.Password);
                        Console.WriteLine($"Xosh gelmisiz {a.Username}");
                        employcast = (Employer)a;
                        EmployerMenu();
                        break;
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine("Bele istifadechi yoxdur"); break;
                    }

            }

        }

        static public void WorkerMenu()
        {
            string a = null;

            do
            {
                Console.WriteLine($"User name: {workcast.Username}");
                Console.WriteLine("1. Create CV");
                Console.WriteLine("2. Find a job");
                Console.WriteLine("3. Search");
                Console.WriteLine("4. Show all job ads");
                Console.WriteLine("5. Show CV information");
                Console.WriteLine("6. Log out");

                a = Console.ReadLine();
                Console.Clear();
                switch (a)
                {
                    case "1": workcast.cv = Create_CV(); break;
                    case "2": Find_Job(); break;

                    case "3":
                        Console.WriteLine("Chose one of the variants to serch");
                        Console.WriteLine("Category:");
                        Console.WriteLine("1. Doctor");
                        Console.WriteLine("2. Driver");
                        Console.WriteLine("3. It spesialist");
                        Console.WriteLine("4. Translator");
                        var input = Convert.ToInt32(Console.ReadLine());
                        Search(input); break;
                    case "4": Show_all_jobs(); break;
                    case "5": Show_CV_info(); break;
                }

                Console.WriteLine("1. Return to main menu");
                Console.WriteLine("2. Log out");

                if (a != "6")
                {
                    do
                    {
                        a = Console.ReadLine();
                        if (a != "1" && a != "2")
                        {
                            Console.Clear();
                            Console.WriteLine("You must press 1 or 2");
                            Console.WriteLine();
                            Console.WriteLine("1. Return to main menu");
                            Console.WriteLine("2. Log out");
                        }
                    } while (a != "1" && a != "2");

                }

                Console.Clear();
            } while (a == "1" && a != "6");
            Console.Clear();
        }

        static public CV Create_CV()
        {
            Console.WriteLine("Create CV:");
            CV cv = new CV();
            Console.WriteLine("Name: ");
            cv.Name = Console.ReadLine();
            Console.WriteLine("Surname: ");
            cv.Surname = Console.ReadLine();
            Console.WriteLine("Gender:");
            Console.WriteLine("1. Man");
            Console.WriteLine("2. Woman");
            Console.WriteLine("3 Other");
            cv.Gender = Convert.ToInt32(Console.ReadLine());
            Console.Write("Age: ");
            cv.Age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Edication: ");
            Console.WriteLine("1. High");
            Console.WriteLine("2. Incomplete higher");
            Console.WriteLine("3. School education");
            cv.Education = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Job experience");
            Console.WriteLine("1. Less than 1 year");
            Console.WriteLine("2. Between 1 and 3 years");
            Console.WriteLine("3. Between 3 and 5 years");
            Console.WriteLine("4. 5 years and more");
            cv.Experience = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Category:");
            Console.WriteLine("1. Doctor");
            Console.WriteLine("2. Driver");
            Console.WriteLine("3. It spesialist");
            Console.WriteLine("4. Translator");
            cv.Category = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("City:");
            Console.WriteLine("1. Baku");
            Console.WriteLine("2. Ganja");
            Console.WriteLine("3. Sumqayit");
            Console.WriteLine("4. Xirdalan");
            cv.City = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Salary:");
            cv.Salary = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Mobil phone:");
            string MobFormat = @"^(\+994)(50)|(51)|(55)|(70)|(77)(\d+){7}$";
            do
            {
                cv.Phone = Console.ReadLine();
                Trueform = Regex.IsMatch(cv.Phone, MobFormat);
                if (!Trueform)
                {
                    Console.WriteLine("Sehv format! Dogru variant misal: +99455xxxxxxx");
                }
            } while (!Trueform);

            return cv;
        }

        static public void Find_Job()
        {
            int count = 0;
            castemployList = employers.Cast<Employer>().ToList();


            var tor = castemployList.Select(x => x.adverlist.Where(y => y.Category == workcast.cv.Category)).ToList();

            foreach (var item in tor)
            {

                foreach (var item1 in item)
                {
                    Console.WriteLine($"{++count}. Company name: {item1.CompanyName}, Job name: {item1.JobName}");
                }
            }
            Console.WriteLine("STOP1");
            var a = Search_Request(tor);
            Console.WriteLine("STOP2");
            Show_Details(a);
            Console.WriteLine("STOP3");
        }

        static public void Find_Worker()
        {
            int count = 0;
            castemployList = employers.Cast<Employer>().ToList();


            var tor = castemployList.Select(x => x.adverlist.Where(y => y.Category == workcast.cv.Category)).ToList();

            foreach (var item in tor)
            {

                foreach (var item1 in item)
                {
                    Console.WriteLine($"{++count}. Company name: {item1.CompanyName}, Job name: {item1.JobName}");
                }
            }
            Console.WriteLine("STOP1");
            var a = Search_Request(tor);
            Console.WriteLine("STOP2");
            Show_Details(a);
            Console.WriteLine("STOP3");
        }

        static public Advertisement Search_Request(List<IEnumerable<Advertisement>> adverts)
        {
            int count = 0;
            var input = Convert.ToInt32(Console.ReadLine());
            Advertisement advert = null;

            foreach (var item in adverts)
            {

                foreach (var item1 in item)
                {
                    if (input == ++count)
                    {
                        advert = item1;

                        break;
                    }
                }
            }
            return advert;
        }

        static public void Show_Details(Advertisement advert)
        {
            Console.Write("Company name: ");
            Console.WriteLine(advert.CompanyName);
            Console.Write("Category: ");
            Console.WriteLine(advert.Category);
            Console.Write("Job name: ");
            Console.WriteLine(advert.JobName);
            Console.Write("Salary: ");
            Console.WriteLine(advert.Salary);
            Console.Write("City: ");
            Console.WriteLine(advert.City);
            Console.Write("Education: ");
            Console.WriteLine(advert.Education);
            Console.Write("Experience: ");
            Console.WriteLine(advert.Experience);
            Console.Write("Age: ");
            Console.WriteLine(advert.Age);
            Console.Write("Phone: ");
            Console.WriteLine(advert.Phone);
            Console.Write("Employ ID: ");
            Console.WriteLine(advert.Employ_ID);

            Console.Write("To request press \"x\" to continue press any key");

            var x = Console.ReadLine();
            if (x=="x")
            {

            var FindEmploy = castemployList.SingleOrDefault(s => s.ID == advert.Employ_ID);
            int si = 0;
            foreach (var item in FindEmploy.adverlist)
            {
                if (item.JobName == advert.JobName && item.Salary == advert.Salary && item.JobDescription == advert.JobDescription)
                {
                    FindEmploy.adverlist[si].Requests.Add(workcast);
                    break;
                }
                si++;
            }
                Console.WriteLine("Muracietiniz gonderildi!");
                Thread.Sleep(4000);
            }

        }

        static public void Search(int category)
        {
            int count = 0;

            castemployList = employers.Cast<Employer>().ToList();
            var tor = castemployList.Select(x => x.adverlist.Where(y => y.Category == category)).ToList();

            foreach (var item in tor)
            {
                foreach (var item1 in item)
                {
                    Console.WriteLine($"{++count}. Company name: {item1.CompanyName}, Job name: {item1.JobName}");
                }
            }
        }

        static public void Show_all_jobs()
        {
            int count = 0;
            castemployList = employers.Cast<Employer>().ToList();
            var tor = castemployList.Where(x => x.adverlist != null);

            foreach (var item in tor)
            {
                foreach (var item1 in item.adverlist)
                {
                    Console.WriteLine($"{++count}. Company name: {item1.CompanyName}, Job name: {item1.JobName}");
                }
            }
        }


        static public void Show_CV_info()
        {
            Console.WriteLine($"NAME: {workcast.cv.Name}");
            Console.WriteLine($"SURNAME: {workcast.cv.Surname}");

            Console.Write("GENDER: ");
            switch (workcast.cv.Gender)
            {
                case 1: Console.WriteLine("Man"); break;
                case 2: Console.WriteLine("Woman"); break;
                case 3: Console.WriteLine("Other"); break;
            }

            Console.Write("CITY: ");
            switch (workcast.cv.City)
            {
                case 1: Console.WriteLine("Baku"); break;
                case 2: Console.WriteLine("Ganja"); break;
                case 3: Console.WriteLine("Sumqayit"); break;
                case 4: Console.WriteLine("Xirdalan"); break;
            }
            Console.Write("CATEGORY: ");
            switch (workcast.cv.Category)
            {
                case 1: Console.WriteLine("Doctor"); break;
                case 2: Console.WriteLine("Driver"); break;
                case 3: Console.WriteLine("It spesialist"); break;
                case 4: Console.WriteLine("Translator"); break;
            }

            Console.WriteLine($"AGE: {workcast.cv.Age}");

            Console.Write("EDICATION: ");
            switch (workcast.cv.Education)
            {
                case 1: Console.WriteLine("High"); break;
                case 2: Console.WriteLine("Incomplete higher"); break;
                case 3: Console.WriteLine("School education"); break;
            }

            Console.Write("JOB EXPERIENCE: ");
            switch (workcast.cv.Experience)
            {
                case 1: Console.WriteLine("Less than 1 year"); break;
                case 2: Console.WriteLine("Between 1 and 3 years"); break;
                case 3: Console.WriteLine("Between 3 and 5 years"); break;
                case 4: Console.WriteLine("5 years and more"); break;
            }

            Console.WriteLine($"SALARY: {workcast.cv.Salary}");
            Console.WriteLine($"PHONE: {workcast.cv.Phone}");
        }

        static public void EmployerMenu()
        {
            Console.WriteLine("1. Declare a job");
            Console.WriteLine("2. Find a worker");
            Console.WriteLine("3. Serch");
            Console.WriteLine("4. Appeals");
            Console.WriteLine("5. Log out");
            var serch = Console.ReadLine();
            Console.Clear();

            switch (serch)
            {
                case "1":
                    Console.WriteLine("Declare a job:"); Declare_a_job();
                    break;
            }
        }

        static public void Declare_a_job() //ish elani yerleshdirmek
        {

            Console.WriteLine($"You have {employcast.adverlist.Count} adverts.");
            Console.WriteLine("Job name:");
            advertisement.JobName = Console.ReadLine();
            Console.WriteLine("Company name:");
            advertisement.CompanyName = Console.ReadLine();
            Console.WriteLine("Category:");
            Console.WriteLine("1. Doctor");
            Console.WriteLine("2. Driver");
            Console.WriteLine("3. It spesialist");
            Console.WriteLine("4. Translator");
            advertisement.Category = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("City:");
            Console.WriteLine("1. Baku");
            Console.WriteLine("2. Ganja");
            Console.WriteLine("3. Sumqayit");
            Console.WriteLine("4. Xirdalan");
            advertisement.City = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Salary:");
            advertisement.Salary = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Age:");
            advertisement.Age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Education:");
            Console.WriteLine("1. High");
            Console.WriteLine("2. Incomplete higher");
            Console.WriteLine("3. School education");
            advertisement.Education = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Experience:");
            Console.WriteLine("1. Less than 1 year");
            Console.WriteLine("2. Between 1 and 3 years");
            Console.WriteLine("3. Between 3 and 5 years");
            Console.WriteLine("4. 5 years and more");
            advertisement.Experience = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Phone");
            string MobFormat = @"^(\+994)(50)|(51)|(55)|(70)|(77)(\d+){7}$";
            advertisement.Requests = new List<Worker>();
            do
            {
                advertisement.Phone = Console.ReadLine();
                Trueform = Regex.IsMatch(advertisement.Phone, MobFormat);
                if (!Trueform)
                {
                    Console.WriteLine("Sehv format! Dogru variant misal: +99455xxxxxxx");
                }
            } while (!Trueform);

            Console.WriteLine("Job description");
            advertisement.JobDescription = Console.ReadLine();
            advertisement.Employ_ID = employcast.ID; //hansi elanin hansi employere aid oldugunu bilmek uchun

            employcast.adverlist.Add(advertisement); //elanin liste elave edilmesi




        }

        static public void Readfile()
        {
            using (StreamReader read = new StreamReader("Workers.json"))
            {
                var fromWorkfile = JsonConvert.DeserializeObject<List<Person>>(read.ReadToEnd(), jsonSetting);
                workers = fromWorkfile;
            }

            using (StreamReader read1 = new StreamReader("Employers.json"))
            {
                var fromEmployFile = JsonConvert.DeserializeObject<List<Person>>(read1.ReadToEnd(), jsonSetting);
                employers = fromEmployFile;
            }
        }

        static public void Password()
        {
            string repass = null;
            do
            {
                if (repass != null)
                {
                    Console.WriteLine("Passwords don't match. Try again.");
                }
                Console.WriteLine("New password:");
                //--------------
                string passwordForm = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$"; //bir boyuk herf bir balaca bir reqem. 8-15 simvol arasi
                do
                {
                    person.Password = Console.ReadLine();
                    Trueform = Regex.IsMatch(person.Password, passwordForm);
                    if (!Trueform)
                    {
                        Console.WriteLine("Sehv format!");
                    }
                } while (!Trueform);

                Console.WriteLine("Please re-enter password");

                repass = Console.ReadLine();
            } while (repass != person.Password);
        }

        static public void SignUp()
        {

            Console.WriteLine("User name:");
            person.Username = Console.ReadLine();
            Console.WriteLine("Email:");
            var mailformat = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            do
            {
                person.Email = Console.ReadLine();
                Trueform = Regex.IsMatch(person.Email, mailformat);
                if (!Trueform)
                {
                    Console.WriteLine("Enter a correct format. Exp: muradheyderov@gmail.com");
                }
            } while (!Trueform);

            Password();

            person.Status = input;

            switch (input)
            {
                case "1": workers.Add(person); break;
                case "2":
                    employers.Add(person); break;
            }
        }


        //--------------------------------------
        static public void Exit()
        {
            var Workerjson = JsonConvert.SerializeObject(workers, jsonSetting);
            var Employersjson = JsonConvert.SerializeObject(employers, jsonSetting);

            using (StreamWriter writer1 = new StreamWriter("Workers.json"))
            {
                writer1.WriteLine(Workerjson);
            }
            using (StreamWriter writer = new StreamWriter("Employers.json"))
            {
                writer.WriteLine(Employersjson);
            }
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
//avtomatik yaradilacaq ve userden bu kodun eynisinin yazilmasini teleb edecek, Duzgun: w3Kp, 5Gq7)------------------//


// melumatlarini daxil edirler.

// 2. Eger isci kimi qeydiyyatdan kecibse esas menyuda bunlar gorsenecek



// 2.1. CV yerlesdir(Bu bolme secilen zaman asagidaki melumatlar elave olunmalidir)*****-----------------

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



// 2.1 Is axtar(CV melumatlarina gore)*********bunu worker kimi qeydiyyatdan kechen ve ya sign in olanlar gormelidir.

// 2.1. Isci bu bolmeni secdiyi zaman onun cv melumatlarina en cox uygun is elanlarini cixartmalidir.Eger isci elandaki sertleri odeyirse o elan gorsenmelidir.

// 2.2. Search

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


    