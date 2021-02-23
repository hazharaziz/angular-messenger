using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.RequestModels;

namespace WebServerTests.TestData
{
    public static class LoginRequests
    {
        public static LoginRequest Isaac = new LoginRequest()
        {
            Username = "isaac",
            Password = "12345"
        };

        public static LoginRequest Oscar = new LoginRequest()
        {
            Username = "oscar",
            Password = "1235"
        };

        public static LoginRequest Adam = new LoginRequest()
        {
            Username = "adam",
            Password = "12346"
        };

        public static LoginRequest Patrick = new LoginRequest()
        {
            Username = "patrick",
            Password = "12342"
        };

        public static LoginRequest Edward = new LoginRequest()
        {
            Username = "edward",
            Password = "1234"
        };
    }
}
