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

        public PayCheckCreate GetPayCheckCreateView()
        {
            List<EmployeeListItem> listOfEmployees = _empService.GetEmployees().ToList();

            return new PayCheckCreate
            {
                ListOfEmployees = listOfEmployees,

            };
        }
        public PayCheckCreate GetPayCheckCreateView(int? employeeID)
        {
            List<EmployeeListItem> listOfEmployees = _empService.GetEmployees().ToList();
            if (employeeID.HasValue)
            {
                List<JobListItem> listOfJobs = _jobService.GetJobs((int)employeeID).ToList();

                return new PayCheckCreate
                {
                    ListOfEmployees = listOfEmployees,
                    EmployeeID = (int)employeeID,
                    ListOfJobs = listOfJobs
                };
            }
            return GetPayCheckCreateView();
        }
        public bool CreatePayCheck(PayCheckCreate model)
        {
            bool saved;
            double payCheckAmount = GetPayCheckAmount(model.ListOfSelectedJobs);
            var entity = new PayCheck
            {
                PayCheckAmount = payCheckAmount,
                EmployeeID = model.EmployeeID,
                AdjustmentNotes = new StringBuilder(" ")
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

        public PayCheckListItem GetPayCheckByID(int id)
        {
            List<int> jobIDs = GetJobIDs(id);
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .PayChecks
                    .Single(e => e.PayCheckID == id);
                return new PayCheckListItem
                {
                    PayCheckID = entity.PayCheckID,
                    EmployeeID = entity.EmployeeID,
                    PayCheckAmount = entity.PayCheckAmount,
                    JobIDs = jobIDs,
                    AdjustmentNotes = entity.AdjustmentNotes,
                    Paid = entity.Paid
                };
            }
        }

        public bool UpdatePayCheck(PayCheckEdit model, List<int> listOfJobsOnPayCheck)
        {
            double payCheckAmount = 0;
            if (model.ListOfJobsAvailable != null)  //ListOfJobsAvailable prop carries the selections made by user at edit view
            {
                payCheckAmount = GetPayCheckAmount((List<int>)model.ListOfJobsAvailable);
            }
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.PayChecks.Find(model.PayCheckID);
                entity.PayCheckAmount = payCheckAmount + model.Adjustments;
                entity.AdjustmentNotes.Append("\n" + DateTime.Now.ToShortDateString() + model.AdjustmentNotes);
                entity.Paid = model.Paid;
                if (ctx.SaveChanges() == 1)
                {
                    AddForeignKeyValueToJob(model, listOfJobsOnPayCheck);
                    return true;
                }
            }
            return false;
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

        private void AddForeignKeyValueToJob(PayCheckEdit model, List<int> listOfJobsOnPayCheck)
        {
            RemoveForeignKeyValueFromJobFirst(listOfJobsOnPayCheck);
            if (model.ListOfJobsAvailable != null)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    for (int i = 0; i < model.ListOfJobsAvailable.Count; i++)
                    {
                        var entity = ctx.Jobs.Find(model.ListOfJobsAvailable.ElementAt(i));
                        entity.PayCheckID = model.PayCheckID;
                        ctx.SaveChanges();
                    }
                }
            }
        }

        private void RemoveForeignKeyValueFromJobFirst(List<int> listOfJobsOnPayCheck)
        {
           using (var ctx = new ApplicationDbContext())
            {
                for (int i =0; i < listOfJobsOnPayCheck.Count; i++)
                {
                    var entity = ctx.Jobs.Find(listOfJobsOnPayCheck.ElementAt(i));
                    entity.PayCheckID = null;
                    ctx.SaveChanges();
                }
            }
        }

        internal int GetPayCheckID()
        {
            List<PayCheckListItem> tempList = GetPayChecks().ToList();
            return tempList.Last().PayCheckID;
        }

        private double GetPayCheckAmount(List<int> listOfSelectedJobs)
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
        public List<int> GetJobIDs(int id)
        {
            List<int> jobIDs = new List<int>();
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var job in ctx.Jobs)
                {
                    if (job.PayCheckID == id)
                        jobIDs.Add(job.JobID);
                }
                return jobIDs;
            }
        }
        public List<int> GetJobIDs(int empID, int payCheckID)
        {
            List<int> jobIDs = new List<int>();
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var job in ctx.Jobs)
                {
                    if (job.EmployeeID == empID && (job.PayCheckID is null || job.PayCheckID == payCheckID))
                        jobIDs.Add(job.JobID);
                }
                return jobIDs;
            }
        }
    }
}
