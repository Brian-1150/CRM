using CRM.Data;
using CRM.Models.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.CalendarEvent
{
    public class CalendarEventEdit
    {
        [Display(Name = "ID")]
        public int CalEventID { get; set; }
        public int CustomerID { get; set; }
        public string Location { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        public DateTimeOffset Start { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        public DateTimeOffset? End { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public Color ColorOfEvent { get; set; }
        [Display(Name = "Customer")]
        public string CustomerFullName { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        [Display(Name = "Employee Pay")]
        public double EmployeePay { get; set; }
        [Display(Name = "Customer Charge")]
        public double CustomerCharge { get; set; }
        [Display(Name = "Choose the Employee")]
        public virtual ICollection<EmployeeListItem> ListOfEmployees { get; set; }

    }
}
