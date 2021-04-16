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



        //Make this class without GUID constructor and see if we can still validate authorized user by using [Authorize] on controller
        //ticket #24
        public InvoiceService()
        {

        }

        public InvoiceCreate GetInvoiceCreatView()
        {
            List<CustomerListItem> listOfCustomers = _custService.GetCustomers().ToList();
            ICollection<JobListItem> listOfJobs = (ICollection<JobListItem>)_jobService.GetJobs();

            return new InvoiceCreate
            {
                ListOfCustomers = listOfCustomers,
                ListOfJobs = listOfJobs
            };
        }
        public bool CreateInvoice(InvoiceCreate model)
        {
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
                if (ctx.SaveChanges() == 1)
                {
                    //_jobService.AddForeignKeyValueToJob(model);
                    return true;
                }
                return false;

            }
        }
    }
}