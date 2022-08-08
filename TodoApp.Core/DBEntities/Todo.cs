using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.DBEntities
{
    public class Todo : BaseEntity
    {
        
        public string Title { get; set; }
        public TodoStatus Status { get; set; }

        public User Customer { get; set; }

        public int CustomerId { get; set; }

    }
    public enum TodoStatus {
        InProgress,
        Active,
        Pending,
        Done
    }
}
