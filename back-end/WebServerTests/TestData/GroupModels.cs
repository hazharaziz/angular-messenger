using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.ResponseModels;

namespace WebServerTests.TestData
{
    public static class GroupModels
    {
        public static GroupModel GroupModel1 = new GroupModel()
        {
            GroupId = 1,
            GroupName = Groups.Group1.GroupName
        };

        public static GroupModel GroupModel2 = new GroupModel()
        {
            GroupId = 2,
            GroupName = Groups.Group2.GroupName
        };

        public static GroupModel GroupModel3 = new GroupModel()
        {
            GroupId = 3,
            GroupName = Groups.Group3.GroupName
        };
    }
}
