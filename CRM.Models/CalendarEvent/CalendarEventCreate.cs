using CRM.Data;
using CRM.Models.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.CalendarEvent
{
    public class CalendarEventCreate
    {


        public string Location { get; set; }

        [Required]
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset? End { get; set; }
        public string Title { get; set; }
        public Color ColorOfEvent { get; set; }
        public string Details { get; set; }
        public EventType TypeOfEvent { get; set; }


    }
}
