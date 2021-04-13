using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
   public class PayCheck
    {
        // Many Jobs
        // Only 1 Employee
        // Zero Customer
        [Key]
        public int PayCheckID { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public HashSet<Job> Jobs { get; set; }
        public double PayCheckAmount { get; set; }

        public bool Paid { get; set; }



    }
}
