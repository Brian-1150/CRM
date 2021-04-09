using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models
{
     public class CustomerDelete:Person
    {
        public int CustomerID { get; set; }
        public DateTimeOffset InitialDateOfContact { get; set; }
        public CustomerStatus StatusOfCustomer { get; set; }
        public bool IsOnDoNotContactList { get; set; }
    }
}
