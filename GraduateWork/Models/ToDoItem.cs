using Microsoft.AspNetCore.Identity;

namespace GraduateWork.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IdentityUser User { get; set; }
    }
}
