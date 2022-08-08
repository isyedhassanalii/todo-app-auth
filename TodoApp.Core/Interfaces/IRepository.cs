using Microsoft.EntityFrameworkCore.Storage;
using TodoApp.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Core.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Table { get; }

        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default(CancellationToken)) where TId : notnull;

        Task<List<T>> ListAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        IDbContextTransaction BeginTransaction();

    }
}
