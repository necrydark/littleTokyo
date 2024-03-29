﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using littleTokyo.Data;
using littleTokyo.Models;
using System.IO;

namespace littleTokyo.Pages.Menus
{
    public class EditModel : PageModel
    {
        private readonly littleTokyoContext _context;

        public EditModel(littleTokyoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Menu Menu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu =  await _context.Menus.FirstOrDefaultAsync(m => m.ID == id);
            if (menu == null)
            {
                return NotFound();
            }
            Menu = menu;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                Menu.ImageData = ms.ToArray();
                ms.Close();
                ms.Dispose();
            }


            _context.Attach(Menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(Menu.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Menus/Index");
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.ID == id);
        }
    }
}
