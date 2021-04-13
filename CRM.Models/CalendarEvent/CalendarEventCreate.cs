using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.CalendarEvent
{
    public class CalendarEventCreate
    {
        

        public virtual ICollection<CustomerListItem> ListOfCustomers { get; set; }
        public int CustomerID { get; set; }

        public int EmployeeID { get; set; }
        public virtual Customer Customer { get; set; }

        [Required]
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset? End { get; set; }
        public string Title { get; set; }
        public EventColor ColorOfEvent { get; set; }
        public string Details { get; set; }


    }
}
