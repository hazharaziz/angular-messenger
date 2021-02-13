﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.DBModels.Models;

namespace WebServer.Interfaces
{
    public interface IUserRepository
    {
        User Get(int id);
        IEnumerable<User> GetAll();
        IEnumerable<User> Find(Expression<Func<User, bool>> predicate);
        void Add(User entity);
        void AddRange(IEnumerable<User> entities);
        void Remove(User entity);
        void RemoveRange(IEnumerable<User> entities);
        User GetByUsername(string username);
    }
}
