using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class CustomerCreate : Person
    {
        //Attempt at highlighting IN in EnumDropDownList
        public new PersonState StateOfPerson { get; set; } = PersonState.IN;
        [Required]
        public new string StreetAddress { get; set; }

        [Required]

        public new string Email { get; set; }

        [Required]

        public string PhoneNumber { get; set; }

        [Required]

        [RegularExpression(@"^(?!00000)[0-9]{5,5}$", ErrorMessage = "Please enter a valid 5 digit U.S. zip code")]
        [Display(Name = "Zip")]
        public int ZipCode { get; set; }
        public TextColor TextColor => StateOfPerson == PersonState.IN ? TextColor.Primary : TextColor.Secondary;
    }
}
