using Microsoft.AspNetCore.Identity;
using TodoApp.Core.DBEntities;
using TodoApp.Core.DBEntities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Core.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> FindByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<ApplicationUser> FindByIdAsync(string id, CancellationToken cancellationToken = default);

        Task<IList<string>> GetUserRoles(ApplicationUser user, CancellationToken cancellationToken = default);

        Task<ApplicationUser> FindByNameAsync(string userName);

        Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password);

        Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, string password);

        Task<IdentityResult> AddRolesAsync(ApplicationUser applicationUser, string roleName);

        Task<User> AddAsync(User customer, CancellationToken cancellationToken = default);

        Task<List<User>> ListOfUsersAsync(CancellationToken cancellationToken = default);
    }
}
