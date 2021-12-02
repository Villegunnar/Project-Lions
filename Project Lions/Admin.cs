using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Lions
{
    public class Admin : User
    {
        public bool _isAdmin;

        public Admin() : this("", "", true) { }

        public Admin(string admUsername, string admPassword, bool isAdmin) : base(admUsername, admPassword)
        {
            this._isAdmin = isAdmin;
        }
    }
}
