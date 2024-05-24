using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GraduateWork.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public int ToDoItemId { get; set; }

        [Display(Name = "Task:")]
        public ToDoItem ToDoItem { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [Display(Name = "Reminder time:")]
        public DateTime ReminderDate { get; set; }

        [Display(Name = "Created on:")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool? Completed { get; set; }
    }
}
