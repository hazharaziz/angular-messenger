﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServer.Interfaces
{
    public interface IFollowerRepository
    {
        IEnumerable<Direct> GetFollowers(int userId);
        IEnumerable<Direct> GetAll();
        IEnumerable<Direct> Find(Expression<Func<Direct, bool>> predicate);
        void Add(Direct entity);
        void AddRange(IEnumerable<Direct> entities);
        void Remove(Direct entity);
        void RemoveRange(IEnumerable<Direct> entities);
        bool HasFollower(int userId, int followerId);
        bool HasRequestFrom(int userId, int followerId);
    }
}
