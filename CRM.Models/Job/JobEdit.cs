using CRM.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Job
{
    public class JobEdit
    {

        public int JobID { get; set; }

        public int CalendarEventID { get; set; }

        public int CustomerID { get; set; }


        public int EmployeeID { get; set; }
        public virtual ICollection<EmployeeListItem> ListOfEmployees { get; set; }

        public double EmployeePay { get; set; }
        public double CustomerCharge { get; set; }
    }
}
