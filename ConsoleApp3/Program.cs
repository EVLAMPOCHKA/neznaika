using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What you want? 1 - login; 2 - registration");
            var x = Console.ReadLine();

            switch(x)
            {
                case "1": Login(); break;
                case "2": CreateUsers(); break;
            }
            
            //foreach (var user in users)
            //{
            //    Console.WriteLine(user);
            //}

            

        }

        static void CreateUsers()
        {
            var sol = new Users();
            Console.WriteLine("Create your login, please");
            sol.Login = Console.ReadLine();
            Console.WriteLine("Create your password, please");
            sol.Password = GetPassword();

            var json = new JavaScriptSerializer().Serialize(sol);

            using (var file = File.Open("List.txt", FileMode.Append))
            {
                using (var sw = new StreamWriter(file))
                {
                    sw.WriteLine(json);
                }

            }
        }


        static List<Users> GetUsers()
        {
            var lines = File.ReadAllLines("List.txt");
            var arrayOfUsers = new List<Users>();

            foreach (var line in lines)
            {
                var a = new JavaScriptSerializer().Deserialize<Users>(line);
                arrayOfUsers.Add(a);
            }
            return arrayOfUsers;
        }

        static void Login()
        {
            Console.WriteLine("Enter your LOGIN?");
            var login = Console.ReadLine();
            Console.WriteLine("Enter your PASSWORD?");
            var password = GetPassword();



            var users = GetUsers();

            var w = users.FirstOrDefault(curUser => curUser.Login == login && curUser.Password == password);

            if (w == null)
            {
                Console.WriteLine("Mistake!!!!!!!!!!");
            }
            else
            {
                Console.WriteLine($"\nOK, {login}, HELLO!!!!");
            }
        }

        static string GetPassword()
        {
            string pass = "";
            do
            {
                var key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            return pass;
        }

    }
}
