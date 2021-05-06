using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Job
{
    public class JobListItem
    {
        [Display(Name = "Job ID")]
        public int JobID { get; set; }


        [Display(Name = "Event ID")]

        public int CalendarEventID { get; set; }

        public int CustomerID { get; set; }

        public int EmployeeID { get; set; }

        public int? PayCheckID { get; set; }

        public int? InvoiceID { get; set; }

        [Display(Name = "Paycheck Amount"), DataType(DataType.Currency)]
        public double EmployeePay { get; set; }

        [Display(Name = "Paycheck Amount"), DataType(DataType.Currency)]
        public double CustomerCharge { get; set; }
    }
}
