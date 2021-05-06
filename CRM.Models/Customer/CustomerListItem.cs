using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models
{
   public class CustomerListItem
    {
        [Display(Name = "ID")]
        public int CustomerID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }
        public CustomerStatus StatusOfCustomer { get; set; }
        public PersonState StateOfPerson { get; set; }

        [Display(Name = "Initial Contant")]
        public DateTimeOffset InitialDateOfContact { get; set; }
    }
}
