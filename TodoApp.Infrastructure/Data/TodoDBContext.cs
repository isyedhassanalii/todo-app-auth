using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TodoApp.Core.DBEntities;
using TodoApp.Core.DBEntities.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Infrastructure.Data
{
    public class TodoDBContext : IdentityDbContext<ApplicationUser>
    {
        public TodoDBContext(DbContextOptions<TodoDBContext> options)
        : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
            string defaultString = jAppSettings["ConnectionString"]["DefaultConnection"].Value<string>();
            //optionsBuilder.UseSqlServer(defaultString);
            optionsBuilder.UseSqlite(defaultString);
        }


    }


}
