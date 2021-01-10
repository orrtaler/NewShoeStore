using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewShoeStore.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderShoe> Shoes { get; set; }

        [Display(Name = "תעודת זהות")]
        //[RegularExpression(@"^[0 - 9]{8}$")]
        [Required(ErrorMessage = "יש להכניס מספר תעודת זהות בן 9 ספרות ")]
        public int CardIdNumber { get; set; }

        [Required(ErrorMessage = "יש להכניס מספר כרטיס")]
        [StringLength(50)]
        [Display(Name = "מספר כרטיס")]
        public string CardName { get; set; }

        [Required]
        [Display(Name = "תאריך תפוגה")]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "יש להכניס 3ספרות בגב הכרטיס")]
        //[MinLength(3)]
        [Range(99, 10000)]
        [Display(Name = "קוד  אבטחה")]
        public int SecurityCode { get; set; }
    }
}
