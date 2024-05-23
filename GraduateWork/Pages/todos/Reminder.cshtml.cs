﻿using System;
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

        [BindProperty]
        public DateTime ReminderDate { get; set; }

        [BindProperty]
        public TimeSpan ReminderTime { get; set; }

        [BindProperty]
        public int ToDoItemId { get; set; }

        public IActionResult OnGetAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("TbI SHO PES");
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
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.Reminders.Add(reminder);

                    await _context.SaveChangesAsync();

                    //reminderDateTime = ReminderDate.Date.Add(ReminderTime);

                    //var message = ToDoItem.Description;

                    //await _emailSender.SendEmailAsync(email, "Reminder", message);
                }
            }
            TempData["SuccessMessage"] = "Reminder set successfully";
            return RedirectToPage("./Index");
        }

    }
}
