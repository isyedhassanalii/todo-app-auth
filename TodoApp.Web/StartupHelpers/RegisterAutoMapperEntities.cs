using AutoMapper;
using TodoApp.Core.DBEntities;
using TodoApp.Web.EnpointModel;
using TodoApp.Web.Model;

namespace TodoApp.Web
{
    public class RegisterAutoMapperEntities : Profile
    {
        public RegisterAutoMapperEntities()
        {
            CreateMap<User, UserModel>();
            CreateMap<Todo, TodoRequest>();



        }
    }

    
}
