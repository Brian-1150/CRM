using CRM.Data;
using CRM.Models.CalendarEvent;
using CRM.Models.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Job
{
    public class JobDetail
    {
        [Display(Name = "Job ID")]
        public int JobID { get; set; }

        [Display(Name = "Event ID")]
        public int CalendarEventID { get; set; }
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeFullName { get; set; }

        
        public int CustomerID { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerFullName { get; set; }
        public string CustomerFullAddress { get; set; }

                   //Ticket #21
        //I really don't think I need these.  When query DB in service, I have access to virtual Cust, Emp, and Cal

        //public CalendarEventDetail CalendarEvent_Detail { get; set; } //Access to the date and calendarId

        //public EmployeeDetail  Employee_Detail { get; set; } // Access to Full name and ID and other details
        //public CustomerDetail Customer_Detail { get; set; } //Access to Full Name and ID and other details
        

        public int? PayCheckID { get; set; }
        
        public int? InvoiceID { get; set; }

        [Display(Name = "Paycheck Amount"), DataType(DataType.Currency)]
        public double EmployeePay { get; set; }
        [Display(Name = "Paycheck Amount"), DataType(DataType.Currency)]
        public double CustomerCharge { get; set; }
    }
}
