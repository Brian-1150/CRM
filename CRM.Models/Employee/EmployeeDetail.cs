using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Employee
{
  public  class EmployeeDetail : Person
    {
        [Display(Name = "ID")]
        public int EmployeeID { get; set; }
        public bool Current { get; set; }

        [Display(Name = "Hire Date")]
        public DateTimeOffset HireDate { get; set; }
    }
}
