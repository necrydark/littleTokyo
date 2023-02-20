using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace littleTokyo.Data
{
    public class OrderItem
    {
        [Key, Required]
        public int OrderNo { get; set; }

        [Required]
        public int StockID { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
