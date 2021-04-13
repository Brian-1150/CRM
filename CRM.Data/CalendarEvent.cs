using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{

    public enum EventColor
    {
        Red, Orange, Yellow, Green, Blue, Violet
    }
  public  class CalendarEvent
    {
        [Key]
        public int CalEventID { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public string Location { get; set; }

        //private string _location;
        //public string Location
        //{
        //    get { return _location; }
        //    set
        //    {
        //        if (Customer.StreetAddress != null)
        //            _location = Customer.StreetAddress;
        //    }
        //}


        [ForeignKey(nameof(Employee))]
        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
        
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset? End { get; set; }
        public string Title { get; set; }
        public EventColor ColorOfEvent { get; set; }
        public string Details { get; set; }



    }
}
