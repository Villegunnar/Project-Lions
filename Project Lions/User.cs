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

        public User(string tempUsername, string tempPassword)
        {
            this.Username = tempUsername;
            this.Password = tempPassword;
        }
        public override string ToString()
        {
            return ("Username: " + Username + "Password: " + Password +"    inloggnings försök: " +   LoginCounter);
        }

        public void DisplayAccounts()
        { }

        public void TransferBalance()
        { }

        public void CreateSavingAcc()
        { }

        public void Loan()
        { }

        public void PreviousTransactions()
        { }
    }
}
