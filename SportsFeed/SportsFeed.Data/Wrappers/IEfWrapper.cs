using System.Linq;

using SportsFeed.Data.Results;

namespace SportsFeed.Data.Wrappers
{
    public interface IEfWrapper<T> where T : class
    {
        T GetById(object id);

        IQueryable<T> All { get; }

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        IDataModifiedResult SaveChanges();
    }
}