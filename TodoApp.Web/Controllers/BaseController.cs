using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.DBEntities.Authentication;
using TodoApp.Core.Interfaces;
using System.Security.Claims;

namespace TodoApp.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IUserService _customerService;
        public BaseController(IUserService customerService)
        {
            _customerService = customerService;
        }
        protected ApplicationUser GetCurrentUser()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            return _customerService.FindByEmailAsync(email).Result;

        }
    }
}
