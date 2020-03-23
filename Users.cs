using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Users
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public Roles Role { get; set; }


        public override string ToString()
        {
            return $"Login: {Login}, Password: {Password}, ID: {Id}, Role: {Role}";
        }

    }

    public enum Roles
    {

        User = 1,
        Admin = 2

    }
}
