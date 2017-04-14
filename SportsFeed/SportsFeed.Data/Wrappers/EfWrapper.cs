using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;

using SportsFeed.Data.Contracts;
using SportsFeed.Data.Results;

namespace SportsFeed.Data.Wrappers
{
    public class EfWrapper<T> : IEfWrapper<T> where T : class
    {
        private readonly IDataModifiedResultFactory resultFactory;

        public EfWrapper(ISportsFeedDbContext context, IDataModifiedResultFactory resultFactory)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (resultFactory == null)
            {
                throw new ArgumentNullException(nameof(resultFactory));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
            this.resultFactory = resultFactory;
        }

        public IQueryable<T> All => this.DbSet;

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public ISportsFeedDbContext Context { get; set; }

        protected IDbSet<T> DbSet { get; set; }

        public void Add(T entity)
        {
            var dbEntity = this.CopyStateFrom(entity);
            var entry = this.AttachIfDetached(dbEntity);
            entry.State = EntityState.Added;
        }

        public void Update(T entity)
        {
            this.DbSet.AddOrUpdate(entity);
            //var entry = this.AttachIfDetached(entity);
            //entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            var entry = this.AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        public IDataModifiedResult SaveChanges()
        {
            try
            {
                this.Context.SaveChanges();

                return this.resultFactory.CreateDatabaseUpdateResult(isSuccessfull: true);
            }
            catch (DbEntityValidationException e)
            {
                var errors = from eve in e.EntityValidationErrors
                             from ve in eve.ValidationErrors
                             select $"Error: \"{ve.ErrorMessage}\"";

                return this.resultFactory.CreateDatabaseUpdateResult(false, errors);
            }
            catch (DbUpdateException e)
            {
                return this.resultFactory.CreateDatabaseUpdateResult(false, new[] { e.Message });
            }
        }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            return entry;
        }

        private T CopyStateFrom(T entity)
        {
            var dbEntity = this.DbSet.Create();
            var type = typeof(T);
            var properties = type.GetProperties();

            foreach (var srcProp in properties)
            {
                if (!srcProp.CanRead)
                {
                    continue;
                }

                var targetProperty = type.GetProperty(srcProp.Name);
                if (targetProperty == null)
                {
                    continue;
                }

                if (!targetProperty.CanWrite)
                {
                    continue;
                }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }

                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }

                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }

                targetProperty.SetValue(dbEntity, srcProp.GetValue(entity, null), null);
            }

            return dbEntity;
        }
    }
}
