using AmbienTown.Models.Interfaces;
using AmbienTown.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AmbienTown.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly Entities entities;

        protected IQueryable<T> DefaultQuery { get; set; }

        public Repository(Entities entities)
        {
            this.DefaultQuery = entities.Query<T>();
            this.entities = entities;
        }

        public virtual int Create(T model)
        {
            entities.Add(model);

            return model.Id;
        }

        public virtual bool Exists(Expression<Func<T, bool>> expression)
        {
            return this.entities.Exists(expression);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return entities.ListAll<T>();
        }

        public virtual T GetById(int id)
        {
            return entities.Query<T>().FirstOrDefault(x => x.Id == id);
        }

        public virtual void Remove(T model)
        {
            entities.Remove(model);
        }

        public virtual void Update(T model)
        {
            entities.Update(model);
        }

        public virtual void PartialUpdate(T model, params string[] properties)
        {
            entities.PartialUpdate(model, properties);
        }
    }
}