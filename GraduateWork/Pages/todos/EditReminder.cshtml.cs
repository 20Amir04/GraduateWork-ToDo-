using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GraduateWork.Data;
using GraduateWork.Models;
using GraduateWork.ViewModels;

namespace GraduateWork.Pages.todos
{
    public class EditReminderModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditReminderModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReminderViewModel Reminder { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Reminders == null)
            {
                return NotFound();
            }

            var reminder = await _context.Reminders
                .Include(x => x.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reminder == null)
            {
                return NotFound();
            }

            Reminder = new ReminderViewModel()
            {
                Id = reminder.Id,
                ReminderDate = reminder.ReminderDate
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DateTime currentDateTime = DateTime.Now;



            if (Reminder.ReminderDate < currentDateTime)
            {
                TempData["ErrorMessage"] = "Reminder cannot be set in the past.";
                return RedirectToPage("./Reminder");
            }

            var reminder = await _context.Reminders.FirstOrDefaultAsync(m => m.Id == Reminder.Id);
            if (reminder == null)
            {
                return NotFound();
            }

            reminder.ReminderDate = Reminder.ReminderDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReminderExists(Reminder.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["SuccessEditMessage"] = "Reminder time has been changed";
            return RedirectToPage("./Reminder");
        }

        private bool ReminderExists(int id)
        {
            return (_context.Reminders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
