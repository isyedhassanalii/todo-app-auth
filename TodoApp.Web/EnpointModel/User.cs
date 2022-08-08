using System.ComponentModel.DataAnnotations;

namespace TodoApp.Web.EnpointModel
{
    public class UserRegisterRequest
    {
        [MaxLength(200)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
    }

    public class UserLoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
    }
}
