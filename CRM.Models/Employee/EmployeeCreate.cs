using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Employee
{
    public class EmployeeCreate : Person
    {
        public DateTimeOffset HireDate { get; set; }
        public new PersonState StateOfPerson { get; set; } = PersonState.IN;
    }
}
