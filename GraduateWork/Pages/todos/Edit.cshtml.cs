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
using Microsoft.AspNetCore.Authorization;

namespace GraduateWork.Pages.todos
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly GraduateWork.Data.ApplicationDbContext _context;

        public EditModel(GraduateWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ToDoItemViewModel ToDoItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ToDoItems == null)
            {
                return NotFound();
            }

            var todoitem = await _context.ToDoItems
                .Include(x => x.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoitem == null)
            {
                return NotFound();
            }

            ToDoItem = new ToDoItemViewModel()
            {
                Id = todoitem.Id,
                Description = todoitem.Description,
                UserId = todoitem.User.Id
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var todoitem = await _context.ToDoItems.FirstOrDefaultAsync(m => m.Id == ToDoItem.Id);
            if (todoitem == null)
            {
                return NotFound();
            }

            todoitem.Description = ToDoItem.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(ToDoItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ToDoItemExists(int id)
        {
            return (_context.ToDoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
