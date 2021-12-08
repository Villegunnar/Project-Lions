using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Lions
{
    public class Admin : User
    {
        public static decimal SekToUsd = 0.11m;
        public static decimal UsdToSek = 9.09m;
        public static decimal SekToEur = 0.097m;
        public static decimal EurToSek = 9.08m;
        public static decimal UsdToEur = 0.89m;
        public static decimal EurToUsd = 0.89m;
        public Admin() : this("", "", true) { }
        public Admin(string admUsername, string admPassword, bool isAdmin) : base(admUsername, admPassword)
        {
            this.IsAdmin = true;
        }
    }
}
