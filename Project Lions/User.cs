using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Lions
{
    public class User
    {
        private string username;
        private string password;
        private int loginCounter;
        private bool lockedOut;
        private bool banned;
        private List<Account> accounts;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        

        public bool Banned { get => banned; set => banned = value; }
        public List<Account> Accounts { get => accounts; set => accounts = value; }
        public int LoginCounter { get => loginCounter; set => loginCounter = value; }
        public bool LockedOut { get => lockedOut; set => lockedOut = value; }

        public User(string tempUsername, string tempPassword)
        {
            this.Username = tempUsername;
            this.Password = tempPassword;
        }
        public override string ToString()
        {
            return ("Username: " + Username + "Password: " + Password +"    inloggnings försök: " +   LoginCounter);
        }
    }
}
