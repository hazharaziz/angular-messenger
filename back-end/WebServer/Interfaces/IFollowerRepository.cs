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
        IEnumerable<DirectMessage> GetFollowers(int userId);
        IEnumerable<DirectMessage> GetAll();
        IEnumerable<DirectMessage> Find(Expression<Func<DirectMessage, bool>> predicate);
        void Add(DirectMessage entity);
        void AddRange(IEnumerable<DirectMessage> entities);
        void Remove(DirectMessage entity);
        void RemoveRange(IEnumerable<DirectMessage> entities);
        bool HasFollower(int userId, int followerId);
        bool HasRequestFrom(int userId, int followerId);
    }
}
