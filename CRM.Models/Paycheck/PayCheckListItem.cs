using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Paycheck
{
     public class PayCheckListItem
    {

        [Display(Name = "PayCheck ID")]
        public int PayCheckID { get; set; }

        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }

        public virtual ICollection<int> JobIDs  { get; set; }

        [Display(Name ="Paycheck Amount"), DataType(DataType.Currency)]
        public double PayCheckAmount { get; set; }

        public string AdjustmentNotes { get; set; }

        [UIHint("Bool")]
        public bool Paid { get; set; }
    }
}
