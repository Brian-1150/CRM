using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Employee
{
    public class EmployeeEdit : Person
    {
        public int EmployeeID { get; set; }
       
        public DateTimeOffset HireDate { get; set; }
        public bool Current { get; set; }
    }
}
