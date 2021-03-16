using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commerce.Models
{
    public class Account
    {
        private string userName;
        private string userPass;

        public string GetUserName() { return userName; }
        public void SetUserName(string userName) { this.userName = userName; }
        public string GetUserPass() { return userPass; }
        public void SetUserPass(string userPass) { this.userPass = userPass; }
    }
}
