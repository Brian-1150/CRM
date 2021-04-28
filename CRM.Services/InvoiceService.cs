using CRM.Data;
using CRM.Models;
using CRM.Models.Invoice;
using CRM.Models.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class InvoiceService
    {
        private CustomerService _custService = new CustomerService();
        private JobService _jobService = new JobService();

        public InvoiceService() { }

        public InvoiceCreate GetInvoiceCreatView()
        {
            List<CustomerListItem> listOfCustomers = _custService.GetCustomers().ToList();
            ICollection<JobListItem> listOfJobs = (ICollection<JobListItem>)_jobService.GetJobs();
            //create filter for list of jobs to display as option for adding to invoice *see paycheck to mimic strategy
            //ticket # 27
            return new InvoiceCreate
            {
                ListOfCustomers = listOfCustomers,
                ListOfJobs = listOfJobs
            };
        }
        public bool CreateInvoice(InvoiceCreate model)
        {
            bool saved;
            double invoiceAmount = 0;
            using (var ctx = new ApplicationDbContext())
            {

                //Put for loop in helper method
                //Ticket #26
                for (int i = 0; i < model.ListOfSelectedJobs.Count; i++)
                {
                    invoiceAmount +=
                       ctx.Jobs.Find(model.ListOfSelectedJobs.ElementAt(i))
                       .CustomerCharge;

                }
                var entity = new Invoice()
                {
                    CustomerID = model.CustomerID,
                    InvoiceAmount = invoiceAmount,

                };
                ctx.Invoices.Add(entity);
                saved = (ctx.SaveChanges() == 1);
            }
            if (saved)
            {
                AddForeignKeyValueToJob(model);
                return true;
            }
            return false;

        }
        public IEnumerable<InvoiceListItem> GetInvoices()
        {

            using (var ctx = new ApplicationDbContext())
            {

                var query =
                    ctx
                    .Invoices
                    .Where(e => e.InvoiceID >= 0)
                    .Select(
                        e =>
                        new InvoiceListItem
                        {
                            InvoiceID = e.InvoiceID,
                            CustomerID = e.CustomerID,
                            //JobIDs = GetJobIDs(e.InvoiceID),
                            InvoiceAmount = e.InvoiceAmount,
                            Paid = e.Paid
                        });
                return query.ToArray();
            }
        }
        //Get invoices for specific customerID
        public IEnumerable<InvoiceListItem> GetInvoices(int id)
        {

            using (var ctx = new ApplicationDbContext())
            {

                var query =
                    ctx
                    .Invoices
                    .Where(e => e.CustomerID == id)
                    .Select(
                        e =>
                        new InvoiceListItem
                        {
                            InvoiceID = e.InvoiceID,
                            CustomerID = e.CustomerID,
                            InvoiceAmount = e.InvoiceAmount,
                            Paid = e.Paid
                        });
                return query.ToArray();
            }
        }
        public InvoiceListItem GetInvoiceByID(int id)
        {
            List<int> jobIDs = GetJobIDs(id);
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Invoices
                    .Single(e => e.InvoiceID == id);

                return
                    new InvoiceListItem
                    {
                        InvoiceID = id,
                        CustomerID = entity.CustomerID,
                        JobIDs = jobIDs,
                        InvoiceAmount = entity.InvoiceAmount,
                        Paid = entity.Paid
                    };
            }

        }
        public bool UpdateInvoice(InvoiceEdit model, List<int> listOfJobsOnInvoice)
        {

            double invoiceAmount = 0;
            using (var ctx = new ApplicationDbContext())
            {
                if (model.ListOfJobsAvailable != null)
                {
                    for (int i = 0; i < model.ListOfJobsAvailable.Count; i++)
                    {
                        invoiceAmount +=
                           ctx.Jobs.Find(model.ListOfJobsAvailable.ElementAt(i))
                           .CustomerCharge;
                    }
                }
                var entity = ctx
                    .Invoices
                    .Find(model.InvoiceID);

                entity.InvoiceAmount = invoiceAmount + model.Adjustments;
                entity.AdjustmentNotes = model.AdjustmentNotes;
                entity.Paid = model.Paid;  // make "mark paid" option in list view.  ticket # 28
                if (ctx.SaveChanges() == 1)
                {
                    AddForeignKeyValueToJob(model, listOfJobsOnInvoice);
                    return true;
                }
            }
            return false;
        }

        //Helper Methods

        public List<int> GetJobIDs(int id)
        {
            List<int> jobIDs = new List<int>();
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var job in ctx.Jobs)
                {
                    if (job.InvoiceID == id)
                        jobIDs.Add(job.JobID);
                }
                return jobIDs;
            }
        }
        public List<int> GetJobIDs(int customerID, int invoiceID)
        {
            List<int> jobIDs = new List<int>();
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var job in ctx.Jobs)
                {
                    if (job.CustomerID == customerID && (job.InvoiceID is null || job.InvoiceID == invoiceID))
                        jobIDs.Add(job.JobID);
                }
                return jobIDs;
            }
        }

        private void AddForeignKeyValueToJob(InvoiceCreate model)
        {
            int invoiceId = GetInvoiceId();
            using (var ctx = new ApplicationDbContext())
            {
                for (int i = 0; i < model.ListOfSelectedJobs.Count; i++)
                {
                    var entity = ctx.Jobs.Find(model.ListOfSelectedJobs.ElementAt(i));
                    entity.InvoiceID = invoiceId;
                    ctx.SaveChanges();

                }
            }
        }
        private void AddForeignKeyValueToJob(InvoiceEdit model, List<int> listOfJobsOnInvoice)
        {
            RemoveForeignKeyValueFromJobFirst(listOfJobsOnInvoice);
            using (var ctx = new ApplicationDbContext())
            {
                for (int i = 0; i < model.ListOfJobsAvailable.Count; i++)
                {
                    var entity = ctx.Jobs.Find(model.ListOfJobsAvailable.ElementAt(i));
                    entity.InvoiceID = model.InvoiceID;
                    ctx.SaveChanges();

                }
            }
        }

        internal int GetInvoiceId()
        {
            List<InvoiceListItem> tempList = GetInvoices().ToList();
            return tempList.Last().InvoiceID;
        }

        private void RemoveForeignKeyValueFromJobFirst(List<int> listOfJobsOnInvoice)
        {
            using (var ctx = new ApplicationDbContext())
            {
                for (int i = 0; i < listOfJobsOnInvoice.Count; i++)
                {
                    var entity = ctx.Jobs.Find(listOfJobsOnInvoice.ElementAt(i));
                    entity.InvoiceID = null;
                    ctx.SaveChanges();
                }
            }
        }
    }
}