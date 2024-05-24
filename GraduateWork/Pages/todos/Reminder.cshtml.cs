using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GraduateWork.Data;
using GraduateWork.Models;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace GraduateWork.Pages.todos
{
    [Authorize]
    public class ReminderModel : PageModel
    {
        private readonly GraduateWork.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ReminderModel(
            GraduateWork.Data.ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public IList<Reminder> Reminder { get; set; } = default!;

        [BindProperty]
        public DateTime ReminderDate { get; set; }

        [BindProperty]
        public TimeSpan ReminderTime { get; set; }

        [BindProperty]
        public int ToDoItemId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToPage("./Index");
            }

            if (_context.Reminders != null)
            {
                Reminder = await _context.Reminders
                    .Include(r => r.ToDoItem)
                    .Where(x => x.ToDoItem.Id == id.Value)
                    .ToListAsync();
            }
            
            ToDoItemId = id.Value;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DateTime currentDateTime = DateTime.Now;


            DateTime reminderDateTime = ReminderDate.Date.Add(ReminderTime);


            if (reminderDateTime < currentDateTime)
            {
                TempData["ErrorMessage"] = "Reminder cannot be set in the past.";
                return RedirectToPage("./Index");
            }

            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var email = user.Email;

                var toDoItem = await _context.ToDoItems.FindAsync(ToDoItemId);

                if (toDoItem != null && toDoItem.User == user)
                {
                    var reminder = new Reminder
                    {
                        ReminderDate = reminderDateTime,
                        ToDoItemId = ToDoItemId,
                        UserId = user.Id,
                        CreatedAt = DateTime.Now
                    };

                    _context.Reminders.Add(reminder);

                    await _context.SaveChangesAsync();
                }
            }
            TempData["SuccessMessage"] = "Reminder set successfully";
            return RedirectToPage("./Index");
        }



    }
}
