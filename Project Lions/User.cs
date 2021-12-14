using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project_Lions
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<Account> Accounts { get; set; }
        public int LoginCounter { get; set; }
        public bool LockedOut { get; set; }
        public decimal LoanSum { get; set; }
        public List<string> Log { get; set; }
        public User(string tempUsername, string tempPassword)
        {
            this.Username = tempUsername;
            this.Password = tempPassword;
            this.IsAdmin = false;
            Accounts = new List<Account>();
            Log = new List<string>();
        }
        public override string ToString()
        {
            return ("Användarnamn: " + Username + "    Lösenord: " + Password + "    Inloggnings försök: " + LoginCounter);
        }
        public void DisplayAccounts()
        {
            Console.Clear();
            BankSystem.CenterColor("KONTOÖVERSIKT\n", true, "Yellow");
            foreach (Account item in Accounts)
            {
                Console.WriteLine(item);
            }
            if (LoanSum > 0)
            {
                Console.WriteLine("\nAktuellt lån: " + LoanSum + " SEK");
            }
            BankSystem.Return();
        }
        public void TransferBalance()
        {
            bool transfer = true;
            while (transfer)
            {
                Console.Clear();
                BankSystem.CenterColor("ÖVERFÖRING\n", true, "Yellow");
                Console.WriteLine("[1] Överföring mellan egna konton");
                Console.WriteLine("[2] Överföring till andra kunder i Lion Bank INC");
                var keyInfo = Console.ReadKey(intercept: true);
                ConsoleKey transferTo = keyInfo.Key;
                if (transferTo == ConsoleKey.D1 || transferTo == ConsoleKey.NumPad1)
                {
                    InternalTransfer();
                    transfer = false;
                    BankSystem.Return();
                }
                else if (transferTo == ConsoleKey.D2 || transferTo == ConsoleKey.NumPad2)
                {
                    transfer = ExternalTransfer();
                }
                else if (transferTo == ConsoleKey.Escape)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ogiltigt alternativ");
                }
            }
        }
        public void InternalTransfer()
        {
            int moveFrom = 0, moveTo = 0;
            decimal amount;
            Console.Clear();
            BankSystem.CenterColor("ÖVERFÖRING MELLAN EGNA KONTON", true, "Yellow");
            BankSystem.CenterColor("Dina konton:\n", false, "DarkYellow");
            for (int i = 0; i < Accounts.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {Accounts[i]}");
            }
            Console.Write("\nAnge vilket konto du vill föra över FRÅN: ");
            while (!(moveFrom <= Accounts.Count) || moveFrom < 1)
            {
                var keyInfo = Console.ReadKey(intercept: true);
                if (char.IsDigit(keyInfo.KeyChar))
                {
                    if (int.Parse(keyInfo.KeyChar.ToString()) <= Accounts.Count && int.Parse(keyInfo.KeyChar.ToString()) > 0)
                    {
                        moveFrom = int.Parse(keyInfo.KeyChar.ToString());
                        Console.WriteLine(Accounts[moveFrom - 1].Name);
                    }
                }
            }
            Console.Write("Ange vilket konto du vill föra över TILL: ");
            while (!(moveTo <= Accounts.Count) || moveTo < 1 || moveTo == moveFrom)
            {
                var keyInfo = Console.ReadKey(intercept: true);
                if (char.IsDigit(keyInfo.KeyChar))
                {
                    if (int.Parse(keyInfo.KeyChar.ToString()) <= Accounts.Count && int.Parse(keyInfo.KeyChar.ToString()) > 0 && int.Parse(keyInfo.KeyChar.ToString()) != moveFrom)
                    {
                        moveTo = int.Parse(keyInfo.KeyChar.ToString());
                        Console.WriteLine(Accounts[moveTo - 1].Name);
                    }
                }
            }
            Console.Write("Skriv in hur mycket du vill föra över och tryck Enter: ");
            while ((!decimal.TryParse(Console.ReadLine(), out amount)) || amount < 0 || amount > Accounts[moveFrom - 1].Balance)
            {
                BankSystem.ClearLine();
                Console.Write("Ogiltig Input. Skriv in hur mycket du vill föra över: ");
            }
            if (Accounts[moveTo - 1].Currency == Accounts[moveFrom - 1].Currency)
            {
                Accounts[moveFrom - 1].Balance -= amount;
                Accounts[moveTo - 1].Balance = Accounts[moveTo - 1].Balance + amount;
            }
            else
            {
                Accounts[moveFrom - 1].Balance -= amount;
                Accounts[moveTo - 1].Balance += BankSystem.CurrencyConverter(Accounts[moveFrom - 1].Currency, Accounts[moveTo - 1].Currency, amount);
            }
            Console.Clear();
            BankSystem.CenterColor("ÖVERFÖRING MELLAN EGNA KONTON\n", true, "Yellow");
            BankSystem.CenterColor($"Transaktion lyckad!\n{amount} {Accounts[moveFrom - 1].Currency} fördes över från {Accounts[moveFrom - 1].Name} till {Accounts[moveTo - 1].Name}", false, "Green");
            Log.Add($"\n{DateTime.Now} \n{amount} {Accounts[moveFrom - 1].Currency} fördes över från {Accounts[moveFrom - 1].Name} till {Accounts[moveTo - 1].Name}");
        }
        public bool ExternalTransfer()
        {
            Console.Clear();
            DateTime today = DateTime.Now;
            DateTime SpecTime1 = new DateTime(today.Year, today.Month, today.Day, today.Hour, 15, 00);
            DateTime SpecTime2 = new DateTime(today.Year, today.Month, today.Day, today.Hour, 30, 00);
            DateTime SpecTime3 = new DateTime(today.Year, today.Month, today.Day, today.Hour, 45, 00);
            DateTime SpecTime4 = new DateTime(today.Year, today.Month, today.Day, today.Hour + 1, 00, 00);
            DateTime time = new DateTime();
            TimeSpan timeUntilExe = SpecTime1 - DateTime.Now;
            decimal amount;
            BankSystem.CenterColor("ÖVERFÖRING TILL ANNAN ANVÄNDARE\n", true, "Yellow");
            Console.Write("Skriv in användaren du vill föra över till: ");
            string usernameSearch = Console.ReadLine();
            bool notEnoughMoney = true;
            bool userFound = false;
            int index = -1;
            for (int i = 0; i < BankSystem.AllUsers.Count; i++)
            {
                if (BankSystem.AllUsers[i].Username == usernameSearch)
                {
                    userFound = true;
                    index = i;
                    break;
                }
            }
            if (userFound)
            {
                while (notEnoughMoney)
                {
                    int moveFrom = 0;
                    Console.Clear();
                    BankSystem.CenterColor("ÖVERFÖRING TILL ANNAN ANVÄNDARE", true, "Yellow");
                    BankSystem.CenterColor($"Användare {usernameSearch} hittad!\n", false, "DarkYellow");
                    for (int i = 0; i < Accounts.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {Accounts[i]}");
                    }
                    Console.WriteLine("\nAnge vilket konto du vill föra över FRÅN: ");
                    Console.SetCursorPosition(43, Accounts.Count + 4);
                    while (!(moveFrom <= Accounts.Count) || moveFrom < 1)
                    {
                        var keyInfo = Console.ReadKey(intercept: true);
                        if (char.IsDigit(keyInfo.KeyChar))
                        {
                            if (int.Parse(keyInfo.KeyChar.ToString()) <= Accounts.Count && int.Parse(keyInfo.KeyChar.ToString()) > 0)
                            {
                                moveFrom = int.Parse(keyInfo.KeyChar.ToString());
                                Console.WriteLine(Accounts[moveFrom - 1].Name);
                            }
                        }
                    }
                    Console.Write("Skriv in hur mycket du vill föra över och tryck Enter: ");
                    decimal.TryParse(Console.ReadLine(), out amount);
                    if (Accounts[moveFrom - 1].Balance >= amount)
                    {
                        if (BankSystem.AllUsers[index].Accounts[0].Currency == Accounts[moveFrom - 1].Currency)
                        {
                            if (DateTime.Now <= SpecTime1)
                            {
                                timeUntilExe = SpecTime1 - DateTime.Now;
                                time = SpecTime1;
                            }
                            else if (DateTime.Now <= SpecTime2)
                            {
                                timeUntilExe = SpecTime2 - DateTime.Now;
                                time = SpecTime2;
                            }
                            else if (DateTime.Now <= SpecTime3)
                            {
                                timeUntilExe = SpecTime3 - DateTime.Now;
                                time = SpecTime3;
                            }
                            else if (DateTime.Now <= SpecTime4)
                            {
                                timeUntilExe = SpecTime4 - DateTime.Now;
                                time = SpecTime4;
                                Accounts[moveFrom - 1].Balance -= amount;
                                Task.Delay(timeUntilExe).ContinueWith((x) => BankSystem.AllUsers[index].Accounts[0].Balance += amount);
                                var now = DateTime.Now;
                                Task.Delay(timeUntilExe).ContinueWith((x) => Log.Add($"\n{time} \n{amount} {Accounts[moveFrom - 1].Currency} överfördes från {Accounts[moveFrom - 1].Name} till {BankSystem.AllUsers[index].Username}"));
                            }
                            else
                            {
                                if (DateTime.Now <= SpecTime1)
                                {
                                    timeUntilExe = SpecTime1 - DateTime.Now;
                                    time = SpecTime1;
                                }
                                else if (DateTime.Now <= SpecTime2)
                                {
                                    timeUntilExe = SpecTime2 - DateTime.Now;
                                    time = SpecTime2;
                                }
                                else if (DateTime.Now <= SpecTime3)
                                {
                                    timeUntilExe = SpecTime3 - DateTime.Now;
                                    time = SpecTime3;
                                }
                                else if (DateTime.Now <= SpecTime4)
                                {
                                    timeUntilExe = SpecTime4 - DateTime.Now;
                                    time = SpecTime4;
                                }
                                Accounts[moveFrom - 1].Balance -= amount;
                                Task.Delay(timeUntilExe).ContinueWith((x) => BankSystem.AllUsers[index].Accounts[0].Balance += BankSystem.CurrencyConverter(Accounts[moveFrom - 1].Currency, BankSystem.AllUsers[index].Accounts[0].Currency, amount));
                                var now = DateTime.Now;
                                Task.Delay(timeUntilExe).ContinueWith((x) => Log.Add($"\n{SpecTime1} \n{amount} {Accounts[moveFrom - 1].Currency} överfördes från {Accounts[moveFrom - 1].Name} till {BankSystem.AllUsers[index].Username}"));
                            }
                            notEnoughMoney = false;
                            Console.Clear();

                            BankSystem.CenterColor("ÖVERFÖRING TILL ANNAN ANVÄNDARE\n", true, "Yellow");
                            BankSystem.CenterColor($"Överföring begärd.\n\n{amount} {Accounts[moveFrom - 1].Currency} överförs från {Accounts[moveFrom - 1].Name} till {BankSystem.AllUsers[index].Username} klockan {time}.\n", false, "Green");
                            Log.Add($"\n{DateTime.Now} \nÖverföring begärd: {amount} {Accounts[moveFrom - 1].Currency} från {Accounts[moveFrom - 1].Name} till {BankSystem.AllUsers[index].Username}");
                            BankSystem.Return();
                            return false;
                        }
                        else
                        {

                            BankSystem.CenterColor("Du har för lite pengar", false, "Red");
                            notEnoughMoney = true;
                            BankSystem.Return();
                        }
                    }
                }
            }
            else
            {
                BankSystem.CenterColor("Användarnamn kunde ej hittas", false, "Red");
                BankSystem.Return();
            }
            return true;
        }
        public void OpenNewBankAccount()
        {
            ConsoleKey select;
            bool loop = true, escape = false;
            do
            {
                Console.Clear();
                //BankSystem.PrintYellow("SKAPA NYTT KONTO\n");
                BankSystem.CenterColor("SKAPA NYTT KONTO\n", true, "Yellow");
                Console.WriteLine("[1] Öppna ett vanligt konto\n[2] Öppna ett sparkonto\n[3] Beräkna med vår ränta hur mycket du kan tjäna på ditt sparkonto");
                var keyInfo = Console.ReadKey(intercept: true);
                select = keyInfo.Key;
                switch (select)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        Console.Clear();
                        BankSystem.CenterColor("SKAPA NYTT KONTO\n", true, "Yellow");
                        //BankSystem.PrintDarkYellow("Vald Kontotyp: Vanligt Konton\n");
                        BankSystem.CenterColor("Vald Kontotyp: Vanligt Konton\n",false,"DarkYellow");
                        Console.Write("Skriv in önskat namn på ditt nya konto: ");
                        string regAccountName = BankSystem.ShowInput();
                        if (regAccountName != "ESC")
                        {
                            Console.Clear();
                            BankSystem.CenterColor("SKAPA NYTT KONTO\n", true, "Yellow");
                            BankSystem.CenterColor("Vald Kontotyp: Vanligt Konton\n", false, "DarkYellow");
                            string tempCurrency = BankSystem.ChooseCurrency();
                            if (tempCurrency != "ESC")
                            {
                                Accounts.Add(new Account { Balance = 0, Name = regAccountName, Currency = tempCurrency });
                                Console.Clear();
                                BankSystem.CenterColor("SKAPA NYTT KONTO\n", true, "Yellow");
                                BankSystem.CenterColor("Vald Kontotyp: Vanligt Konton\n", false, "DarkYellow");
                                Console.WriteLine($"Kontot {regAccountName} har skapats!");
                                Log.Add($"\n{DateTime.Now} \nKontot {regAccountName} skapades, som håller valutan {Accounts[Accounts.Count - 1].Currency}");
                                loop = false;
                            }
                            else
                            {
                                escape = true;
                            }
                        }
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        Console.Clear();
                        //BankSystem.PrintYellow("SKAPA NYTT SPARKONTO\n");
                        BankSystem.CenterColor("SKAPA NYTT SPARKONTO\n", true, "Yellow");
                        Console.Write("Välj typ av Sparkonto:" +
                                  "\n\n[1] Fasträntekonto, 1,10 % ränta årsbasis, bindningstid 1år" +
                                    "\n[2] Fasträntekonto, 1,40 % ränta årsbasis, bindningstid 2år" +
                                    "\n[3] Rörligt räntekonto, aktuellränta: 0,60 % årsbasis, ingen bindningstid");
                        decimal tempInterest = 0;
                        bool intrLoop = true;
                        string accType = "";
                        select = Console.ReadKey(intercept: true).Key;
                        do
                        {
                            switch (select)
                            {
                                case ConsoleKey.NumPad1:
                                case ConsoleKey.D1:
                                    accType = "Fasträntekonto, 1,10 % ränta/år, bindningstid 1år";
                                    tempInterest = 0.011m;
                                    intrLoop = false;
                                    break;
                                case ConsoleKey.NumPad2:
                                case ConsoleKey.D2:
                                    accType = "Fasträntekonto, 1,40 % ränta årsbasis, bindningstid 2år";
                                    tempInterest = 0.014m;
                                    intrLoop = false;
                                    break;
                                case ConsoleKey.NumPad3:
                                case ConsoleKey.D3:
                                    accType = "Rörligt räntekonto, aktuellränta: 0,60 % årsbasis, ingen bindningstid";
                                    tempInterest = 0.006m;
                                    intrLoop = false;
                                    break;
                                case ConsoleKey.Escape:
                                    intrLoop = false;
                                    break;
                                default:
                                    break;
                            }
                        } while (intrLoop);
                        if (select != ConsoleKey.Escape)
                        {
                            Console.Clear();
                            //BankSystem.PrintYellow($"SKAPA NYTT SPARKONTO");
                            BankSystem.CenterColor("SKAPA NYTT SPARKONTO\n", true, "Yellow");
                            //BankSystem.PrintDarkYellow($"Vald kontotyp: {accType}\n");
                            BankSystem.CenterColor($"Vald kontotyp: {accType}\n", false, "DarkYellow");
                            Console.Write($"Skriv in önskat namn på ditt nya sparkonto: ");
                            string saveAccountName = BankSystem.ShowInput();
                            if (saveAccountName != "ESC")
                            {
                                Console.Clear();
                                //BankSystem.PrintYellow($"SKAPA NYTT SPARKONTO");
                                BankSystem.CenterColor("SKAPA NYTT SPARKONTO\n", true, "Yellow");
                                BankSystem.CenterColor($"Vald kontotyp: {accType}\n", false, "DarkYellow");
                                string tempCurrency = BankSystem.ChooseCurrency();
                                if (tempCurrency != "ESC")
                                {
                                    Accounts.Add(new Account { Balance = 0, Name = saveAccountName, Currency = tempCurrency, Interest = tempInterest });
                                    Console.Clear();
                                    Console.Clear();
                                    BankSystem.CenterColor("SKAPA NYTT SPARKONTO\n", true, "Yellow");
                                    BankSystem.CenterColor($"Vald kontotyp: {accType}\n", false, "DarkYellow");
                                    Console.WriteLine($"Sparkontot {saveAccountName} har skapats!");
                                    Log.Add($"\n{DateTime.Now} \nSparkontot {saveAccountName} skapades, som håller valutan {Accounts[Accounts.Count - 1].Currency} och har en ränta på {decimal.Round((tempInterest * 100), 2)}%");
                                    loop = false;
                                }
                                else
                                {
                                    escape = true;
                                }
                            }
                        }
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        escape = BankSystem.CheckInterest();
                        if (!escape)
                        {
                            BankSystem.Return();
                        }
                        break;
                    case ConsoleKey.Escape:
                        loop = false;
                        break;
                    default:
                        break;
                }
            } while (loop);
            if (select != ConsoleKey.Escape && !escape)
            {
                BankSystem.Return();
            }
        }
        public void Loans()
        {
            Console.Clear();
            decimal LoanInput;
            decimal intresteamount = 0;
            decimal IntrestCal = 0;
            decimal total = 0;
            decimal accountSum = 0;
            Console.Write("Skriv in hur mycket du vill låna: ");
            decimal.TryParse(Console.ReadLine(), out LoanInput);
            Console.Clear();
            foreach (Account u in Accounts)
            {
                accountSum = accountSum + u.Balance;
            }
            if (accountSum * 5 < LoanInput + LoanSum)
            {
                Console.Clear();
                Console.WriteLine("Du har för lite pengar för att ta detta lån.");
                Console.WriteLine("Vänligen kontakta vår personal för att se över dina möjligheter");
                BankSystem.Return();
            }
            else
            {
                if (LoanInput < 100000)
                {
                    IntrestCal = 1.05m;
                    intresteamount = 5m;
                    total = LoanInput * IntrestCal;
                }
                else if (LoanInput >= 100000)
                {
                    IntrestCal = 1.02m;
                    intresteamount = 2m;
                    total = LoanInput * IntrestCal;
                }
                else if (LoanInput >= 500000)
                {
                    IntrestCal = 1.015m;
                    intresteamount = 1.5m;
                    total = LoanInput * IntrestCal;
                }
                Console.WriteLine("Din totala kostnad för ett lån på " + LoanInput + " blir " + total);
                Console.WriteLine("Du har då en ränta på " + intresteamount + "%\n");
                bool loanCon = true;
                while (loanCon)
                {
                    if (accountSum * 5 > LoanInput + LoanSum)
                    {
                        Console.WriteLine("För att bekräfta ett nytt lån på " + total + " skriv in ditt lösenord: ");
                        Console.WriteLine("För att avbryta tryck [Q]");
                        string pass = BankSystem.HideInput();
                        if (pass == Password)
                        {
                            Console.Clear();
                            Console.WriteLine($"Grattis till ditt lån. Pengarna finns nu tillgängliga på {Accounts[0].Name}");
                            LoanSum = LoanSum + total;
                            Accounts[0].Balance += LoanInput;
                            Log.Add($"{DateTime.Now} \nEtt lån togs på {total} SEK. Din totala lånsumma blev då {LoanSum} SEK\n");
                            BankSystem.Return();
                            loanCon = false;
                        }
                        else if (pass.ToUpper() == "Q")
                        {
                            Console.Clear();
                            loanCon = false;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Fel lösenord, försök igen....");
                        }
                    }
                }
            }
        }
        public void PrintLog()
        {
            Console.Clear();
            //BankSystem.PrintYellow("KONTOHISTORIK");
            BankSystem.CenterColor("KONTOHISTORIK\n", true, "Yellow");
            if (Log.Count > 0)
            {
                BankSystem.CenterColor("Alla föregående transaktioner, senaste till första:", false, "DarkYellow");
                for (int i = Log.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine(Log[i]);
                }
            }
            else
            {
                Console.WriteLine("Du har inte utfört några handlingar på denna Användare.");
            }
            BankSystem.Return();
        }
    }
}
