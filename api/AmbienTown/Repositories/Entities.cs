using AmbienTown.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AmbienTown.Repositories
{
    public class Entities
    {
        protected readonly AmbienTownContext context;

        public Entities(AmbienTownContext context)
        {
            this.context = context;

            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add(object model)
        {
            this.context.Add(model);

            this.context.SaveChanges();

            this.context.Entry(model).State = EntityState.Detached;
        }

        public void AddRange(IEnumerable<object> models)
        {
            this.context.AddRange(models);

            this.context.SaveChanges();

            models.Select(model => this.context.Entry(model).State = EntityState.Detached); //-V3010
        }

        public bool Exists<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return this.Query<T>().Any(expression);
        }

        public IEnumerable<T> ListAll<T>() where T : class => this.Query<T>().ToList();

        public IQueryable<T> Query<T>() where T : class => this.context.Set<T>().AsQueryable();

        public void Update(object model)
        {
            this.context.Entry(model).State = EntityState.Modified;

            this.context.SaveChanges();

            this.context.Entry(model).State = EntityState.Detached;
        }

        public void PartialUpdate(object model, params string[] properties)
        {
            this.context.Entry(model).State = EntityState.Modified;

            var navigationProperties = this.context.Entry(model).Metadata.GetNavigations().Select(x => x.PropertyInfo);

            var propertiesNames = this.context.Entry(model).CurrentValues.Properties.Select(x => x.PropertyInfo).Except(navigationProperties).Select(x => x.Name);

            foreach (var property in propertiesNames)
            {
                if (properties.Contains(property))
                {
                    this.context.Entry(model).Property(property).IsModified = true;
                    continue;
                }
                this.context.Entry(model).Property(property).IsModified = false;
            }

            this.context.SaveChanges();

            this.context.Entry(model).State = EntityState.Detached;
        }

        public virtual void Remove(object model)
        {
            this.context.Remove(model);

            this.context.SaveChanges();
        }

        public virtual void RemoveRange(IEnumerable<object> models)
        {
            this.context.RemoveRange(models);

            this.context.SaveChanges();
        }
    }
}