using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GraduateWork.Data;
using GraduateWork.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GraduateWork.Pages.todos
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly GraduateWork.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(GraduateWork.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<ToDoItem> ToDoItem { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.ToDoItems != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var tasks = _context.ToDoItems.Where(x => x.User == user);

                if (!string.IsNullOrEmpty(SearchString))
                {
                    tasks = tasks.Where(s => s.Description.Contains(SearchString));
                }

                ToDoItem = await tasks.ToListAsync();
            }
        }
    }
}
