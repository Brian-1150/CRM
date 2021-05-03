using CRM.Data;
using CRM.Models.Job;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Invoice
{
    public class InvoiceCreate
    {
        public int CustomerID { get; set; }

      public  List<int> ListOfSelectedJobs { get; set; }

        [Display(Name = "Choose the customer")]

        public virtual ICollection<CustomerListItem> ListOfCustomers { get; set; } = new List<CustomerListItem>();
        public virtual ICollection<JobListItem> ListOfJobs { get; set; } = new List<JobListItem>();
    }
}
