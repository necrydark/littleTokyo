using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using littleTokyo.Data;
using littleTokyo.Models;

namespace littleTokyo.Pages.Menus
{
    public class IndexModel : PageModel
    {
        private readonly littleTokyo.Data.MenuContext _context;

        public IndexModel(littleTokyo.Data.MenuContext context)
        {
            _context = context;
        }

        public IList<Menu> Menu { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string ? SearchString { get; set; }
        public SelectList ? Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ? FoodCategory { get; set; }

        public async Task OnGetAsync()
        {
            var items = from m in _context.Menus
                        select m;
            if(!string.IsNullOrEmpty(SearchString))
            {
                items = items.Where(s => s.itemName.Contains(SearchString));
            }

            Menu = await items.ToListAsync();

        }
    }
}
