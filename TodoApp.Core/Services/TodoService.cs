using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Data;
using TodoApp.Core.DBEntities;
using TodoApp.Core.DBEntities.Authentication;
using TodoApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Core.Services
{
    public class TodoService : ITodoService
    {
        private IRepository<Todo> _repositoryTodo;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoService(IRepository<Todo> repositoryTodo,
            UserManager<ApplicationUser> userManager)
        {
            _repositoryTodo = repositoryTodo;
            _userManager = userManager;
        }

        public Task<Todo> AddAsync(Todo product, CancellationToken cancellationToken = default)
        {
            return _repositoryTodo.AddAsync(product, cancellationToken);
        }

        public Task UpdateAsync(Todo product, CancellationToken cancellationToken = default)
        {
            return _repositoryTodo.UpdateAsync(product, cancellationToken);
        }

        public Task DeleteAsync(Todo product, CancellationToken cancellationToken = default)
        {
            return _repositoryTodo.DeleteAsync(product, cancellationToken);
        }

        public Task<Todo> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _repositoryTodo.GetByIdAsync(id, cancellationToken);
        }

        public Task<List<Todo>> GetByIdAsync(int[] id, CancellationToken cancellationToken = default)
        {
            return _repositoryTodo.Table.Where(m => id.Contains(m.Id)).ToListAsync();
        }

        public Task<List<Todo>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _repositoryTodo.Table
                .Include(m=> m.Customer)
                .ThenInclude(m=> m.ApplicationUser)
                .ToListAsync();
        }

        public Task<List<Todo>> GetAllByUserIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return _repositoryTodo.Table
                .Where(m=> m.CustomerId == customerId)
                .Include(m => m.Customer)
                .ThenInclude(m => m.ApplicationUser)
                .ToListAsync();
        }
    }
}
