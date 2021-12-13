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
        static BankSystem()
        {
            User Viktor = new User("viktor", "viktor123");
            Viktor.Accounts.AddRange(new List<Account> { new Account { Balance = 100, Name = "Lönekonto", Currency = "SEK" }, new Account { Balance = 2000, Name = "Investeringskoto", Currency = "SEK" } });
            User Lukas = new User("lukas", "lukas123");
            Lukas.Accounts.AddRange(new List<Account> { new Account { Balance = 200, Name = "Lönekonto", Currency = "SEK" }, new Account { Balance = 3000, Name = "Ölkonto", Currency = "SEK" } });
            User Erik = new User("erik", "erik123");
            Erik.Accounts.AddRange(new List<Account> { new Account { Balance = 300, Name = "Lönekonto", Currency = "SEK" }, new Account { Balance = 1003, Name = "Eventkonto", Currency = "SEK" } });
            User Anas = new Admin("anas", "anas123", true);
            AllUsers.Add(Erik);
            AllUsers.Add(Viktor);
            AllUsers.Add(Lukas);
            AllUsers.Add(Anas);
        }
        public static bool PassCheck(string userTry, string passTry)
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
                            if (user.IsAdmin)
                            {
                                AdminMenu(user);
                                return true;
                            }
                            else
                            {
                                MainMenu(user);
                                return true;
                            }
                        }
                        else
                        {
                            user.LoginCounter++;
                            if (user.LoginCounter > 2)
                            {
                                Console.Clear();
                                PrintRed($"Kontot {user.Username} är nu låst");                                
                                Return();
                                user.LockedOut = true;
                            }
                            return false;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        PrintRed($"Kontot {user.Username} är låst");
                        Return();
                        return false;
                    }
                }
            }
            return false;
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
            bool loginSuccess = false;
            int tries = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Användarnamn: \nLösenord: \n\nTryck Escape för att återgå");
                if (tries > 0)
                {
                    Console.SetCursorPosition(0, 2);
                    BankSystem.PrintRed("Felaktigt lösenord eller användarnamn.");
                }
                Console.SetCursorPosition(14, 0);
                string usernameInput = ShowInput();
                if (usernameInput.ToUpper() == "ESC")
                {
                    break;
                }
                Console.SetCursorPosition(10, 1);
                string passwordInput = HideInput();
                if (passwordInput.ToUpper() == "ESC")
                {
                    break;
                }
                loginSuccess = PassCheck(usernameInput, passwordInput);
                tries++;
            } while (!loginSuccess);
        }
        public static string HideInput()
        {
            ConsoleKey key;
            string pass = "";
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;
                if (key == ConsoleKey.Escape)
                {
                    return "ESC";
                }
                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            Console.WriteLine();
            return pass;
        }
        public static string ShowInput()
        {
            ConsoleKey key;
            string username = "";
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;



                if (key == ConsoleKey.Escape)
                {
                    return "ESC";
                }



                if (key == ConsoleKey.Backspace && username.Length > 0)
                {
                    Console.Write("\b \b");
                    username = username[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write(keyInfo.KeyChar);
                    username += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            Console.WriteLine();
            return username;
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
        public static void AdminMenu(User admin)//ej klar
        {
            bool adminLoggedin = true;
            while (adminLoggedin)
            {
                Console.WriteLine("Välkommen " + admin.Username);
                Console.WriteLine("Inloggad som admin");
                Console.WriteLine();
                Console.WriteLine("[1] Se alla kunder");
                Console.WriteLine("[2] Registrera ny kund");
                Console.WriteLine("[3] Ändra valutakurs");
                Console.WriteLine("[4] Logga ut");
                int adminmenu;
                int.TryParse(Console.ReadLine(), out adminmenu);
                switch (adminmenu)
                {
                    case 1:
                        foreach (User allusers in BankSystem.AllUsers)
                        {
                            Console.WriteLine(allusers);
                        }
                        break;
                    case 2:
                        UserFactory.CreateNewUser();
                        break;
                    case 3:
                        Console.WriteLine("ändra valutakurs");
                        break;
                    case 4:
                        Console.Clear();
                        Console.Write("Du loggas ut");
                        for (int j = 0; j < 10; j++)
                        {
                            Console.Write(".");
                            Thread.Sleep(200);
                        }
                        Console.WriteLine();
                        adminLoggedin = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt alternativ");
                        return;
                }
            }
        }
        public static void MainMenu(User user)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.Clear();
                Console.WriteLine($"Välkommen {user.Username}");
                PrintMenu();
                var keyInfo = Console.ReadKey(intercept: true);
                ConsoleKey menuChoice = keyInfo.Key;
                switch (menuChoice)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        user.DisplayAccounts();
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        user.TransferBalance();
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        user.OpenNewBankAccount();
                        break;
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        user.Loans();
                        break;
                    case ConsoleKey.NumPad5:
                    case ConsoleKey.D5:
                        user.PrintLog();
                        break;
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.D6:
                    case ConsoleKey.Escape:
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
                        break;
                }
            }
        }
        public static void Init()
        {
            bool loop = true;
            ConsoleKey selector;
            while (loop)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("------LION BANK INC------");
                    Console.WriteLine();
                    Console.WriteLine("[1]. Logga In");
                    Console.WriteLine("[2]. Avsluta");
                    var keyInfo = Console.ReadKey(intercept: true);
                    selector = keyInfo.Key;
                } while (selector != ConsoleKey.NumPad1 && selector != ConsoleKey.NumPad2 && selector != ConsoleKey.D1 && selector != ConsoleKey.D2);
                if (selector == ConsoleKey.NumPad1 || selector == ConsoleKey.D1)
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
        public static void PrintRed(string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ResetColor();
        }
        public static void Return()
        {
            Console.WriteLine("\nTryck Enter för att återgå");
            Console.ReadLine();
            Console.Clear();
        }
        public static decimal CurrencyConverter(string FromCurrency, string ToCurrency, decimal convertAmount)
        {
            if (FromCurrency == "SEK")
            {
                if (ToCurrency == "USD")
                {
                    decimal dollarResult = convertAmount * Admin.SekToUsd;
                    return dollarResult;
                }
                else if (ToCurrency == "EUR")
                {
                    decimal euroResult = convertAmount * Admin.SekToEur;
                    return euroResult;
                }
            }
            if (FromCurrency == "USD")
            {
                if (ToCurrency == "SEK")
                {
                    decimal sekResult = convertAmount * Admin.UsdToSek;
                    return sekResult;
                }
                else if (ToCurrency == "EUR")
                {
                    decimal euroResult = convertAmount * Admin.UsdToEur;
                    return euroResult;
                }
            }
            if (FromCurrency == "EUR")
            {
                if (ToCurrency == "SEK")
                {
                    decimal eurResult = convertAmount * Admin.EurToSek;
                    return eurResult;
                }
                else if (ToCurrency == "USD")
                {
                    decimal usdResult = convertAmount * Admin.EurToUsd;
                    return usdResult;
                }
            }
            return 0;
        }
        public static void CheckInterest()
        {
            decimal varIntRate = 0.006m;
            decimal fixIntRate1 = 0.011m;
            decimal fixIntRate2 = 0.014m;
            Console.Clear();
            Console.WriteLine("Beräkna ränta på ett sparkonto");
            Console.WriteLine();
            Console.Write("Exempel på hur mycket pengar du vill sätta in: ");
            decimal saveAccAmount;
            while (!decimal.TryParse(Console.ReadLine(), out saveAccAmount))
            {
                BankSystem.ClearLine();
                Console.Write("Exempel på hur mycket pengar du vill sätta in: ");
            }
            Console.Write("Exempel på hur många år du kommer att spara: ");
            int numberOfYears;
            while (!int.TryParse(Console.ReadLine(), out numberOfYears))
            {
                BankSystem.ClearLine();
                Console.Write("Exempel på hur många år du kommer att spara: ");
            }
            Console.Clear();
            Console.WriteLine("[1] Fasträntekonto, 1,10 % ränta årsbasis, bindningstid 1år" +
                $"\nPå {numberOfYears} år kommer dina: {saveAccAmount}kr att bli {CalcInterest(fixIntRate1, numberOfYears, saveAccAmount)} kr" +
                            "\n\n[2] Fasträntekonto, 1,40 % ränta årsbasis, bindningstid 2år." +
                            $"\nPå {numberOfYears} år kommer dina: {saveAccAmount}kr att bli {CalcInterest(fixIntRate2, numberOfYears, saveAccAmount)} kr" +
                            "\n\n[3] Rörligträntekonto, aktuellränta: 0,60 % årsbasis, ingen bindningstid" +
                $"\nPå {numberOfYears} år kommer dina: {saveAccAmount}kr att bli {CalcInterest(varIntRate, numberOfYears, saveAccAmount)} kr");
        }
        public static decimal CalcInterest(decimal rate, int years, decimal sum)
        {
            for (int i = 0; i < years; i++)
            {
                sum = sum * (1 + rate);
            }
            return decimal.Round(sum, 2);
        }
        public static string ChooseCurrency()
        {
            Console.WriteLine("Vilken Valuta: ");
            Console.WriteLine("[1] SEK\n[2] USD\n[3] EUR");
            int currencyChoice;
            while (!int.TryParse(Console.ReadLine(), out currencyChoice) || currencyChoice < 1 || currencyChoice > 3)
            {
                BankSystem.ClearLine();
                Console.Write("Vilken Valuta: ");
            }
            switch (currencyChoice)
            {
                case 1:
                    return "SEK";
                case 2:
                    return "USD";
                case 3:
                    return "EUR";
                default:
                    return "";
            }

        }
    }
}
