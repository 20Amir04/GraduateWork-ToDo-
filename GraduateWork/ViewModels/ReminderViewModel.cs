using System.ComponentModel.DataAnnotations;

namespace GraduateWork.ViewModels
{
    public class ReminderViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Reminder time:")]
        public DateTime ReminderDate { get; set; }
    }
}
