﻿using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CRM.Data.Person;

namespace CRM.Models
{
   public class CustomerEdit
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string StreetAddress { get; set; }
        public string City { get; set; }

        public PersonState StateOfPerson { get; set; }
        public DateTimeOffset InitialDateOfContact { get; set; }
        public CustomerStatus StatusOfCustomer { get; set; }
    }
}
