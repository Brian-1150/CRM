using CRM.Data;
using CRM.Data.Deleted;
using CRM.Models;
using CRM.Models.CalendarEvent;
using CRM.Models.Employee;
using CRM.Models.Invoice;
using CRM.Models.Job;
using CRM.Models.Paycheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class JobService
    {

        private CustomerService _custService = new CustomerService();
        private EmployeeService _empService = new EmployeeService();
        private CalendarEventService _calEventService = new CalendarEventService();

        public JobService() { }

        public JobCreate GetJobCreateView()
        {

            List<CustomerListItem> listOfCustomers = _custService.GetCustomers().ToList();
            List<EmployeeListItem> listOfEmployees = _empService.GetEmployees().ToList();
            List<CalendarEventListItem> listOfCalEvents = _calEventService.GetAvailableCalEvents();
            return new JobCreate
            {
                ListOfCustomers = listOfCustomers,
                ListOfEmployees = listOfEmployees,
                ListOfCalEvents = listOfCalEvents
            };
        }
        public JobCreate GetJobCreateViewForCalEvent()
        {

            List<CustomerListItem> listOfCustomers = _custService.GetCustomers().ToList();
            List<EmployeeListItem> listOfEmployees = _empService.GetEmployees().ToList();
            int id = _calEventService.GetLastCalEventID();
            return new JobCreate
            {
                ListOfCustomers = listOfCustomers,
                ListOfEmployees = listOfEmployees,
                CalendarEventID = id
            };
        }

        public bool CreateJob(JobCreate model)
        {
            ChangeCustStatus(model.CustomerID); //if customer happened to be inactive, scheduling a new job for them reactivates their status
            _calEventService.AssignColorToCalEvent(model.EmployeeID, model.CalendarEventID);
            _calEventService.SetCalEventLocationAndTitle(model.CustomerID, model.CalendarEventID);
            var entity = new Job()
            {
                CustomerID = model.CustomerID,
                CalendarEventID = model.CalendarEventID,
                EmployeeID = model.EmployeeID,
                CustomerCharge = model.CustomerCharge,
                EmployeePay = model.EmployeePay,
                JobID = model.CalendarEventID

            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Jobs.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }

        public IEnumerable<Job> GetJobsFromDB()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Jobs.Include("Customer").Include("Employee").ToList();
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
        public IEnumerable<JobListItem> GetJobs(int empID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Jobs
                    .Where(e => e.EmployeeID == empID && e.PayCheckID == null)
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
        public IEnumerable<JobListItem> GetJobsByCustomerID(int custID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Jobs
                    .Where(e => e.CustomerID == custID && e.InvoiceID == null)
                    .Select(
                        e =>
                        new JobListItem
                        {
                            JobID = e.JobID,
                            CalendarEventID = e.CalendarEventID,
                            CustomerID = e.CustomerID,
                            EmployeeID = e.EmployeeID,

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
                        CustomerFullAddress = entity.Customer.FullAddress,
                        EmployeeID = entity.EmployeeID,
                        EmployeeFullName = entity.Employee.FullName,
                        EmployeePay = entity.EmployeePay,
                        CustomerCharge = entity.CustomerCharge,
                        PayCheckID = entity.PayCheckID,
                        InvoiceID = entity.InvoiceID
                    };
            }
        }

        public bool UpdateJob(JobEdit model)
        {
            _calEventService.AssignColorToCalEvent(model.EmployeeID, model.CalendarEventID);
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Jobs
                    .Find(model.JobID);

                entity.EmployeeID = model.EmployeeID;
                entity.EmployeePay = model.EmployeePay;
                entity.CustomerCharge = model.CustomerCharge;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteJob(int id)
        {
            StringBuilder sb = new StringBuilder();

            using (var ctx = new ApplicationDbContext())
            {
                var entityToDelete =
                    ctx
                    .Jobs
                    .Find(id);

                var copyOfDeletedEntity = new JobDeleted
                {
                    JobID = entityToDelete.JobID,
                    CalendarEventID = entityToDelete.CalendarEventID,
                    CustomerID = entityToDelete.CustomerID,
                    EmployeeID = entityToDelete.EmployeeID,
                    EmployeePay = entityToDelete.EmployeePay,
                    CustomerCharge = entityToDelete.CustomerCharge
                };
                ctx.JobsDeleted.Add(copyOfDeletedEntity);
                if (ctx.SaveChanges() == 1)
                {
                    ctx.Jobs.Remove(entityToDelete);
                    return ctx.SaveChanges() == 1;
                }
                return false;

            }
        }

        //Helper Methods

        private void ChangeCustStatus(int customerID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Customers.Find(customerID);
                if (entity.StatusOfCustomer != CustomerStatus.Active)
                {
                    entity.StatusOfCustomer = CustomerStatus.Active;
                    ctx.SaveChanges();
                }
            }
        }


    }
}
