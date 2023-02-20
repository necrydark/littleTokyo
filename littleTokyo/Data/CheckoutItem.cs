using System.ComponentModel.DataAnnotations;

namespace littleTokyo.Data
{
    public class CheckoutItem
    {
        [Key, Required]
        public int ID { get; set; }

        [Required]
        public decimal itemPrice { get; set; }

        [Required, StringLength(50)]
        public string itemName { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
