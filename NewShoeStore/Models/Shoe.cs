using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewShoeStore.Models
{
    public class Shoe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "שם מוצר")]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "צבע")]
        public string Color { get; set; }
        [Range(0, 5000)]
        [Display(Name = "מחיר")]
        public float Price { get; set; }
        [Required]
        [StringLength(10000)]
        [Display(Name = "תאור מוצר")]
        public string ProductDescription { get; set; }
        [Required]
        [Display(Name = "תמונה")]
        public string Img { get; set; }
        
        [Display(Name = "רשימת הזמנות למוצר")]
        public ICollection<OrderShoe> Orders { get; set; }
        [Display(Name = "כמות צפיות במוצר")]
        public int Views { get; set; }
        [Required]
        [Range(3,46)]
        [Display(Name = "מידה")]
        public int Size { get; set; }
        [Required]
        [Display(Name = "קטגוריה")]
        public string Category { get; set; }
    }
}
