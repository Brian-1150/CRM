using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.CalendarEvent
{
    public class CalendarEventDetail
    {
        [Display(Name = "ID")]
        public int CalEventID { get; set; }
        public string Location { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public string Title { get; set; }

        [Display(Name = "Color")]
        public Color ColorOfEvent { get; set; }
        public string Details { get; set; }
        public EventType TypeOfEvent { get; set; }
    }
}
