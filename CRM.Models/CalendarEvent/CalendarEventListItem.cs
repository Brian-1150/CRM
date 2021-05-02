using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.CalendarEvent
{
    public class CalendarEventListItem
    {
        [Display(Name ="ID")]
        public int CalendarEventID { get; set; }
        public int JobID { get; set; }
        public string Location { get; set; }

        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }

        public Color ColorOfEvent { get; set; }
        public EventType TypeOfEvent { get; set; }


    }
}
