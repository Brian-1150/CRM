using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
    public class Employee : Person
    {
        //Tied to many Jobs, CalEvents, and Paychecks

        [Key]
        public int EmployeeID { get; set; }
        public bool Current { get; set; }

        public DateTimeOffset HireDate { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<PayCheck> Paychecks { get; set; }
        public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }

    }
}
