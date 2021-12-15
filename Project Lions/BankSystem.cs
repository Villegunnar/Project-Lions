using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Project_Lions
{
    public static class BankSystem
    {
        public static List<Customer> AllCustomers = new List<Customer>();
        public static List<Admin> AllAdmins = new List<Admin>();
        static BankSystem()
        {
            Customer Viktor = new Customer("Viktor", "Viktor123!", new Account { Balance = 50000, Name = "Lönekonto", Currency = "SEK" });
            Viktor.Accounts.AddRange(new List<Account> { new Account { Balance = 2000, Name = "Investeringskoto", Currency = "SEK" } });
            Customer Lukas = new Customer("Lukas", "Lukas123!", new Account { Balance = 50000, Name = "Lönekonto", Currency = "SEK" });
            Lukas.Accounts.AddRange(new List<Account> { new Account { Balance = 32000, Name = "Ölkonto", Currency = "SEK" } });
            Customer Erik = new Customer("Erik", "Erik123!", new Account { Balance = 50000, Name = "Lönekonto", Currency = "SEK" });
            Erik.Accounts.AddRange(new List<Account> { new Account { Balance = 13003, Name = "Eventkonto", Currency = "SEK" } });
            Admin Anas = new Admin("Anas", "Anas123!");
            Admin Tobias = new Admin("Tobias", "Tobias123!");
            AllCustomers.Add(Erik);
            AllCustomers.Add(Viktor);
            AllCustomers.Add(Lukas);
            AllAdmins.Add(Anas);
            AllAdmins.Add(Tobias);
        }
        public static bool PassCheck(User user, string userTry, string passTry)
        {
            if (user.Username == userTry)
            {
                if (!user.LockedOut)
                {
                    if (user.Password == passTry)
                    {

                        user.LoginCounter = 0;
                        Console.Clear();
                        return true;
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
            return false;
        }
        public static bool UserCheck(string userTry, string passTry)
        {
            foreach (Customer user in AllCustomers)
            {
                if (PassCheck(user, userTry, passTry))
                {
                    LoadingAnimation("Loggar in");
                    MainMenu(user);
                   
                    return true;

                }
            }
            foreach (Admin admin in AllAdmins)
            {
                if (PassCheck(admin, userTry, passTry))
                {
                    LoadingAnimation("Loggar in");
                    AdminMenu(admin);
                    
                    return true;
                }
            }
            return false;
        }
        static void Exit()
        {
            Console.SetCursorPosition(0, 15);
            CenterColor("Programmet stängs ner", true, "Red");
            Console.SetCursorPosition(53, 15);
            Console.ForegroundColor = ConsoleColor.Green;
            for (int j = 0; j < 3; j++)
            {
                Console.Write(".");
                Thread.Sleep(600);
            }

            Environment.Exit(0);
        }
        public static void LogInMenu()
        {
            bool loginSuccess = false;
            int tries = 0;
            do
            {
                Console.Clear();
                CenterColor("INLOGGNING", true, "Yellow");
                Console.WriteLine("Användarnamn: \nLösenord: \n\nTryck Escape för att återgå");
                if (tries > 0)
                {
                    Console.SetCursorPosition(0, 3);  
                    CenterColor("Felaktigt lösenord eller användarnamn.", false, "Red");
                }
                Console.SetCursorPosition(14, 1);
                string usernameInput = ShowInput();
                if (usernameInput.ToUpper() == "ESC")
                {
                    break;
                }
                Console.SetCursorPosition(10, 2);
                string passwordInput = HideInput();
                if (passwordInput.ToUpper() == "ESC")
                {
                    break;
                }
                loginSuccess = UserCheck(usernameInput, passwordInput);
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
        public static void AdminMenu(Admin admin)
        {
            bool adminLoggedin = true;
            while (adminLoggedin)
            {
                Console.Clear();
                CenterColor($"Inloggad som {admin.Username} ", true, "Yellow");
                Console.WriteLine("\n[1] Se alla kunder\n[2] Registrera ny kund\n[3] Ändra valutakurs\n[4] Ändra bankens rörliga ränta\n[5] Logga ut");
                ConsoleKey menuChoice = Console.ReadKey(intercept: true).Key;
                switch (menuChoice)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        Console.Clear();
                        CenterColor("Alla användare", true, "Yellow");
                        Console.WriteLine();
                        foreach (User allusers in AllCustomers)
                        {
                            Console.WriteLine(allusers);
                        }
                        Return();
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        CustomerFactory.CreateNewCustomer();
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        Admin.CurrencyRates();
                        break;
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        Admin.ChangeInterestRate();
                        break;
                    case ConsoleKey.NumPad5:
                    case ConsoleKey.D5:
                    case ConsoleKey.Escape:
                        Console.Clear();
                        LoggingOut();
                        adminLoggedin = false;
                        break;
                    default:
                        return;
                }
            }
        }
        public static void MainMenu(Customer user)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.Clear();
                CenterColor($"Inloggad som {user.Username}", true, "Yellow");
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
                        LoggingOut();
                        loggedIn = false;
                        break;
                    default:
                        break;
                }
            }
        }
        public static void Init()
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 75;

            PrintLogo();
            Console.WindowWidth = 85;
            bool loop = true;
            ConsoleKey selector;
            while (loop)
            {
                do
                {
                    Console.Clear();
                    CenterColor("------LION BANK INC------", true,"Yellow");
                    CenterColor("Kings of the finance jungle", true, "DarkYellow");
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
                    Console.SetCursorPosition(47, 15);
                    CenterColor("Programmet stängs ner", true, "Red");
                    Console.SetCursorPosition(53, 15);
                    Console.ForegroundColor = ConsoleColor.Red;
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write(".");
                        Thread.Sleep(600);
                    }
                    Console.WriteLine();
                    Environment.Exit(0);
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
            decimal varIntRate = Admin.InterestRate;
            decimal fixIntRate1 = 0.011m;
            decimal fixIntRate2 = 0.014m;
            Console.Clear();
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
                    CenterColor("SPARKONTO- OCH RÄNTEINFORMATION\n", true, "Yellow");
                    Console.WriteLine("[1] Fasträntekonto, 1,10 % ränta årsbasis, bindningstid 1år" +
                                   $"\n    På {numberOfYears} år kommer dina {saveAccAmount}kr att bli {CalcInterest(fixIntRate1, numberOfYears, saveAccAmount)} kr" +
                                  "\n\n[2] Fasträntekonto, 1,40 % ränta årsbasis, bindningstid 2år." +
                                   $"\n    På {numberOfYears} år kommer dina {saveAccAmount}kr att bli {CalcInterest(fixIntRate2, numberOfYears, saveAccAmount)} kr" +
                                  $"\n\n[3] Rörligträntekonto, aktuellränta: {decimal.Round((Admin.InterestRate * 100), 2)} årsbasis, ingen bindningstid" +
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
        public static void CenterColor(string text, bool Center, string color = "White")
        {
            if (Center)
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
        public static void PrintLogo()
        {
            string logo = System.IO.File.ReadAllText(@"C:\Users\ville\Source\Repos\Villegunnar\Project-Lions\Project Lions\Resources\TeamLionBankIncLogo.txt");
            Random randy = new Random();
            for (int i = 0; i <= 100; i++)
            {
                Console.SetCursorPosition(30, 5);
                CenterColor($"LADDAR: {i}%",true);
                int delayAmt = randy.Next(1, 11);
                int delay = 0;
                if (delayAmt != 10)
                {
                    delay = randy.Next(5, 51);
                }
                else
                {
                    delay = randy.Next(50, 301);
                }
                Thread.Sleep(delay);
                Console.Clear();
            }
            foreach (char c in logo)
            {
                Console.Write(c);
                if (c != ' ')
                {
                    int delayAmt = randy.Next(1, 11);
                    int delay = 0;
                    if (delayAmt != 10)
                    {
                        delay = randy.Next(1, 4);
                    }
                    else
                    {
                        delay = randy.Next(3, 13);
                    }
                    Thread.Sleep(delay);
                }
            }
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(logo);
                Thread.Sleep(80);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(logo);
                Thread.Sleep(80);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(logo);
                Thread.Sleep(80);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(logo);
                Thread.Sleep(80);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(logo);
                Thread.Sleep(80);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(logo);
                Thread.Sleep(80);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(logo);
                Thread.Sleep(80);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(logo);
                Thread.Sleep(80);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(logo);
                Thread.Sleep(80);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(logo);
                Thread.Sleep(80);
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine(logo);
                Console.SetCursorPosition(33, 37);
                Console.WriteLine("LION BANK INC");
                Console.SetCursorPosition(27, 38);
                Console.WriteLine("Kings of the finance jungle");
                Thread.Sleep(3000);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(logo);
                Thread.Sleep(150);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(logo);
                Thread.Sleep(150);
                Console.Clear();
                Console.WriteLine(logo);
                Thread.Sleep(150);
                Console.Clear();
                Thread.Sleep(400);
            }
        }
        public static void LoadingAnimation(string tempText = "", int tempSleepTime = 10)
        {   
            for (int i = 0; i <= 99; i++)
            {
                Console.SetCursorPosition(0, 15);
                CenterColor($"{tempText}: {i}%   ",true);
                Thread.Sleep(tempSleepTime);

            }
            Console.SetCursorPosition(0, 15);
            CenterColor($"{tempText}: 100%   ",true,"Green");
            Thread.Sleep(1000);
            
            

        } 
        public static void LoggingOut()
        {
            Console.SetCursorPosition(0, 15); 
            CenterColor("Loggas ut", true, "Green");
            Console.SetCursorPosition(47, 15);
            Console.ForegroundColor = ConsoleColor.Green;
            for (int j = 0; j < 3; j++)
            {
                Console.Write(".");
                Thread.Sleep(600);
            }
            
        }

    }
}
