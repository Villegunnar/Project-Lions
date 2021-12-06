using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Project_Lions
{
    class Program
    {
        static void Main(string[] args)
        {
            User Viktor = new User("viktor", "viktor123");
            Viktor.Accounts.AddRange(new List<Account> { new Account { Balance = 100, Name = "Lönekonto", Currency = "SEK" }, new Account { Balance = 2000, Name = "Investeringskoto", Currency = "SEK" } });
            User Lukas = new User("lukas", "lukas123");
            Lukas.Accounts.AddRange(new List<Account> { new Account { Balance = 200, Name = "Lönekonto", Currency = "SEK" }, new Account { Balance = 3000, Name = "Ölkonto", Currency = "SEK" } });
            User Erik = new User("erik", "erik123");
            Erik.Accounts.AddRange(new List<Account> { new Account { Balance = 300, Name = "Lönekonto", Currency = "SEK" }, new Account { Balance = 1003, Name = "Eventkonto", Currency = "SEK" } });
            Admin Anas = new Admin("Anas", "Anas123", true);
            System.AllUsers.Add(Erik);
            System.AllUsers.Add(Viktor);
            System.AllUsers.Add(Lukas);
            System.AllUsers.Add(Anas);
            System.Init();
        }
    }
}
