using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Employee
{
  public  class EmployeeDetail : Person
    {
        public int EmployeeID { get; set; }
        public bool Current { get; set; }
        public DateTimeOffset HireDate { get; set; }
    }
}
