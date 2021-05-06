using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
    public enum PersonState
    {

        AK, AL, AR, AS, AZ, CA, CO, CT, DC, DE, FL, GA, GU, HI, IA, ID, IL, IN, KS, KY, LA, MA, MD, ME, MI, MN, MO, MP, MS, MT, NC, ND, NE, NH, NJ, NM, NV, NY, OH, OK, OR, PA, PR, RI, SC, SD, TN, TX, UM, UT, VA, VI, VT, WA, WI, WV, WY
    }
    abstract public class Person
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "A First Name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number. (10 digits no dashes) ex(317123456) ")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public PersonState StateOfPerson { get; set; }

        [DataType(DataType.PostalCode)]
        public int ZipCode { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }


        }

        public string FullAddress
        {
            get
            {
                return StreetAddress + " " + City + " " + StateOfPerson + " " + ZipCode;
            }

        }
    }
}
