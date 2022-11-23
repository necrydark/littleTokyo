using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using littleTokyo.Data;
using littleTokyo.Models;

namespace littleTokyo.Pages.Menus
{
    public class DeleteModel : PageModel
    {
        private readonly littleTokyo.Data.MenuContext _context;

        public DeleteModel(littleTokyo.Data.MenuContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Menu Menu { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FirstOrDefaultAsync(m => m.ID == id);

            if (menu == null)
            {
                return NotFound();
            }
            else 
            {
                Menu = menu;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }
            var menu = await _context.Menus.FindAsync(id);

            if (menu != null)
            {
                Menu = menu;
                _context.Menus.Remove(Menu);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
