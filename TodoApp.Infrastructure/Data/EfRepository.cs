using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Data;
using TodoApp.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : BaseEntity
    {
        public EfRepository(TodoDBContext dbContext) : base(dbContext)
        {

        }

    }
}
