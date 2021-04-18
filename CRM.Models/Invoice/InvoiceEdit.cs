using CRM.Models.Job;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Invoice
{
    public class InvoiceEdit
    {
        public int InvoiceID { get; set; }

        public int CustomerID { get; set; }

        [Display(Name = "Jobs Currently on Invoice")]
        public List<int> ListOfJobsOnInvoice { get; set; } = new List<int>();

        [Display(Name = "Add/Remove as necessary to override jobs on this invoice.")]
        public List<int> ListOfJobsAvailable { get; set; } = new List<int>();

        [Display(Name ="Current invoice total.  Click save to see new total.")]
        [DataType(DataType.Currency)]
        public double InvoiceAmount { get; set; }

        [Display(Name ="Additional charges/discounts(use negative sign)")]
        public double Adjustments { get; set; }
        public string AdjustmentNotes { get; set; }
        public bool Paid { get; set; }
    }
}
