using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantGuide.Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProps = "");

        Task<T> GetById(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task Delete(int id);
    }
}