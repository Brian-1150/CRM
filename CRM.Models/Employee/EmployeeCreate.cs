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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [Display(Name = "Hire Date")]
        public DateTimeOffset HireDate { get; set; } = DateTimeOffset.Now;
        public new PersonState StateOfPerson { get; set; } = PersonState.IN;

        [RegularExpression(@"^(?!00000)[0-9]{5,5}$", ErrorMessage = "Please enter a valid 5 digit U.S. zip code")]

        [Display(Name = "Zip")]
        public int ZipCode { get; set; }
    }
}
