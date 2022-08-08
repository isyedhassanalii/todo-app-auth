using Microsoft.Extensions.DependencyInjection;
using TodoApp.Core.Data;
using TodoApp.Core.DBEntities;
using TodoApp.Core.Interfaces;
using TodoApp.Core.Services;
using TodoApp.Infrastructure.Data;
using TodoApp.Web.EnpointModel;
using TodoApp.Web.Helpers;
using System;

namespace TodoApp.Web
{
    internal class DependencyRegistrar
    {
        private IServiceCollection _services;

        public DependencyRegistrar(IServiceCollection services)
        {
            this._services = services;
        }

        internal void ConfigureDependencies()
        {
            _services.AddScoped<IRepository<User>, EfRepository<User>>();
       
            _services.AddScoped<IRepository<Log>, EfRepository<Log>>();

            _services.AddScoped<IRepository<Todo>, EfRepository<Todo>>();

            _services.AddScoped<IUserService, UserService>();
         
            _services.AddScoped<ILogService, LogService>();

            _services.AddScoped<ITodoService, TodoService>();

            _services.AddScoped<IResponseGeneric, ResponseGeneric>();
        }
    }
}