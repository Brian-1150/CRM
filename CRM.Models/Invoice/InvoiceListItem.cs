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

        public int InvoiceID { get; set; }

        
        public int CustomerID { get; set; }
        
        public  List<int> JobIDs { get; set; }
        public double InvoiceAmount { get; set; }

        [UIHint("Bool")]
        public bool Paid { get; set; }
    }
}
