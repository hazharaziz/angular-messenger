using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServer.Interfaces
{
    public interface IDirectRepository
    {
        IEnumerable<Direct> GetAll();
        IEnumerable<Direct> GetDirects(int userId);
        Direct Get(int directId);
        IEnumerable<Direct> Find(Expression<Func<Direct, bool>> predicate);
        void Add(Direct entity);
        void AddRange(IEnumerable<Direct> entities);
        void Remove(Direct entity);
        void RemoveRange(IEnumerable<Direct> entities);
    }
}
