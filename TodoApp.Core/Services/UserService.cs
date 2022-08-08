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
    public class UserService : IUserService
    {
        private IRepository<User> _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IRepository<User> repository,
            UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password)
        {
            return await _userManager.CheckPasswordAsync(applicationUser, password);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, string password)
        {
            return await _userManager.CreateAsync(applicationUser, password);
        }
        public async Task<IdentityResult> AddRolesAsync(ApplicationUser applicationUser, string roleName)
        {
            return await _userManager.AddToRoleAsync(applicationUser, roleName);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _userManager.Users.Where(m => m.Email == email).Include(m => m.User).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IList<string>> GetUserRoles(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<ApplicationUser> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _userManager.Users.Where(m => m.Id == id)
                .Include(m => m.User)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> AddAsync(User customer, CancellationToken cancellationToken = default)
        {
            return await _repository.AddAsync(customer, cancellationToken);
        }

        public async Task<List<User>> ListOfUsersAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userManager.GetUsersInRoleAsync("User");
            return _repository.Table
                .Where(m => users
                    .Select(x => x.Id)
                    .Contains(m.ApplicationUserId)).ToList();
        }

       
    }
}
