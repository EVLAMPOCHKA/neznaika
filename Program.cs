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
            Users User = null;
            int mod = 0;

            switch (x)
            {
                case "1": User = Login(); break;
                case "2": User = CreateUsers(mod); break;
            }

            if (User.Role == Roles.Admin)
            {
                Console.WriteLine("If you want to delete user push f");
                var option = Console.ReadLine();
                if (option == "f")
                {
                    ShowUsers();
                    Console.WriteLine("User ID for delete?");
                    var IdUserForDelete = int.Parse(Console.ReadLine());
                    if (IdUserForDelete == User.Id)
                        Console.WriteLine("You are stuped!!! You can't delete youself!!!!");
                    else
                            DeleteUser(IdUserForDelete);
                }

                Console.WriteLine("If you want to create new admin push f");
                var choice = Console.ReadLine();
                if (choice == "f")
                {
                    mod = 1;
                    CreateUsers(mod);

                }


            }
            else
            {
                ShowUsers();
            }
            //foreach (var user in users)
            //{
            //    Console.WriteLine(user);
            //}



        }

        static void DeleteUser(int IdUserForDel)
        {

            var Users = GetUsers();
            Users = Users.Where(x => x.Id != IdUserForDel).ToList();

            var json = new JavaScriptSerializer().Serialize(Users);

            using (var file = File.Create("List.txt"))
            {
                using (var sw = new StreamWriter(file))
                {
                    Users.ForEach(x => sw.WriteLine(
                        new JavaScriptSerializer().Serialize(x)
                        ));
                }

            }



        }
        static Users CreateUsers(int mod)
        {
            var users = GetUsers();

            var sol = new Users();
            Console.WriteLine("Create your login, please");
            sol.Login = Console.ReadLine();
            Console.WriteLine("Create your password, please");
            sol.Password = GetPassword();


            
            var MaxId = users.Max(x => x.Id);
            if (MaxId == 0) mod = 1;
            sol.Id = ++MaxId;
            if (mod == 0)
                sol.Role = Roles.User;
            else
                sol.Role = Roles.Admin;

            var json = new JavaScriptSerializer().Serialize(sol);

            using (var file = File.Open("List.txt", FileMode.Append))
            {
                using (var sw = new StreamWriter(file))
                {
                    sw.WriteLine(json);
                }

            }

            return sol;

        }


        static void ShowUsers()
        {
            GetUsers().ForEach(x => Console.WriteLine($"Login: {x.Login}, Password: {x.Password}, ID: {x.Id}, Role: {x.Role}"));
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

        static Users Login()
        {
            Console.WriteLine("Enter your LOGIN?");
            var login = Console.ReadLine();
            Console.WriteLine("Enter your PASSWORD?");
            var password = GetPassword();



            var users = GetUsers();

            var user = users.FirstOrDefault(curUser => curUser.Login == login && curUser.Password == password);

            if (user == null)
            {
                Console.WriteLine("Mistake!!!!!!!!!!");
            }
            else
            {
                Console.WriteLine($"\nOK, {login}, HELLO!!!!");
            }

            return user;
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
