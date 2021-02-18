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
    public class DirectRepository : Repository<Direct>,  IDirectRepository
    {
        public DirectRepository(MessengerContext messengerContext) : base(messengerContext) { }

        public List<Direct> GetDirects(int userId)
            => Find(d => d.FirstUserId == userId || d.SecondUserId == userId);

        public Direct Get(int directId)
            => Find(d => d.DirectId == directId).FirstOrDefault();

        public bool HasDirect(int firstUserId, int secondUserId)
            => Find(d => (d.FirstUserId == firstUserId && d.SecondUserId == secondUserId) ||
                         (d.FirstUserId == secondUserId && d.SecondUserId == firstUserId))
                         .FirstOrDefault() != null;
    }
}
