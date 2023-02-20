using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using littleTokyo.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace littleTokyo.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public OrderHistory Order = new OrderHistory();
        private readonly littleTokyoContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public IList<CheckoutItem> Items { get; private set; }

        public decimal Total;
        public long amountPayable;

        public CheckoutModel(littleTokyoContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            CheckoutCustomer customer = await _context.checkoutCustomers.FindAsync(user.Email);


            Items = _context.CheckoutItems.FromSqlRaw("SELECT Menu.ID, Menu.itemPrice, Menu.itemName, BasketItems.BasketID, BasketItems.Quantity " +
                "FROM Menu INNER JOIN BasketItems ON Menu.ID = BasketItems.BasketID " +
                "WHERE BasketID = {0}", customer.BasketID).ToList();

            Total = 0;

            foreach(var item in Items)
            {
                Total += (item.Quantity * item.itemPrice);
            }
            amountPayable = (long)Total;
        }

        public async Task<IActionResult> OnPostBuyAsync()
        {
            var currentOrder = _context.OrderHistories.FromSqlRaw("SELECT * From OrderHistories")
                .OrderByDescending(b => b.OrderNo)
                .FirstOrDefault();
            if(currentOrder == null)
            {
                Order.OrderNo = 1;
            } 
            else
            {
                Order.OrderNo = currentOrder.OrderNo + 1;
            }

            var user = await _userManager.GetUserAsync(User);
            Order.Email = user.Email;
            _context.OrderHistories.Add(Order);

            CheckoutCustomer customer = await _context
                .checkoutCustomers
                .FindAsync(user.Email);

            var basketItems =
                _context.BasketItems
                .FromSqlRaw("SELECT * From BasketItems WHERE BasketID = {0}", customer.BasketID)
                .ToList();

            foreach(var item in basketItems)
            {
                OrderItem oi = new OrderItem
                {
                    OrderNo = Order.OrderNo,
                    StockID = item.StockID,
                    Quantity = item.Quantity
                };

                _context.OrderItems.Add(oi);
                _context.BasketItems.Remove(item);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    
    }
}
