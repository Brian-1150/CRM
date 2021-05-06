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
        public new string StreetAddress { get; set; }
        public new string Email { get; set; } 
        public string PhoneNumber { get; set; } 

        [RegularExpression(@"^(?!00000)[0-9]{5,5}$", ErrorMessage = "Please enter a valid 5 digit U.S. zip code")]
        public int ZipCode { get; set; }
        public TextColor TextColor => StateOfPerson == PersonState.IN ? TextColor.Primary : TextColor.Secondary;
    }
}
