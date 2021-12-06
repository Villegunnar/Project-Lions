using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Lions
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Account> Accounts { get; set; }
        public int LoginCounter { get; set; }
        public bool LockedOut { get; set; }
        public decimal LoanSum { get; set; }
        public List<string> Log { get; set; }

        public User(string tempUsername, string tempPassword)
        {
            this.Username = tempUsername;
            this.Password = tempPassword;
            Accounts = new List<Account>();
            Log = new List<string>();
        }
        public override string ToString()
        {
            return ("Username: " + Username + "Password: " + Password +"    inloggnings försök: " +   LoginCounter);
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
                System.ClearLine();
                Console.Write("Ogiltig input. Ange vilket konto du vill föra över FRÅN: ");
            }
            Console.Write("Ange vilket konto du vill föra över TILL: ");
            while (!int.TryParse(Console.ReadLine(), out moveTo) || moveTo < 1 || moveTo > Accounts.Count || moveTo == moveFrom)
            {
                System.ClearLine();
                Console.Write("Ogiltig input. Ange med vilket konto du vill föra över TILL: ");
            }
            Console.Write("Skriv in hur mycket du vill föra över: ");
            while ((!decimal.TryParse(Console.ReadLine(), out amount)) || amount < 0 || amount > Accounts[moveFrom - 1].Balance)
            {
                System.ClearLine();
                Console.Write("Ogiltig Input. Skriv in hur mycket du vill föra över: ");
            }
            Accounts[moveFrom - 1].Balance = Accounts[moveFrom - 1].Balance - amount;
            Accounts[moveTo - 1].Balance = Accounts[moveTo - 1].Balance + amount;
            Console.Clear();
            System.PrintGreen($"Transaktion lyckad!\n\n{amount} {Accounts[moveFrom - 1].Currency} fördes över från {Accounts[moveFrom - 1].Name} till {Accounts[moveTo - 1].Name}\n");
            Log.Add($"{DateTime.Now} {amount} {Accounts[moveFrom - 1].Currency} fördes över från {Accounts[moveFrom - 1].Name} till {Accounts[moveTo - 1].Name}");
            Console.WriteLine("Tryck Enter för att återgå till huvudmenyn.");
            Console.ReadLine();
        }
        public void OpenNewBankAccount()
        {
            Console.Clear();

            Console.WriteLine("[1] Öppna nytt konto");
            Console.WriteLine("[2] Öppna nytt sparkonto");
            int createAccChoise;
            int.TryParse(Console.ReadLine(), out createAccChoise);
            switch (createAccChoise)
            {
                case 1:
                    Console.WriteLine("Namn på ditt nya konto: ");
                    string regAccountName = Console.ReadLine();
                    Console.Clear();
                    Accounts.Add(new Account { Balance = 0, Name = regAccountName, Currency = "SEK" });
                    Console.WriteLine("Nytt konto " + regAccountName + " skapat!");
                    break;
                case 2:


                    Console.WriteLine("Namn på ditt nya sparkonto: ");
                    string saveAccountName = Console.ReadLine();

                    decimal tempInterest = 1.06m;
                    Console.Clear();
                    Console.WriteLine("Beräkna räntan");
                    Console.WriteLine();
                    Console.WriteLine("Här kan du se hur mycket ränta du kommer\nfå på ditt sparkonto, välj ett av alternativen nedan: ");
                    Console.WriteLine();
                    Console.WriteLine("[1] Fasträntekonto, 1,10 % ränta årsbasis, bindningstid 1år . ");
                    Console.WriteLine("[2] Fasträntekonto, 1,40 % ränta årsbasis, bindningstid 2år. ");
                    Console.WriteLine("[3] Rörligträntekonto, aktuellränta: 0,60 % årsbasis, ingen bindningstid");


                    int createSaveAccChoise;
                    int.TryParse(Console.ReadLine(), out createSaveAccChoise);
                    switch (createSaveAccChoise)
                    {
                        case 1:


                            Console.WriteLine("Exempel på hur mycket pengar du vill sätta in: ");
                            decimal saveAccAmount;
                            decimal.TryParse(Console.ReadLine(), out saveAccAmount);
                            

                            Accounts.Add(new Account { Balance = 0, Name = saveAccountName, Currency = "SEK", Interest = 0 });

                            Console.WriteLine($"På 1 år kommer dina: {saveAccAmount} att bli {saveAccAmount * 1.01m} kr");


                            break;
                        case 2:


                            Console.WriteLine("Exempel på hur mycket pengar du vill sätta in: ");
                            decimal.TryParse(Console.ReadLine(), out saveAccAmount);

                            Accounts.Add(new Account { Balance = 0, Name = saveAccountName, Currency = "SEK", Interest = 0 });


                            Console.WriteLine($"På 2 år kommer dina {saveAccAmount} att bli {(saveAccAmount * 1.04m) * 1.04m} kr");

                            break;
                        case 3:
                            Console.WriteLine("Exempel på hur mycket pengar du vill sätta in: ");
                            decimal.TryParse(Console.ReadLine(), out saveAccAmount);

                            Accounts.Add(new Account { Balance = 0, Name = saveAccountName, Currency = "SEK", Interest = 0 });

                            Console.WriteLine($"På 1 år kommer dina {saveAccAmount} att bli {(saveAccAmount * tempInterest) * tempInterest} kr");

                            break;
                        default:
                            break;
                    }





                    break;
                default:
                    Console.WriteLine("Ogiltigt alternativ");
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
                System.Return();
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
                            System.Return();
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
            System.Return();
        }


    }
}
