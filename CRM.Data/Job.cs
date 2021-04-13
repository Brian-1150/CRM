using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
   public  class Job
    {
        [Key, ForeignKey(nameof(CalendarEvent))]
        
        public int JobID { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey(nameof(PayCheck))]
        public int? PayCheckID { get; set; }
        public virtual PayCheck Paycheck { get; set; }

        [ForeignKey(nameof(Invoice))]
        public int? InvoiceID { get; set; }
        public virtual Invoice Invoice { get; set; }

        [ForeignKey(nameof())]
        public double EmployeePay { get; set; }
        public double CustomerCharge { get; set; }
    }
}
