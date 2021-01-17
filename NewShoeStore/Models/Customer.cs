using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewShoeStore.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "יש להכניס שם מלא")]
        [StringLength(100)]
        [Display(Name = "שם פרטי ומשפחה")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "יש להכניס עיר")]
        [StringLength(50)]
        [Display(Name = "עיר")]
        public string City { get; set; }
        [Required(ErrorMessage = "יש להכניס ארץ")]
        [StringLength(50)]
        [Display(Name = "ארץ")]
        public string Country { get; set; }
        [Required(ErrorMessage = "יש להכניס רחוב")]
        [StringLength(100)]
        [Display(Name = "רחוב")]
        public string Street { get; set; }
        [Range(0, 2000)]
        [Display(Name = "מספר בית")]
        public int HouseNumber { get; set; }
        [Required(ErrorMessage = "כתובת המייל אינה חוקית")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "כתובת המייל אינה חוקית")]
        //[RegularExpression(@"^[A-Z,a-z,0-9]+@([a-z,A-Z]+\\.)+[a-z,A-Z]{2,6}]&")]
        [Display(Name = "דואר אלקטרוני")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "יש להכניס סיסמא")]
        [StringLength(8)]
        [Display(Name = "סיסמא")]
        public string Password { get; set; }
        public Order Order { get; set; }

    }
}
