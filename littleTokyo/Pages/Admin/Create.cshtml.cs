using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using littleTokyo.Data;
using littleTokyo.Models;
using System.IO;

namespace littleTokyo.Pages.Menus
{
    public class CreateModel : PageModel
    {
        private readonly littleTokyoContext _context;
        [BindProperty]
        public Menu Menu { get; set; }

        public CreateModel(littleTokyoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

      
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid) { return Page(); }

          foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                Menu.ImageData = ms.ToArray();
                ms.Close();
                ms.Dispose();
            }

            _context.Menus.Add(Menu);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Menus/Index");
        }
    }
}
