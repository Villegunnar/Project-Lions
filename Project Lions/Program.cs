using System;

namespace Project_Lions
{
    class Program
    {
        static void Main(string[] args)
        {
            User Viktor = new User("viktor", "viktor123");
            User Lukas = new User("lukas", "lukas123");
            User Erik = new User("erik", "erik123");
            Admin Anas = new Admin("Anas", "Anas123", true);

            System.AllUsers.Add(Erik);
            System.AllUsers.Add(Viktor);
            System.AllUsers.Add(Lukas);
            System.AllUsers.Add(Anas);


            System.Init();
        }
    }
}
