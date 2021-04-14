using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.Job
{
   public class JobCreate
    {
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public double CustomerCharge { get; set; }
        public double EmployeePay { get; set; }

    }
}
