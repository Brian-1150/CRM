using CRM.Data;
using CRM.Models.Employee;
using CRM.Models.Job;
using CRM.Models.Paycheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class PayCheckService
    {
        private EmployeeService _empService = new EmployeeService();
        private JobService _jobService = new JobService();

        public PayCheckService() { }

        public PayCheckCreate GetInvoiceCreateView()
        {
            List<EmployeeListItem> listOfEmployees = _empService.GetEmployees().ToList();

            return new PayCheckCreate
            {
                ListOfEmployees = listOfEmployees,

            };
        }
        public PayCheckCreate GetInvoiceCreateView(int? employeeID)
        {
            List<EmployeeListItem> listOfEmployees = _empService.GetEmployees().ToList();
            List<JobListItem> listOfJobs = new List<JobListItem>();
            if (employeeID.HasValue)
            {
                listOfJobs = _jobService.GetJobs((int)employeeID).ToList();

                return new PayCheckCreate
                {
                    ListOfEmployees = listOfEmployees,
                    EmployeeID = (int)employeeID,
                    ListOfJobs = listOfJobs
                };
            }
            return GetInvoiceCreateView();
        }

        
    }
}
