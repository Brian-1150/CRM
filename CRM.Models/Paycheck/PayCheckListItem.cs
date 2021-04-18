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
        public int PayCheckID { get; set; }

        public int EmployeeID { get; set; }

        public virtual ICollection<int> JobIDs  { get; set; }

        [Display(Name ="Paycheck Amount"), DataType(DataType.Currency)]
        public double PayCheckAmount { get; set; }

        public StringBuilder AdjustmentNotes { get; set; }

        [UIHint("Bool")]
        public bool Paid { get; set; }
    }
}
