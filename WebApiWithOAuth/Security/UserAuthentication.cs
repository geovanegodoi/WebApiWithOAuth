using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiWithOAuth.Security
{
    public class UserAuthentication
    {
        private static readonly IEnumerable<UserCredentials> _credentials = new List<UserCredentials>
        {
            new UserCredentials { Login = "user001", Password="pwd001"},
            new UserCredentials { Login = "user002", Password="pwd002"},
            new UserCredentials { Login = "user003", Password="pwd003"}
        };

        public static bool Login(string login, string password)
        {
            return _credentials.Any(user => user.Login.Equals(login, StringComparison.OrdinalIgnoreCase) 
                                         && user.Password == password);
        }
    }

    public class UserCredentials
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
