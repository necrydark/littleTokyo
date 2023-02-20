using System.ComponentModel.DataAnnotations;

namespace littleTokyo.Data
{
    public class BasketItem
    {
        [Required]
        public int StockID { get; set; }

        [Required]
        public int BasketID { get; set; }

        [Required]
        public int Quantity { get; set; }

    }
}
