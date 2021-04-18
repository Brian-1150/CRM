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
        private readonly Guid _userId;
        private CustomerService _custService = new CustomerService();
        private EmployeeService _empService = new EmployeeService();
        private CalendarEventService _calEventService = new CalendarEventService();

        public JobService() { }
        public JobService(Guid userId)
        {
            _userId = userId;
        }


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

        //internal void AddForeignKeyValue(InvoiceCreate model)
        //{
        //    for (int i = 0; i < model.ListOfSelectedJobs.Count; i++)
        //    {
        //        using (var ctx = new ApplicationDbContext())
        //        {
        //            var entity =
        //            ctx.Jobs.Find(model.ListOfSelectedJobs.ElementAt(i));
        //            entity.InvoiceID = model.
        //        }
        //    }
        //}

        public bool CreateJob(JobCreate model)
        {


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
                    .Where(e => e.EmployeeID == empID)
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


        //Helper Mehtods
        //public IEnumerable<int> GetJobs(int id)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //            .Jobs
        //            .Where(e => e.InvoiceID == id)
        //            .Select(
        //                e =>
        //                new JobListItem
        //                {
        //                    JobID = e.JobID,
        //                    CalendarEventID = e.CalendarEventID,
        //                    CustomerID = e.CustomerID,
        //                    EmployeeID = e.EmployeeID,
        //                    EmployeePay = e.EmployeePay,
        //                    CustomerCharge = e.CustomerCharge,
        //                    PayCheckID = e.PayCheckID,
        //                    InvoiceID = e.InvoiceID
        //                });
        //        return query.ToArray();
        //    }
        //}


    }
}
