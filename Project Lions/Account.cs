using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Lions
{
    public class Account
    {
        private decimal balance;
        private string accountName;
        protected decimal Balance { get => balance; set => balance = value; }
        protected string Name { get => accountName; set => accountName = value; }
        public override string ToString()
        {
            return $"{accountName}: {balance}";
        }
    }
}
