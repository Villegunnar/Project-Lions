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
                                CenterColor($"Kontot {user.Username} är nu låst", false, "Red");
                                Return();
                                user.LockedOut = true;
                            }
                            return false;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        CenterColor($"Kontot {user.Username} är låst", false, "Red");
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
                    CenterColor("Felaktigt lösenord eller användarnamn.", false, "Red");
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
        public static void AdminMenu(User admin)
        {

            bool adminLoggedin = true;        
            Console.WriteLine("Välkommen " + admin.Username);
            Console.WriteLine("Inloggad som admin");
            Console.WriteLine();
            while (adminLoggedin)
            {
                Console.WriteLine("[1] Se alla kunder");
                Console.WriteLine("[2] Registrera ny kund");
                Console.WriteLine("[3] Ändra valutakurs");
                Console.WriteLine("[4] Logga ut");
                int adminmenu;
                int.TryParse(Console.ReadLine(), out adminmenu);
                switch (adminmenu)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Alla användare");
                        foreach (User allusers in BankSystem.AllUsers)
                        {
                            Console.WriteLine(allusers);
                        }
                        Return();
                        break;
                    case 2:
                        UserFactory.CreateNewUser();
                        break;
                    case 3:
                        Console.WriteLine("ändra valutakurs");
                        Admin.CurrencyRates();
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
                //BankSystem.PrintYellow($"Välkommen {user.Username}");
                BankSystem.CenterColor($"Välkommen {user.Username}", true, "Yellow");
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
                    CenterColor("------LION BANK INC------", true,"Yellow");

                    //Console.WriteLine("------LION BANK INC------");
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
        public static void Return()
        {

            CenterColor("\nTryck Enter för att återgå", false, "Cyan");
            Console.ReadLine();
            Console.Clear();
        }
        public static decimal CurrencyConverter(string FromCurrency, string ToCurrency, decimal convertAmount)
        {
            if (FromCurrency == "SEK")
            {
                if (ToCurrency == "USD")
                {
                    decimal dollarResult = convertAmount * (Admin.SekRate / Admin.DollarRate);
                    return dollarResult;
                }
                else if (ToCurrency == "EUR")
                {
                    decimal euroResult = convertAmount * (Admin.SekRate / Admin.EurRate);
                    return euroResult;
                }
            }
            if (FromCurrency == "USD")
            {
                if (ToCurrency == "SEK")
                {
                    decimal sekResult = convertAmount * (Admin.DollarRate / Admin.SekRate);
                    return sekResult;
                }
                else if (ToCurrency == "EUR")
                {
                    decimal euroResult = convertAmount * (Admin.DollarRate / Admin.EurRate);
                    return euroResult;
                }
            }
            if (FromCurrency == "EUR")
            {
                if (ToCurrency == "SEK")
                {
                    decimal eurResult = convertAmount * (Admin.EurRate / Admin.SekRate);
                    return eurResult;
                }
                else if (ToCurrency == "USD")
                {
                    decimal usdResult = convertAmount * (Admin.EurRate / Admin.DollarRate);
                    return usdResult;
                }
            }
            return 0;
        }
        public static bool CheckInterest()
        {
            decimal varIntRate = 0.006m;
            decimal fixIntRate1 = 0.011m;
            decimal fixIntRate2 = 0.014m;
            Console.Clear();
            //BankSystem.PrintYellow("BERÄKNA RÄNTA, SPARKONTO\n\n");
            CenterColor("BERÄKNA RÄNTA, SPARKONTO\n\n", true, "Yellow");
            Console.Write("Skriv ett exempel på hur mycket pengar du vill sätta in: ");
            decimal saveAccAmount = 0;
            string saveAccIn = BankSystem.ShowInput();
            while (!decimal.TryParse(saveAccIn, out saveAccAmount) ^ saveAccIn == "ESC")
            {
                BankSystem.ClearLine();
                Console.Write("Skriv ett exempel på hur mycket pengar du vill sätta in: ");
                saveAccIn = BankSystem.ShowInput();
            }
            if (saveAccIn != "ESC")
            {
                Console.Write("Exempel på hur många år du kommer att spara: ");
                int numberOfYears = 0;
                string yearNumIn = BankSystem.ShowInput();
                while (!int.TryParse(yearNumIn, out numberOfYears) ^ yearNumIn == "ESC")
                {
                    BankSystem.ClearLine();
                    Console.Write("Exempel på hur många år du kommer att spara: ");
                    yearNumIn = BankSystem.ShowInput();
                }
                if (yearNumIn != "ESC")
                {
                    Console.Clear();
                    //BankSystem.PrintYellow("SPARKONTO- OCH RÄNTEINFORMATION\n");
                    CenterColor("SPARKONTO- OCH RÄNTEINFORMATION\n", true, "Yellow");
                    Console.WriteLine("[1] Fasträntekonto, 1,10 % ränta årsbasis, bindningstid 1år" +
                                   $"\n    På {numberOfYears} år kommer dina {saveAccAmount}kr att bli {CalcInterest(fixIntRate1, numberOfYears, saveAccAmount)} kr" +
                                  "\n\n[2] Fasträntekonto, 1,40 % ränta årsbasis, bindningstid 2år." +
                                   $"\n    På {numberOfYears} år kommer dina {saveAccAmount}kr att bli {CalcInterest(fixIntRate2, numberOfYears, saveAccAmount)} kr" +
                                  "\n\n[3] Rörligträntekonto, aktuellränta: 0,60 % årsbasis, ingen bindningstid" +
                                   $"\n    På {numberOfYears} år kommer dina {saveAccAmount}kr att bli {CalcInterest(varIntRate, numberOfYears, saveAccAmount)} kr");
                    return false;
                }
            }
            return true;
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
            bool loop = true;
            do
            {
                Console.WriteLine("Välj kontots valuta: ");
                Console.WriteLine("[1] SEK\n[2] USD\n[3] EUR");
                ConsoleKey select = Console.ReadKey().Key;
                switch (select)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        loop = false;
                        return "SEK";
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        loop = false;
                        return "USD";
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        loop = false;
                        return "EUR";
                    case ConsoleKey.Escape:
                        loop = false;
                        return "ESC";
                    default:
                        return "";
                }
            } while (loop);
        }

        public static void CenterColor(string text, bool center, string color = "White")
        {
            if (center)
            {
                Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            }
            if (color == "Yellow")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (color == "Cyan")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            if (color == "DarkYellow")
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            if (color == "Red")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (color == "Green")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            if (color == "White")
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
