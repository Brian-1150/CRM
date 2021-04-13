using CRM.Data;
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


        [Display(Name = "Choose the customer")]

        public virtual ICollection<CustomerListItem> ListOfCustomers { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
