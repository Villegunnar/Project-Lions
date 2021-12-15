using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Lions
{
    public class Admin : User
    {
        public static decimal SekRate = 1m;
        public static decimal DollarRate = 9.1m;
        public static decimal EurRate = 10.25m;

        public static decimal InterestRate = 0.006m;
        public Admin(string admUsername, string admPassword, bool isAdmin)
        {
            this.Username = admUsername;
            this.Password = admPassword;
            this.IsAdmin = true;
        }
        public static void CurrencyRates()
        {
            Console.Clear();
            BankSystem.CenterColor("ÄNDRA VALUTAKURS\n", true, "Yellow");
            Console.WriteLine("[1] Ändra Dollarvärde\n[2] Ändra Eurovärde");
            int currencyMenu;
            int.TryParse(Console.ReadLine(), out currencyMenu);
            Console.WriteLine();
            switch (currencyMenu)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Aktuellt dollarvärde: " + DollarRate);
                    Console.Write("Ändra Dollarvärde: ");
                    DollarRate = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Nytt dollarvärde: " + DollarRate);
                    BankSystem.Return();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Aktuellt Euroarvärde: " + EurRate);
                    Console.Write("Ändra Eurovärde: ");
                    EurRate = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Nytt Eurovärde: " + EurRate);
                    BankSystem.Return();
                    break;
                default:
                    Console.WriteLine("Ogiltigt alternativ...");
                    break;
            }
        }
        public static void ChangeInterestRate()
        {
            Console.Clear();
            BankSystem.CenterColor("ÄNDRA BANKENS RÖRLIGA RÄNTA\n", true, "Yellow");
            Console.WriteLine($"Aktuell ränta:  {decimal.Round((InterestRate*100),2) } %");
            Console.Write("Ange ny ränta: ");
            InterestRate = (Convert.ToDecimal(Console.ReadLine())/100);
            Console.WriteLine($"Bankens nya ränta är nu:   {decimal.Round((InterestRate * 100), 2)} %");
            BankSystem.Return();
        }

    }
}
