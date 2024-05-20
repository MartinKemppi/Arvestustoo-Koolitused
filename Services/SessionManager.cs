using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koolitused.Services
{
    public static class SessionManager
    {
        public static string LoggedInUsername { get; private set; }
        public static bool IsAuthenticated { get; private set; }

        public static void SetSession(string username)
        {
            LoggedInUsername = username;
            IsAuthenticated = true;
        }

        public static void ClearSession()
        {
            LoggedInUsername = "not logged in";
            IsAuthenticated = false;
        }
    }
}
