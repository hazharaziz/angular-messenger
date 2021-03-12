using Microsoft.EntityFrameworkCore;
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
    public class GroupMemberRepository : Repository<GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRepository(MessengerContext context) : base(context) { }

        public List<int> GetUserGroups(int userId)
            => Find(g => g.UserId == userId).Select(g => g.GroupId).ToList();

        public List<int> GetGroupMembers(int groupId)
            => Find(g => g.GroupId == groupId).Select(g => g.UserId).ToList();

        public GroupMember GetGroupMember(int groupId, int memberId)
            => Find(g => g.GroupId == groupId && g.UserId == memberId).FirstOrDefault();

        public bool IsMemberOfGroup(int userId, int groupId)
            => Find(g => g.UserId == userId && g.GroupId == groupId).FirstOrDefault() != null;
    }
}
