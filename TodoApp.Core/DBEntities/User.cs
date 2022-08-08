using TodoApp.Core.DBEntities.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.DBEntities
{
    public class User : BaseEntity
    {
        [MaxLength(500)]
        public string Name { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }        
        [MaxLength(200)]
        public string ApplicationUserId { get; set; }
        public virtual List<Todo> Todos { get; set; }
    }
}
