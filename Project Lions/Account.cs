using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Lions
{
    public class Account
    {
        public decimal Balance { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal Interest { get; set; }
        public override string ToString()
        {
            return $"{Name}: {Balance} {Currency }";
        }
    }
}
