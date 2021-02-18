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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MessengerContext messengerContext): base(messengerContext) { }

        public User Get(int id)
            => Find(user => user.Id == id).FirstOrDefault();

        public User GetByUsername(string username)
            => Find(u => u.Username == username).FirstOrDefault();
    }
}
