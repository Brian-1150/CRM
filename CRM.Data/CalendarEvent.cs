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
        // May have zero or one Job
        // IF it has a job, need to access the customer info from it to set location
        // IF it has a job, need to adjust color based on Employee that is on the job
        [Key]
        public int CalEventID { get; set; }
        public virtual Job Job { get; set; }
        public string Location { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset? End { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public EventColor ColorOfEvent { get; set; }

      



        



    }
}
