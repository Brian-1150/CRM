using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.CalendarEvent
{
    public class CalendarEventEdit
    {
        public int CalEventID { get; set; }
       
        public string Location { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        public DateTimeOffset Start { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        public DateTimeOffset? End { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public EventColor ColorOfEvent { get; set; }
    }
}
