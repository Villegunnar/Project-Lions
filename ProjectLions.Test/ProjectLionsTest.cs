using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Lions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectLions.Test
{
    [TestClass]
    public class ProjectLionsTest
    {
        [TestMethod]
        public void TakeLoan_WithPositivAmount_Return_SameAmount_AsNewLoan()
        {
            //Arrange ----------------------------------------------------------------------------------------------------------------
            Customer testingLoan = new Customer("testingLoan", "testingLoan123!", new Account() { Balance = 1000, Name = "test" });
            List<Account> Accounts = testingLoan.Accounts;
            string Password = testingLoan.Password;
            decimal LoanSum = 0;
            string testInput = "100";




            //Act ----------------------------------------------------------------------------------------------------------------
            decimal intresteamount = 0;
            decimal IntrestCal = 0;
            decimal total = 0;
            decimal accountSum = 0;
            decimal loanAmount;

            string loanInput = testInput;
            while (!decimal.TryParse(loanInput, out loanAmount) ^ loanInput == "ESC")
            {
            }
            if (loanInput != "ESC")
            {
                foreach (Account u in Accounts)
                {
                    accountSum = accountSum + u.Balance;
                }
                if (accountSum * 5 < loanAmount + LoanSum)
                {
                }
                else
                {
                    if (loanAmount < 100000)
                    {
                        IntrestCal = 1.05m;
                        intresteamount = 5m;
                        total = loanAmount * IntrestCal;
                    }
                    else if (loanAmount >= 100000)
                    {
                        IntrestCal = 1.02m;
                        intresteamount = 2m;
                        total = loanAmount * IntrestCal;
                    }
                    else if (loanAmount >= 500000)
                    {
                        IntrestCal = 1.015m;
                        intresteamount = 1.5m;
                        total = loanAmount * IntrestCal;
                    }

                    bool loanCon = true;
                    while (loanCon)
                    {
                        if (accountSum * 5 > loanAmount + LoanSum)
                        {
                            string pass = testingLoan.Password;
                            while (pass != Password && pass != "ESC")
                            {

                            }
                            if (pass == Password)
                            {
                                LoanSum = LoanSum + total;
                                Accounts[0].Balance += loanAmount;
                                loanCon = false;
                            }
                            else if (pass == "ESC")
                            {
                                loanCon = false;
                            }
                        }
                    }
                }
            }

            var actual = LoanSum;
            var expected = 105;


            //Assert ----------------------------------------------------------------------------------------------------------------
            Assert.AreEqual(actual, expected);

        }
        [TestMethod]
        public void TakeLoan_WithNegativAmount_Return_Error()
        {
            {
                //Arrange ----------------------------------------------------------------------------------------------------------------

                Customer testingLoan = new Customer("testingLoan", "testingLoan123!", new Account() { Balance = 1000, Name = "test" });
                List<Account> Accounts = testingLoan.Accounts;
                string Password = testingLoan.Password;
                decimal LoanSum = 0;
                string testInput = "-100";

                //Act ----------------------------------------------------------------------------------------------------------------
                decimal intresteamount = 0;
                decimal IntrestCal = 0;
                decimal total = 0;
                decimal accountSum = 0;
                decimal loanAmount;

                string loanInput = testInput;
                while (!decimal.TryParse(loanInput, out loanAmount) ^ loanInput == "ESC")
                {
                }
                if (loanInput != "ESC")
                {
                    foreach (Account u in Accounts)
                    {
                        accountSum = accountSum + u.Balance;
                    }
                    if (accountSum * 5 < loanAmount + LoanSum)
                    {
                    }
                    else
                    {
                        if (loanAmount < 100000)
                        {
                            IntrestCal = 1.05m;
                            intresteamount = 5m;
                            total = loanAmount * IntrestCal;
                        }
                        else if (loanAmount >= 100000)
                        {
                            IntrestCal = 1.02m;
                            intresteamount = 2m;
                            total = loanAmount * IntrestCal;
                        }
                        else if (loanAmount >= 500000)
                        {
                            IntrestCal = 1.015m;
                            intresteamount = 1.5m;
                            total = loanAmount * IntrestCal;
                        }

                        bool loanCon = true;
                        while (loanCon)
                        {
                            if (accountSum * 5 > loanAmount + LoanSum)
                            {
                                string pass = testingLoan.Password;
                                while (pass != Password && pass != "ESC")
                                {

                                }
                                if (pass == Password)
                                {
                                    LoanSum = LoanSum + total;
                                    Accounts[0].Balance += loanAmount;
                                    loanCon = false;
                                }
                                else if (pass == "ESC")
                                {
                                    loanCon = false;
                                }
                            }
                        }
                    }
                }


                var actual = LoanSum;

                bool expected = actual < 0;

                //Assert ----------------------------------------------------------------------------------------------------------------

                Assert.IsFalse(expected);

            }

        }
        [TestMethod]
        public void Admin_Set_Dollar_Rate_Positive_Return_Same_Rate()
        {

            //Arrange ----------------------------------------------------------------------------------------------------------------

            Admin testAdmin = new Admin("TestAdmin", "TestAdminPass");
            decimal DollarRate = Admin.DollarRate;
            decimal EurRate = Admin.EurRate;

            string testDollarAmount = "10";


            //Act ----------------------------------------------------------------------------------------------------------------
            decimal tempDollarRate = 0m;
            string dollarAmountIn = testDollarAmount;
            while ((!decimal.TryParse(dollarAmountIn, out tempDollarRate)) && dollarAmountIn != "ESC")
            {
            }
            if (dollarAmountIn != "ESC")
            {
                DollarRate = tempDollarRate;
            }



            var actual = DollarRate;

            var expected = 10;

            //Assert ----------------------------------------------------------------------------------------------------------------
            Assert.AreEqual(actual, expected);

        }
        [TestMethod]
        public void Admin_Set_Dollar_Rate_Negative_Return_Error()
        {

            //Arrange ----------------------------------------------------------------------------------------------------------------

            Admin testAdmin = new Admin("TestAdmin", "TestAdminPass");
            decimal DollarRate = Admin.DollarRate;
            decimal EurRate = Admin.EurRate;
            string testDollarAmount = "-10";


            //Act
            decimal tempDollarRate = 0m;
            string dollarAmountIn = testDollarAmount;
            while ((!decimal.TryParse(dollarAmountIn, out tempDollarRate)) && dollarAmountIn != "ESC")
            {
            }
            if (dollarAmountIn != "ESC")
            {
                DollarRate = tempDollarRate;
            }


            var actual = DollarRate;
            bool expected = actual < 0;

            //Assert ----------------------------------------------------------------------------------------------------------------
            Assert.IsFalse(expected);

        }
        [TestMethod]

        public void ExternalTransfer_Send_Money_To_Same_User_Return_Error()
        {
            // Arrange ----------------------------------------------------------------------------------------------------------------

            Customer externalTransferTest = new Customer("Testingtransfer", "Testingtransfer123!", new Account() { Balance = 1000, Name = "test" });

            List<Account> Accounts = externalTransferTest.Accounts;
            string moneyTransfer = "100";
            string usernameSearch = "Testingtransfer";
            BankSystem.AllCustomers.Add(externalTransferTest);


            // Act ----------------------------------------------------------------------------------------------------------------

            decimal amount = 0;
            bool userFound = false;
            int index = 0;
            for (int i = 0; i < BankSystem.AllCustomers.Count; i++)
            {
                if (BankSystem.AllCustomers[i].Username == usernameSearch)
                {
                    userFound = true;
                    index = i;
                    break;
                }
            }
            if (userFound)
            {

                int moveFrom = 1;

                string amountIn = moneyTransfer;
                while (((!decimal.TryParse(amountIn, out amount)) || amount < 0 || amount > Accounts[moveFrom - 1].Balance) && amountIn != "ESC")
                {

                }

                if (BankSystem.AllCustomers[index].Accounts[0].Currency == Accounts[moveFrom - 1].Currency)
                {
                    Accounts[moveFrom - 1].Balance -= amount;
                    BankSystem.AllCustomers[index].Accounts[0].Balance += amount;
                }
            }
            else
            {
                BankSystem.CenterColor("Användarnamn kunde ej hittas", false, "Red");
            }



            var actual = amount;
            var expected = 100;


            // Assert ----------------------------------------------------------------------------------------------------------------
            Assert.AreNotEqual(expected, actual); 
        }
    }
}
