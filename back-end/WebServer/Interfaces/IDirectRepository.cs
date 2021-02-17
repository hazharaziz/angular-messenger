﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServer.Interfaces
{
    public interface IDirectRepository
    {
        IEnumerable<DirectMessage> GetAll();
        IEnumerable<DirectMessage> GetDirects(int userId);
        DirectMessage Get(int directId);
        IEnumerable<DirectMessage> Find(Expression<Func<DirectMessage, bool>> predicate);
        void Add(DirectMessage entity);
        void AddRange(IEnumerable<DirectMessage> entities);
        void Remove(DirectMessage entity);
        void RemoveRange(IEnumerable<DirectMessage> entities);
    }
}
