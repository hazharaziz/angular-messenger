using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.DataContext;
using WebServer.Interfaces;
using WebServer.Models.DBModels;

namespace WebServer.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(MessengerContext messengerContext): base(messengerContext) { }

        public Group Get(int groupId)
            => Find(g => g.GroupId == groupId).FirstOrDefault();
    }
}
