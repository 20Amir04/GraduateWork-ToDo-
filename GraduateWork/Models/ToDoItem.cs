using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GraduateWork.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        [Display(Name = "Task:")]
        public string Description { get; set; }
        public IdentityUser User { get; set; }
    }
}
