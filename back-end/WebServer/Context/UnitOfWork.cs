using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.DataContext;
using WebServer.Interfaces;
using WebServer.Repositories;

namespace WebServer.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        public MessengerContext _context;
        public IUserRepository Users { get; private set; }
        public IMessageRepository Messages { get; private set; }
        public IFollowerRepository Followers { get; private set; }

        public UnitOfWork(MessengerContext messengerContext)
        {
            _context = messengerContext;
            Users = new UserRepository(_context);
            Messages = new MessageRepository(_context);
            Followers = new FollowerRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
