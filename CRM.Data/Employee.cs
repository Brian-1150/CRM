using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
    public class Employee : Person
    {
        [Key]
        public int EmployeeID { get; set; }
        public bool Current { get; set; }
        public DateTimeOffset HireDate { get; set; }
    }
}
