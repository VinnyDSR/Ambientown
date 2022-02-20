using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AmbienTown.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        int Create(T model);
        void Update(T model);
        void PartialUpdate(T model, params string[] properties);
        void Remove(T model);
        T GetById(int id);
        bool Exists(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
    }
}