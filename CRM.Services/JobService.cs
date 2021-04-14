using CRM.Data;
using CRM.Models;
using CRM.Models.Employee;
using CRM.Models.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class JobService
    {
        private readonly Guid _userId;
        private CustomerService _custService = new CustomerService();
        private EmployeeService _empService = new EmployeeService();
        public JobService(Guid userId)
        {
            _userId = userId;
        }


        public JobCreate JobCreateView()
        {

            List<CustomerListItem> listOfCustomers = _custService.GetCustomers().ToList();
            List<EmployeeListItem> listOfEmployees = _empService.GetEmployees().ToList();
            return new JobCreate
            {
                ListOfCustomers = listOfCustomers,
                ListOfEmployees = listOfEmployees
            };
        }
        public bool CreateJob(JobCreate model)
        {


            var entity = new Job()
            {
                CustomerID = model.CustomerID,
                CalendarEventID = model.CalendarEventID,
                EmployeeID = model.EmployeeID,
                CustomerCharge = model.CustomerCharge,
                EmployeePay = model.EmployeePay,

            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Jobs.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }

        public IEnumerable<JobListItem> GetJobs()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Jobs
                    .Where(e => e.JobID >= 0)
                    .Select(
                        e =>
                        new JobListItem
                        {
                            JobID = e.JobID,
                            CalendarEventID = e.CalendarEventID,
                            CustomerID = e.CustomerID,
                            EmployeeID = e.EmployeeID,
                            EmployeePay = e.EmployeePay,
                            CustomerCharge = e.CustomerCharge,
                            PayCheckID = e.PayCheckID,
                            InvoiceID = e.InvoiceID
                        });
                return query.ToArray();
            }
        }

        public JobDetail GetJobByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Jobs
                    .Single(e => e.JobID == id);


                return
                    new JobDetail
                    {
                        JobID = entity.JobID,
                        CalendarEventID = entity.CalendarEventID,

                        CustomerID = entity.CustomerID,
                        CustomerFullName = entity.Customer.FullName,
                        CustomerFullAddress = entity.Customer.FullName,
                        EmployeeID = entity.EmployeeID,
                        EmployeeFullName = entity.Employee.FullName,

                        EmployeePay = entity.EmployeePay,
                        CustomerCharge = entity.CustomerCharge,
                        PayCheckID = entity.PayCheckID,
                        InvoiceID = entity.InvoiceID
                    };
            }
        }
    }
}
