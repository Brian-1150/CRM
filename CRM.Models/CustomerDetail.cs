using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class CustomerDetail : Person
    {
        public int CustomerID { get; set; }
        
        public DateTimeOffset InitialDateOfService { get; set; }
    }
}
