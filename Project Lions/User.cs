using System;
using System.Collections.Generic;
using System.Text;

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
            return ("Username: " + Username + "Password: " + Password + "    inloggnings försök: " + LoginCounter);
        }
        public void DisplayAccounts()
        {
            Console.Clear();
            Console.WriteLine("Dina konton:");
            Console.WriteLine();
            foreach (Account item in Accounts)
            {
                Console.WriteLine(item);
            }
            if (LoanSum > 0)
            {
                Console.WriteLine("Aktuellt lån: " + LoanSum + " SEK");
            }
            Console.WriteLine();
            Console.WriteLine("Tryck enter för att återgå till huvudmenyn:");
            Console.ReadKey();
            Console.Clear();
        }
        public void TransferBalance()
        {
            bool transfer = true;
            while (transfer)
            {
                Console.WriteLine("[1] Överföring mellan egna konton");
                Console.WriteLine("[2] Överföring till andra kunder i Lion Bank INC");
                int transferTo;
                int.TryParse(Console.ReadLine(), out transferTo);
                if (transferTo == 1)
                {
                    int moveFrom, moveTo;
                    decimal amount;
                    Console.Clear();
                    Console.WriteLine("Dina konton:\n");
                    for (int i = 0; i < Accounts.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {Accounts[i]}");
                    }
                    Console.WriteLine();
                    Console.Write("Ange vilket konto du vill föra över FRÅN: ");
                    while (!int.TryParse(Console.ReadLine(), out moveFrom) || moveFrom < 1 || moveFrom > Accounts.Count)
                    {
                        BankSystem.ClearLine();
                        Console.Write("Ogiltig input. Ange vilket konto du vill föra över FRÅN: ");
                    }
                    Console.Write("Ange vilket konto du vill föra över TILL: ");
                    while (!int.TryParse(Console.ReadLine(), out moveTo) || moveTo < 1 || moveTo > Accounts.Count || moveTo == moveFrom)
                    {
                        BankSystem.ClearLine();
                        Console.Write("Ogiltig input. Ange med vilket konto du vill föra över TILL: ");
                    }
                    Console.Write("Skriv in hur mycket du vill föra över: ");
                    while ((!decimal.TryParse(Console.ReadLine(), out amount)) || amount < 0 || amount > Accounts[moveFrom - 1].Balance)
                    {
                        BankSystem.ClearLine();
                        Console.Write("Ogiltig Input. Skriv in hur mycket du vill föra över: ");
                    }
                    if (Accounts[moveTo - 1].Currency == Accounts[moveFrom - 1].Currency)
                    {
                        Accounts[moveFrom - 1].Balance = Accounts[moveFrom - 1].Balance - amount;
                        Accounts[moveTo - 1].Balance = Accounts[moveTo - 1].Balance + amount;
                    }
                    else
                    {
                        Accounts[moveTo - 1].Balance += BankSystem.CurrencyConverter(Accounts[moveFrom - 1].Currency, Accounts[moveTo - 1].Currency, amount);
                        Accounts[moveFrom - 1].Balance -= amount;
                    }
                    Console.Clear();
                    BankSystem.PrintGreen($"Transaktion lyckad!\n\n{amount} {Accounts[moveFrom - 1].Currency} fördes över från {Accounts[moveFrom - 1].Name} till {Accounts[moveTo - 1].Name}\n");
                    Log.Add($"{DateTime.Now} {amount} {Accounts[moveFrom - 1].Currency} fördes över från {Accounts[moveFrom - 1].Name} till {Accounts[moveTo - 1].Name}");
                    Console.WriteLine("Tryck Enter för att återgå till huvudmenyn.");
                    transfer = false;
                    Console.ReadLine();
                    Console.Clear();
                }
                else if (transferTo == 2)
                {
                    decimal transferMoney = 0;
                    Console.WriteLine("Sök på det användarnamnet du vill föra över till.");
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
                        int moveFrom;
                        Console.Clear();
                        Console.WriteLine("Användare hittad.");
                        while (notEnoughMoney)
                        {
                            for (int i = 0; i < Accounts.Count; i++)
                            {
                                Console.WriteLine($"[{i + 1}] {Accounts[i]}");
                            }
                            Console.Write("Ange vilket konto du vill föra över FRÅN: ");
                            while (!int.TryParse(Console.ReadLine(), out moveFrom) || moveFrom < 1 || moveFrom > Accounts.Count)
                            {
                                BankSystem.ClearLine();
                                Console.Write("Ogiltig input. Ange vilket konto du vill föra över FRÅN: ");
                            }
                            Console.WriteLine("Hur mycket pengar vill du för över?");
                            decimal.TryParse(Console.ReadLine(), out transferMoney);
                            if (Accounts[moveFrom - 1].Balance >= transferMoney)
                            {
                                if (BankSystem.AllUsers[index].Accounts[0].Currency == Accounts[moveFrom - 1].Currency)
                                {
                                    BankSystem.AllUsers[index].Accounts[0].Balance += transferMoney;
                                    Accounts[moveFrom - 1].Balance -= transferMoney;
                                }
                                else
                                {
                                    BankSystem.AllUsers[index].Accounts[0].Balance = +BankSystem.CurrencyConverter(Accounts[moveFrom - 1].Currency, BankSystem.AllUsers[index].Accounts[0].Currency, transferMoney);
                                    Accounts[moveFrom - 1].Balance = -transferMoney;
                                }
                                Console.WriteLine(transferMoney + " " + Accounts[moveFrom - 1].Currency + " fördes över till " + BankSystem.AllUsers[index].Username);
                                transfer = false;
                                notEnoughMoney = false;
                                Log.Add($"{DateTime.Now} {transferMoney} fördes över till {BankSystem.AllUsers[index].Username}");
                                BankSystem.Return();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Du har för lite pengar");
                                notEnoughMoney = true;
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("användare ej hittad");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ogiltigt alternativ");
                }
            }
        }
        public void OpenNewBankAccount()
        {
            Console.Clear();
            Console.WriteLine("[1] Öppna ett vanligt konto\n[2] Öppna ett sparkonto\n[3] Beräkna med vår ränta hur mycket du kan tjärna på ditt sparkonto");
            int createAccChoice;
            int.TryParse(Console.ReadLine(), out createAccChoice);
            switch (createAccChoice)
            {
                case 1:
                    Console.WriteLine("Namn på ditt nya konto: ");
                    string regAccountName = Console.ReadLine();
                    Console.Clear();
                    Accounts.Add(new Account { Balance = 0, Name = regAccountName, Currency = BankSystem.ChooseCurrency() });
                    Console.WriteLine("Nytt konto " + regAccountName + " skapat!");
                    Log.Add($"{DateTime.Now} Kontot {regAccountName} skapades, som håller valutan {Accounts[Accounts.Count - 1].Currency}");
                    break;
                case 2:
                    Console.Clear();
                    Console.Write("[1] Fasträntekonto, 1,10 % ränta årsbasis, bindningstid 1år" +
                        "\n[2] Fasträntekonto, 1,40 % ränta årsbasis, bindningstid 2år." +
                        "\n[3] Rörligträntekonto, aktuellränta: 0,60 % årsbasis, ingen bindningstid" +
                        "\n\nVälj typ av sparkonto: ");
                    int choice;
                    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                    {
                        BankSystem.ClearLine();
                        Console.Write("Välj typ av sparkonto: ");
                    }
                    decimal tempInterest = 0;
                    switch (choice)
                    {
                        case 1:
                            tempInterest = 0.011m;
                            break;
                        case 2:
                            tempInterest = 0.014m;
                            break;
                        case 3:
                            tempInterest = 0.006m;
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine("Namn på ditt nya sparkonto: ");
                    string saveAccountName = Console.ReadLine();
                    Console.Clear();
                    Accounts.Add(new Account { Balance = 0, Name = saveAccountName, Currency = BankSystem.ChooseCurrency(), Interest = tempInterest });
                    Console.WriteLine("Nytt konto " + saveAccountName + " skapat!");
                    Log.Add($"{DateTime.Now} Sparkontot {saveAccountName} skapades, som håller valutan {Accounts[Accounts.Count - 1].Currency} och har en ränta på {decimal.Round((tempInterest * 100), 2)}%");
                    break;
                case 3:
                    BankSystem.ChechInterest();
                    break;
                default:
                    Console.WriteLine("Ogiltigt alternativ");
                    Console.ReadLine();
                    return;
            }
            Console.WriteLine();
            Console.WriteLine("Tryck enter för att återgå till huvudmenyn:");
            Console.ReadKey();
            Console.Clear();
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
            foreach (Account u in Accounts)
            {
                accountSum = accountSum + u.Balance;
            }
            if (accountSum * 5 < LoanInput + LoanSum)
            {
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
                Console.WriteLine("\nDin totala kostnad för ett lån på " + LoanInput + " blir " + total);
                Console.WriteLine("Du har då en ränta på " + intresteamount + "%\n");
                bool loanCon = true;
                while (loanCon)
                {
                    if (accountSum * 5 > LoanInput + LoanSum)
                    {
                        Console.WriteLine("För att bekräfta ett nytt lån på " + total + " skriv in ditt lösenord: ");
                        Console.WriteLine("För att avbryta tryck [Q]");
                        string pass = Console.ReadLine();
                        if (pass == Password)
                        {
                            Console.WriteLine("Grattis till ditt lån.");
                            LoanSum = LoanSum + total;
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
            Console.WriteLine("Alla föregående transaktioner, senaste till första:\n");
            foreach (string item in Log)
            {
                Console.WriteLine(item);
            }
            BankSystem.Return();
        }
    }
}
