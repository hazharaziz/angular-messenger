using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServerTests.TestData
{
    public static class Directs
    {

        public static Direct Direct1 = new Direct()
        {
            DirectId = 1,
            FirstUserId = Users.Adam.Id,
            SecondUserId = 0
        };

        public static Direct Direct2 = new Direct()
        {
            DirectId = 2,
            FirstUserId = Users.Patrick.Id,
            SecondUserId = Users.Nico.Id
        };

        public static Direct Direct3 = new Direct()
        {
            DirectId = 3,
            FirstUserId = Users.Adam.Id,
            SecondUserId = Users.Nico.Id
        };

        public static Direct Direct4 = new Direct()
        {
            DirectId = 4,
            FirstUserId = Users.Adam.Id,
            SecondUserId = Users.Isaac.Id
        };

        public static Direct Direct5 = new Direct()
        {
            DirectId = 5,
            FirstUserId = Users.Isaac.Id,
            SecondUserId = Users.Oscar.Id
        };

        public static Direct Direct6 = new Direct()
        {
            DirectId = 6,
            FirstUserId = Users.Oscar.Id,
            SecondUserId = Users.Nico.Id
        };

        public static Direct Direct7 = new Direct()
        {
            DirectId = 7,
            FirstUserId = Users.Nico.Id,
            SecondUserId = Users.Isaac.Id
        };

        public static Direct Direct8 = new Direct()
        {
            DirectId = 8,
            FirstUserId = Users.Oscar.Id,
            SecondUserId = Users.Patrick.Id
        };

        public static Direct Direct9 = new Direct()
        {
            DirectId = 9,
            FirstUserId = Users.Isaac.Id,
            SecondUserId = Users.Patrick.Id
        };

        public static Direct Direct10 = new Direct()
        {
            DirectId = 10,
            FirstUserId = Users.Patrick.Id,
            SecondUserId = 0
        };

        public static Direct Null = null;
    }
}
