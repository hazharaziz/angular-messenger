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
        List<Direct> GetAll();
        List<Direct> Find(Expression<Func<Direct, bool>> predicate);
        void Add(Direct direct);
        void AddRange(IEnumerable<Direct> directs);
        void Remove(Direct direct);
        void RemoveRange(IEnumerable<Direct> directs);
        List<Direct> GetDirects(int userId);
        Direct Get(int directId);
    }
}
