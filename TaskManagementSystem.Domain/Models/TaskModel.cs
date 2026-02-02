using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Models
{
    public class TaskItem
    {
        public int Id { get; set; }                   

        public string Title { get; set; } = default!; 

        public string? Description { get; set; }      

        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }       

        public DateTime? DueDate { get; set; }        
    }
}
