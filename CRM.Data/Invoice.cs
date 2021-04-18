using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
    public class Invoice
    {

        // One or many Jobs
        // Only one customer
        // Zero Employee
        [Key]
        public int InvoiceID { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
        public double  InvoiceAmount { get; set; }
        public string AdjustmentNotes { get; set; }
        public bool Paid { get; set; }

    }
}
