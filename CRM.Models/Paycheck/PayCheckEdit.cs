using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Paycheck
{
    public class PayCheckEdit
    {
        public int PayCheckID { get; set; }
        public int EmployeeID { get; set; }

        [Display(Name = "Jobs Currenlty on PayCheck")]
        public virtual ICollection<int> ListOfJobsOnPayCheck { get; set; }

        [Display(Name = "Add/Remove as necessary to override jobs on this paycheck.")]
        public virtual ICollection<int> ListOfJobsAvailable { get; set; }

        [Display(Name = "Current paycheck total.  Click save to see new total."), DataType(DataType.Currency)]
        public double PayCheckAmount { get; set; }

        [Display(Name = "Additional pay/bonuses (use negative sign if docking pay)"), DataType(DataType.Currency)]
        public double Adjustments { get; set; }

        [Display(Name ="Please document the reason for adjustments")]
        public string AdjustmentNotes { get; set; }

        public bool Paid { get; set; }
    }
}
