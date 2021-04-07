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
        public virtual CalendarEvent CalendarEvent { get; set; }
        public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }
        public DateTimeOffset InitialDateOfService { get; set; }

    }
    public enum Status { Prospect, Active, Inactive}
}
