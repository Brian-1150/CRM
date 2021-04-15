using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data.Deleted
{
   public  class JobDeleted
    {
       
        [Key]
        public int JobID { get; set; }

        public int CalendarEventID { get; set; }
        
        public int CustomerID { get; set; }
       
        public int EmployeeID { get; set; }
       
        public double EmployeePay { get; set; }
        public double CustomerCharge { get; set; }


    }
}
