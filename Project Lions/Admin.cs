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
        public Admin(string admUsername, string admPassword)
        {
            this.Username = admUsername;
            this.Password = admPassword;
        }
        public static void CurrencyRates()
        {
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                BankSystem.CenterColor("ÄNDRA VALUTAKURS\n", true, "Yellow");
                Console.WriteLine("[1] Ändra Dollarvärde\n[2] Ändra Eurovärde\n");
                ConsoleKey menuChoice = Console.ReadKey(intercept: true).Key;
                switch (menuChoice)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        Console.Clear();
                        BankSystem.CenterColor("ÄNDRA VALUTAKURS\n", true, "Yellow");
                        Console.Write($"Aktuellt Dollarvärde: {DollarRate}\nÄndra Dollarvärde: ");
                        decimal tempDollarRate = 0m;
                        string dollarAmountIn = BankSystem.ShowInput();
                        while ((!decimal.TryParse(dollarAmountIn, out tempDollarRate)) && dollarAmountIn != "ESC")
                        {
                            Console.Clear();
                            BankSystem.CenterColor("ÄNDRA VALUTAKURS\n", true, "Yellow");
                            Console.Write($"Aktuellt Dollarvärde: {DollarRate}\nÄndra Dollarvärde: ");
                            dollarAmountIn = BankSystem.ShowInput();
                        }
                        if (dollarAmountIn != "ESC")
                        {
                            Console.Clear();
                            DollarRate = tempDollarRate;
                            BankSystem.CenterColor("ÄNDRA VALUTAKURS\n", true, "Yellow");
                            Console.WriteLine("Nytt Dollarvärde: " + DollarRate);
                            loop = false;
                            BankSystem.Return();
                        }
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        Console.Clear();
                        BankSystem.CenterColor("ÄNDRA VALUTAKURS\n", true, "Yellow");
                        Console.Write($"Aktuellt Eurovärde: {EurRate}\nÄndra Eurovärde: ");
                        decimal tempEuroRate = 0m;
                        string euroAmountIn = BankSystem.ShowInput();
                        while ((!decimal.TryParse(euroAmountIn, out tempEuroRate)) && euroAmountIn != "ESC")
                        {
                            Console.Clear();
                            BankSystem.CenterColor("ÄNDRA VALUTAKURS\n", true, "Yellow");
                            Console.Write($"Aktuellt Eurovärde: {EurRate}\nÄndra Eurovärde: ");
                            euroAmountIn = BankSystem.ShowInput();
                        }
                        if (euroAmountIn != "ESC")
                        {
                            Console.Clear();
                            EurRate = tempEuroRate;
                            BankSystem.CenterColor("ÄNDRA VALUTAKURS\n", true, "Yellow");
                            Console.WriteLine("Nytt Eurovärde: " + EurRate);
                            loop = false;
                            BankSystem.Return();
                        }
                        break;
                    case ConsoleKey.Escape:
                        loop = false;
                        break;
                    default:
                        break;
                }
            }
        }
        public static void ChangeInterestRate()
        {
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                BankSystem.CenterColor("ÄNDRA BANKENS RÖRLIGA RÄNTA\n", true, "Yellow");
                Console.WriteLine($"Aktuell ränta: {decimal.Round((InterestRate * 100), 2) } %");
                Console.Write("Ange ny ränta: ");
                string intRateIn = BankSystem.ShowInput();
                decimal tempIntRate = 0m;
                while (!(decimal.TryParse(intRateIn, out tempIntRate)) && intRateIn != "ESC")
                {
                    Console.Clear();
                    BankSystem.CenterColor("ÄNDRA BANKENS RÖRLIGA RÄNTA\n", true, "Yellow");
                    Console.WriteLine($"Aktuell ränta: {decimal.Round((InterestRate * 100), 2) } %");
                    Console.Write("Ange ny ränta: ");
                    intRateIn = BankSystem.ShowInput();
                }
                if (intRateIn != "ESC")
                {
                    InterestRate = (tempIntRate / 100);
                    Console.Clear();
                    BankSystem.CenterColor("ÄNDRA BANKENS RÖRLIGA RÄNTA\n", true, "Yellow");
                    Console.WriteLine($"Bankens nya ränta är nu: {decimal.Round((InterestRate * 100), 2)} %");
                    loop = false;
                    BankSystem.Return();
                }
                loop = false;
            }
        }

    }
}
