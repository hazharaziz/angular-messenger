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
            Username = Users.Isaac.Username,
            Name = Users.Isaac.Name,
            IsPublic = 0
        };

        public static UserModel Oscar = new UserModel()
        {
            Id = 2,
            Username = Users.Oscar.Username,
            Name = Users.Oscar.Name,
            IsPublic = 1
        };

        public static UserModel Adam = new UserModel()
        {
            Id = 8,
            Username = Users.Adam.Username,
            Name = Users.Adam.Name,
            IsPublic = 0
        };

        public static UserModel Patrick = new UserModel()
        {
            Id = 10,
            Username = Users.Patrick.Username,
            Name = Users.Patrick.Name,
            IsPublic = 1
        };

        public static UserModel Nico = new UserModel()
        {
            Id = 10,
            Username = Users.Nico.Username,
            Name = Users.Nico.Name,
            IsPublic = 1
        };
    }
}
