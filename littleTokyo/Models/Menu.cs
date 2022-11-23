using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace littleTokyo.Models
{
    public class Menu
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string itemName { get; set; }

        [Required]
        [Display(Name = "Item Description")]
        public string itemDesc { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Column(TypeName ="decimal(18,2)")]
        public decimal itemPrice { get; set; }


        [Required]
        [Display(Name = "Category")]
        public string category { get; set; } 
        
    
    }
}
