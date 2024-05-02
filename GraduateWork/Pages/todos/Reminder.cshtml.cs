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

namespace GraduateWork.Pages.todos
{
    [Authorize]
    public class ReminderModel : PageModel
    {
        private readonly GraduateWork.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReminderModel(GraduateWork.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public DateTime ReminderDate { get; set; }

        [BindProperty]
        public TimeSpan ReminderTime { get; set; }

        [BindProperty]
        public int DescriptionId { get; set; }

        public IActionResult OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var email = user.Email;

                var ToDoItem = await _context.ToDoItems.FindAsync(DescriptionId);

                if (ToDoItem != null && ToDoItem.User == user)
                {
                    var reminderDateTime = ReminderDate.Date.Add(ReminderTime);

                    var message = ToDoItem.Description;

                    await SendEmailReminder(email, reminderDateTime, message);
                }
            }
            return RedirectToPage("ReminderSuccess");
        }

        private async Task SendEmailReminder(string email, DateTime reminderDateTime, string message)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.Credentials = new NetworkCredential("your.todos.zieit@gmail.com", "1234ZIEIT8765");
                client.EnableSsl = true;

                var mailMessage = new MailMessage("your.todos.zieit@gmail.com", email)
                {
                    Subject = $"Reminder for {reminderDateTime}",
                    Body = message
                };

                await client.SendMailAsync(mailMessage);
            }
        }


    }
}