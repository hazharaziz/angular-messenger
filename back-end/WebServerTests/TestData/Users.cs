using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Models.RequestModels;

namespace WebServerTests.TestData
{
    public static class Users
    {
        public static User Isaac = new User()
        {
            Id = 1,
            Username = "isaac",
            Password = "12345",
            Name = "Isaac Green",
            IsPublic = 0
        };

        public static User Oscar = new User()
        {
            Id = 2,
            Username = "oscar",
            Password = "1235",
            Name = "Oscar Barker",
            IsPublic = 1
        };

        public static User Adam = new User()
        {
            Id = 8,
            Username = "adam",
            Password = "12346",
            Name = "Adam Bradley",
            IsPublic = 0
        };

        public static User Patrick = new User()
        {
            Id = 10,
            Username = "patrick",
            Password = "12342",
            Name = "Patrick Holmes",
            IsPublic = 1
        };

        public static User Nico = new User()
        {
            Id = 13,
            Username = "nico",
            Password = "1234",
            Name = "Nico Lara",
            IsPublic = 0
        };

        public static User Null = null;
    }
}
