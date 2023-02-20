using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace littleTokyo.Models
{
    public class Menu
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        [StringLength(50, MinimumLength = 3)]
        public string itemName { get; set; }

        [Required]
        [Display(Name = "Item Description")]
        [StringLength(250, MinimumLength = 10)]
        public string itemDesc { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Column(TypeName ="decimal(18,2)")]
        public decimal itemPrice { get; set; }


        [Required]
        [Display(Name = "Category")]
        public string category { get; set; }

        [Display(Name = "Image Description")]
        public string ImageDescription { get; set; }

        [Display(Name = "Image")]
        public byte[] ImageData { get; set; } 
    
    }
}
