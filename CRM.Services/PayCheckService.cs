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
        private InvoiceService _invoiceService = new InvoiceService();

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
        public bool CreatePayCheck(PayCheckCreate model)
        {
            bool saved;
            double payCheckAmount = GetInvoiceAmount(model.ListOfSelectedJobs);
            var entity = new PayCheck
            {
                PayCheckAmount = payCheckAmount,
                EmployeeID = model.EmployeeID,
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.PayChecks.Add(entity);
                saved = (ctx.SaveChanges() == 1);
            }
            if (saved)
            {
                AddForeignKeyValueToJob(model);
                return true;
            }
            return false;
        }

        public IEnumerable<PayCheckListItem> GetPayChecks()
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query =
                    ctx
                    .PayChecks
                    .Where(e => e.PayCheckID >= 0)
                    .Select(
                        e =>
                        new PayCheckListItem
                        {
                            PayCheckID = e.PayCheckID,
                            EmployeeID = e.EmployeeID,
                            PayCheckAmount = e.PayCheckAmount,
                            AdjustmentNotes = e.AdjustmentNotes,
                            Paid = e.Paid
                        });
                return query.ToArray();
            }
        }



        //Helper Methods

        private void AddForeignKeyValueToJob(PayCheckCreate model)
        {
            int payCheckID = GetPayCheckID();
            using (var ctx = new ApplicationDbContext())
            {
                for (int i = 0; i < model.ListOfSelectedJobs.Count; i++)
                {
                    var entity = ctx.Jobs.Find(model.ListOfSelectedJobs.ElementAt(i));
                    entity.PayCheckID = payCheckID;
                    ctx.SaveChanges();

                }
            }
        }
        internal int GetPayCheckID()
        {
            List<PayCheckListItem> tempList = GetPayChecks().ToList();
            return tempList.Last().PayCheckID;
        }

        private double GetInvoiceAmount(List<int> listOfSelectedJobs)
        {
            double payCheckAmount = 0;
            using (var ctx = new ApplicationDbContext())
            {
                for (int i = 0; i < listOfSelectedJobs.Count; i++)
                {
                    payCheckAmount +=
                      ctx.Jobs.Find(listOfSelectedJobs.ElementAt(i))
                         .EmployeePay;
                }
                return payCheckAmount;
            }
        }
    }
}
