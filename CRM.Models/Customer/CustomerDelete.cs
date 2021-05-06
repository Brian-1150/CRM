using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models
{
     public class CustomerDelete:Person
    {
        [Display(Name = "ID")]
        public int CustomerID { get; set; }

        [Display(Name = "Initial Contact")]
        public DateTimeOffset InitialDateOfContact { get; set; }

        public CustomerStatus StatusOfCustomer { get; set; }

        [Display(Name = "ON DO NOT CALL LIST")]
        public bool IsOnDoNotContactList { get; set; }
    }
}
