using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Employee
{
    public class EmployeeCreate : Person
    {
        public DateTimeOffset HireDate { get; set; }
        public new PersonState StateOfPerson { get; set; } = PersonState.IN;

        [RegularExpression(@"^(?!00000)[0-9]{5,5}$", ErrorMessage = "Please enter a valid 5 digit U.S. zip code")]
        public int ZipCode { get; set; }
    }
}
