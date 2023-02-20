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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace littleTokyo.Pages.Menus
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly littleTokyoContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        
        public IndexModel(littleTokyoContext context, UserManager<ApplicationUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Menu> Menu { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FoodCategory { get; set; }


        public async Task OnGetAsync()
        {
            var items = from m in _context.Menus
                        select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                items = items.Where(predicate: s => s.itemName.Contains(SearchString));
            }

            Menu = await items.ToListAsync();

        }

        public async Task<IActionResult> OnPostBuyAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            CheckoutCustomer customer = await _context.checkoutCustomers.FindAsync(user.Email);

            var item = _context.BasketItems
                .FromSqlRaw("SELECT * FROM BasketItems WHERE StockID = {0} AND BasketID = {1}", id, customer.BasketID)
                    .ToList()
                    .FirstOrDefault();

            if(item == null)
            {
                BasketItem newItem = new BasketItem
                {
                    BasketID = customer.BasketID,
                    StockID = id,
                    Quantity = 1
                };
                _context.BasketItems.Add(newItem);
                await _context.SaveChangesAsync();
            } else
            {
                item.Quantity += 1;
                _context.Attach(item).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException e)
                {
                    throw new Exception($"Basket Not Found!", e);
                }
            }
            return RedirectToPage();
        }

      

      
    }
}
