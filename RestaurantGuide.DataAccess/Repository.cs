using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantGuide.Core.Interfaces;

namespace RestaurantGuide.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> entities;

        public Repository(DbContext context)
        {
            this.entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProps = "")
        {
            IQueryable<T> query = this.entities;
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProps.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, prop) => current.Include(prop));

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public Task<T> GetById(int id)
        {
            return this.entities.FindAsync(id);
        }

        public void Create(T entity)
        {
            this.entities.Add(entity);
        }

        public void Update(T entity)
        {
            this.entities.Update(entity);
        }

        public void Delete(T entity)
        {
            this.entities.Remove(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await this.GetById(id);
            this.Delete(entity);
        }
    }
}