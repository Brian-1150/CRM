using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Invoice
{
   public class InvoiceListItem
    {

        [Display(Name = "Invoice")]
        public int InvoiceID { get; set; }

        [Display(Name = "Customer")]
        public int CustomerID { get; set; }

        [Display(Name = "Jobs")]
        public  List<int> JobIDs { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Invoice Amount")]
        public double InvoiceAmount { get; set; }

        [UIHint("Bool")]
        public bool Paid { get; set; }

        [DataType(DataType.MultilineText)]
        public string AdjustmentNotes { get; set; }
    }
}
