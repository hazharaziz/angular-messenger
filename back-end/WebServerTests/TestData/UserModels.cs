using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.ResponseModels;

namespace WebServerTests.TestData
{
    public static class UserModels
    {
        public static UserModel Isaac = new UserModel()
        {
            Id = 1,
            Username = "isaac",
            Name = "Isaac Green",
            IsPublic = 0
        };

        public static UserModel Oscar = new UserModel()
        {
            Id = 2,
            Username = "oscar",
            Name = "Oscar Barker",
            IsPublic = 1
        };

        public static UserModel Adam = new UserModel()
        {
            Id = 8,
            Username = "adam",
            Name = "Adam Bradley",
            IsPublic = 1
        };

        public static UserModel Patrick = new UserModel()
        {
            Id = 10,
            Username = "patrick",
            Name = "Patrick Holmes",
            IsPublic = 1
        };
    }
}
