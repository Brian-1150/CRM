using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
    public class Customer: Person
    {
        [Key]
        public int CustomerID { get; set; }
        
        public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }


        public DateTimeOffset InitialDateOfContact { get; set; }
        public CustomerStatus StatusOfCustomer { get; set; }
        //add Estimate prop ticket # 1
        public bool IsOnDoNotContactList { get; set; }
    }
    public enum CustomerStatus { Prospect, Active, Inactive}
}
