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
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;

namespace GraduateWork.Pages.todos
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly GraduateWork.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(GraduateWork.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ToDoItemViewModel ToDoItem { get; set; } = default!;



        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.ToDoItems == null || ToDoItem == null || string.IsNullOrWhiteSpace(ToDoItem.Description))
            {
                return Page();
            }


            var dto = new ToDoItem()
            {
                Description = ToDoItem.Description,
                User = await _userManager.GetUserAsync(User)
            };

            _context.ToDoItems.Add(dto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }
    }
}
