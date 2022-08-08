using TodoApp.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Core.Interfaces
{
    public interface ITodoService
    {
        Task<Todo> AddAsync(Todo todo, CancellationToken cancellationToken = default);
        Task UpdateAsync(Todo todo, CancellationToken cancellationToken = default);
        Task DeleteAsync(Todo todo, CancellationToken cancellationToken = default);
        Task<Todo> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Todo>> GetByIdAsync(int[] id, CancellationToken cancellationToken = default);
        Task<List<Todo>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<List<Todo>> GetAllByUserIdAsync(int customerId, CancellationToken cancellationToken = default);
    }
}
