using CRM.Models.Employee;
using CRM.Models.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Paycheck
{
    public class PayCheckCreate
    {
        public int EmployeeID { get; set; }
        public List<int> ListOfSelectedJobs { get; set; }
        public virtual ICollection<EmployeeListItem> ListOfEmployees { get; set; } = new List<EmployeeListItem>();
        public virtual ICollection<JobListItem> ListOfJobs { get; set; } = new List<JobListItem>();
    }
}
