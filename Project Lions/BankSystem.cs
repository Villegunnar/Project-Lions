using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Project_Lions
{
    public static class BankSystem
    {

        public static List<User> AllUsers = new List<User>();
        public static List<Admin> Alladmins = new List<Admin>();



         static BankSystem()
        {
            User Viktor = new User("viktor", "viktor123");
            Viktor.Accounts.AddRange(new List<Account> { new Account { Balance = 100, Name = "Lönekonto", Currency = "SEK" }, new Account { Balance = 2000, Name = "Investeringskoto", Currency = "SEK" } });
            User Lukas = new User("lukas", "lukas123");
            Lukas.Accounts.AddRange(new List<Account> { new Account { Balance = 200, Name = "Lönekonto", Currency = "SEK" }, new Account { Balance = 3000, Name = "Ölkonto", Currency = "SEK" } });
            User Erik = new User("erik", "erik123");
            Erik.Accounts.AddRange(new List<Account> { new Account { Balance = 300, Name = "Lönekonto", Currency = "SEK" }, new Account { Balance = 1003, Name = "Eventkonto", Currency = "SEK" } });
            Admin Anas = new Admin("Anas", "Anas123", true);
            AllUsers.Add(Erik);
            AllUsers.Add(Viktor);
            AllUsers.Add(Lukas);
            AllUsers.Add(Anas);

        }

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

        public static void LogInMenu()
        {
            Console.Clear();
            Console.WriteLine("Användarnamn: \nLösenord: ");
            Console.SetCursorPosition(14, 0);
            string usernameInput = Console.ReadLine();
            Console.SetCursorPosition(10, 1);
            string passwordInput = Console.ReadLine();

            PassCheck(usernameInput, passwordInput);

        }

        
        public static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("[1] Se dina konton och saldo");
            Console.WriteLine("[2] Överföring mellan konton");
            Console.WriteLine("[3] Skapa nytt konto");
            Console.WriteLine("[4] Lån");
            Console.WriteLine("[5] Tidigare överföringar");
            Console.WriteLine("[6] Logga ut");
        }
        public static void MainMenu(User user)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.Clear();
                Console.WriteLine($"Välkommen {user.Username}");
                PrintMenu();
                int menuChoice;

                int.TryParse(Console.ReadLine(), out menuChoice);
                switch (menuChoice)
                {
                    case 1:
                        user.DisplayAccounts();
                        break;
                    case 2:
                        user.TransferBalance();
                        break;
                    case 3:
                        user.OpenNewBankAccount();
                        break;
                    case 4:
                        user.Loans();
                        break;
                    case 5:
                        user.PrintLog();
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
                        Console.WriteLine("Ogiltigt alternativ");
                        return;
                }
            }
        }
        public static void Init()
        {
            bool loop = true;
            int selector;
            while (loop)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("------LION BANK INC------");
                    Console.WriteLine();
                    Console.WriteLine("[1]. Logga In");
                    Console.WriteLine("[2]. Avsluta");
                } while ((!int.TryParse(Console.ReadLine(), out selector)) && selector != 1 && selector != 2);
                if (selector == 1)
                {
                    {
                        LogInMenu();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write("Programmet stängs ner");
                    for (int j = 0; j < 10; j++)
                    {
                        Console.Write(".");
                        Thread.Sleep(200);
                    }
                    Console.WriteLine();
                    loop = false;
                }
            }
        }
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new String(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
        public static void PrintGreen(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ResetColor();
        }
        public static void Return()
        {
            Console.WriteLine("\nTryck Enter för att återgå till huvudmenyn");
            Console.ReadLine();
            Console.Clear();
        }

    }
}
