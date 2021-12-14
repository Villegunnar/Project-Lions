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
        public Admin(string admUsername, string admPassword, bool isAdmin)
        {
            this.Username = admUsername;
            this.Password = admPassword;
            this.IsAdmin = true;
        }
        public static void CurrencyRates()
        {
            Console.Clear();
            Console.WriteLine("[1] Ändra Dollarvärde");
            Console.WriteLine("[2] Ändra Eurovärde");
            Console.WriteLine("[3] Ändra valutakurs");
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
    }
}
