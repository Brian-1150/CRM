using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
    public class Job
    {
        // MUST be tied to one calendar event
        // MUST be tied to one employee
        // MUST be tied to one customer

        [Key, ForeignKey(nameof(CalendarEvent))]
        //Program breaks if no CalEvent is chosen.  Do I need [Required] attribute
        //Ticket # 25

        public int CalendarEventID { get; set; }
        public virtual CalendarEvent CalendarEvent { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey(nameof(PayCheck))]
        public int? PayCheckID { get; set; }

        public virtual PayCheck PayCheck { get; set; }

        [ForeignKey(nameof(Invoice))]
        public int? InvoiceID { get; set; }
        public virtual Invoice Invoice { get; set; }

        public double EmployeePay { get; set; }
        public double CustomerCharge { get; set; }
    
        public int JobID { get; set; }

        //the following strategy did not work.
        //{    private int _jobID { get; set; }
        //    set { _jobID = CalendarEventID; }
        //    get { return _jobID; }
        //}

    }
}
