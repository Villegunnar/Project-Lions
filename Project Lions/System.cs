using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Project_Lions
{
    public class System
    {

        public static List<User> AllUsers = new List<User>();
        public static List<Admin> Alladmins = new List<Admin>();

        public System(){}

        public static void PassCheck(string userTry, string passTry)
        {
            foreach (User user in AllUsers)
            {
                if (user.Username == userTry)
                {
                    if (!user.LockedOut)
                    {
                        if (user.Password == passTry)
                        {
                            Console.WriteLine("Login lyckades!");
                            user.LoginCounter = 0;
                            Console.Clear();
                            MainMenu(user);
                        }
                        else
                        {
                            Console.WriteLine("Felaktigt lösenord.");
                            user.LoginCounter++;
                            if (user.LoginCounter > 2)
                            {
                                Console.WriteLine("Denna användare är nu låst");
                                user.LockedOut = true;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Denna användare är låst.");
                    }
                }
            }
        }
        public static void ExitAppFunc()
        {
            Console.Clear();
            Console.Write("Stänger ner programmet");
            for (int j = 0; j < 10; j++)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }
            Console.WriteLine();
            Environment.Exit(0);
        }
        //public static void ReturnToMenu()
        //{
        //    Console.Write("Tryck <Q> för att återgå till huvudmenyn");
        //    Console.WriteLine();
        //    while (Console.ReadKey().Key != ConsoleKey.Q) { }
        //}
        public static void LogInMenu()
        {
            Console.WriteLine("Skriv in användarnamn: ");
            string usernameInput = Console.ReadLine();
            Console.WriteLine("Skriv in lösenord: ");
            string passwordInput = Console.ReadLine();

            PassCheck(usernameInput, passwordInput);

        }
        public static void DisplayAccounts()
        { }
        public static void TransferBalance()
        { }
        public static void CreateSavingAcc()
        { }
        public static void Loan()
        { }
        public static void PreviousTransactions()
        { }
        public static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("[1] Se dina konton och saldo");
            Console.WriteLine("[2] Överföring mellan konton");
            Console.WriteLine("[3] skapa sparkonto");
            Console.WriteLine("[4] lån");
            Console.WriteLine("[5] tidigare överföringar");
            Console.WriteLine("[6] Logga ut");
        }
        public static void MainMenu(User user)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine($"Välkommen {user.Username}");
                PrintMenu();
                int menuChoice;

                int.TryParse(Console.ReadLine(), out menuChoice);
                switch (menuChoice)
                {
                    case 1:
                        DisplayAccounts();
                        break;
                    case 2:
                        TransferBalance();
                        break;
                    case 3:
                        CreateSavingAcc();
                        break;
                    case 4:
                        Loan();
                        break;
                    case 5:
                        PreviousTransactions();
                        break;
                    case 6:
                        Console.Clear();
                        Console.Write("Du loggas ut");
                        for (int j = 0; j < 10; j++)
                        {
                            Console.Write(".");
                            Thread.Sleep(200);
                        }
                        Console.WriteLine();
                        loggedIn = false;
                        break;
                    default:
                        Console.WriteLine("Not valid option");
                        return;
                }
            }
        }
        public static void Init()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                LogInMenu();
            }
        }

    }
}
