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
        [Display(Name = "Invoice")]
        public int InvoiceID { get; set; }

        [Display(Name = "Customer")]
        public int CustomerID { get; set; }

        [Display(Name = "Jobs Currently on Invoice")]
        public virtual ICollection<int> ListOfJobsOnInvoice { get; set; } = new List<int>();

        [Display(Name = "Add/Remove as necessary to override jobs on this invoice.")]
        public virtual ICollection<int> ListOfJobsAvailable { get; set; } = new List<int>();

        [Display(Name ="Current invoice total.  Click save to see new total.")]
        [DataType(DataType.Currency)]
        public double InvoiceAmount { get; set; }

        [Display(Name ="Additional charges/discounts(use negative sign)")]
        [DataType(DataType.Currency)]
        public double Adjustments { get; set; }
        public string AdjustmentNotes { get; set; }
        public bool Paid { get; set; }
    }
}
