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
    public class DirectRepository : IDirectRepository
    {
        public MessengerContext Context;
        public DirectRepository(MessengerContext messengerContext)
        {
            Context = messengerContext;
        }

        public void Add(Direct direct)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<Direct> directs)
        {
            throw new NotImplementedException();
        }

        public List<Direct> Find(Expression<Func<Direct, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Direct Get(int directId)
        {
            throw new NotImplementedException();
        }

        public List<Direct> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Direct> GetDirects(int userId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Direct direct)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(List<Direct> directs)
        {
            throw new NotImplementedException();
        }
    }
}
