using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.CalendarEvent
{
    public class CalendarEventListItem
    {

        public int CalEventID { get; set; }
        public int JobID { get; set; }
        public string Location { get; set; }

        public DateTimeOffset Start { get; set; }
        public DateTimeOffset? End { get; set; }

        public EventColor ColorOfEvent { get; set; }


    }
}
