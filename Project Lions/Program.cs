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

            System.Allusers.Add(Viktor);
            System.Allusers.Add(Lukas);
            System.Allusers.Add(Erik);


            System.LogIn();
        }
    }
}
