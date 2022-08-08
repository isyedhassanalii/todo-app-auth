using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TodoApp.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Infrastructure.Data
{
    public abstract class RepositoryBase<T> where T : BaseEntity
    {
        private readonly DbContext dbContext;
        private DbSet<T> _entities;

        public RepositoryBase(DbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public virtual IQueryable<T> Table => Entities;
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = dbContext.Set<T>();

                return _entities;
            }
        }
        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Set<T>().Add(entity);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }
        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Entry(entity).State = EntityState.Modified;

            await SaveChangesAsync(cancellationToken);
        }
        public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Set<T>().Remove(entity);

            await SaveChangesAsync(cancellationToken);
        }
        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            dbContext.Set<T>().RemoveRange(entities);
            await SaveChangesAsync(cancellationToken);
        }
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }
        public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return dbContext.Database.BeginTransaction();
        }
    }
}
