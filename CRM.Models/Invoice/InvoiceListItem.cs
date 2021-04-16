using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Invoice
{
   public class InvoiceListItem
    {

        public int InvoiceID { get; set; }

        
        public int CustomerID { get; set; }
        
        public  ICollection<int> JobIDs { get; set; }
        public double InvoiceAmount { get; set; }

        public bool Paid { get; set; }
    }
}
