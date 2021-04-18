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
   public class JobCreate
    {
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public int CalendarEventID { get; set; }
        public double CustomerCharge { get; set; }
        public double EmployeePay { get; set; }

        [Display(Name = "Choose the customer")]
        public virtual ICollection<CustomerListItem> ListOfCustomers { get; set; }

        [Display(Name = "Choose the employee")]
        public virtual ICollection<EmployeeListItem> ListOfEmployees { get; set; }
        public virtual ICollection<CalendarEventListItem> ListOfCalEvents { get; set; }
    }
}
