﻿using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Employee
{
    public class EmployeeListItem 
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
               
        public string PhoneNumber { get; set; }

        [UIHint("Current")]
        public bool Current { get; set; }


    }
}
