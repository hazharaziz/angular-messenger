﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServer.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        User GetByUsername(string username);
        IEnumerable<User> Find(Expression<Func<User, bool>> predicate);
        void Add(User entity);
        void AddRange(IEnumerable<User> entities);
        void Remove(User entity);
        void RemoveRange(IEnumerable<User> entities);
    }
}
