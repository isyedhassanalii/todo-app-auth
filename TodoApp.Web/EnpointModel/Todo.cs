using TodoApp.Core.DBEntities;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Web.EnpointModel
{
  
    public class TodoRequest
    {
        [MaxLength(500)]
        [Required]
        public string Title { get; set; }

        public string UserId { get; set; }


    }

    public class TodoResponse
    {
        public int TodoId { get; set; }
        public string Title { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }


        public TodoStatus Status { get; set; }


    }


    public class TodoUpdateRequest
    {
        [Required]
        public int TodoId { get; set; }
                
        public TodoStatus Status { get; set; }

    }
}
