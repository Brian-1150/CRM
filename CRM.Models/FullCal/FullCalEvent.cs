using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models.FullCal
{
  public class FullCalEvent
    {
        public int id { get; set; }
        public bool allDay { get; set; }
        public DateTimeOffset start { get; set; }
        public DateTimeOffset end { get; set; }
        public string title { get; set; }
        public string color { get; set; }
        public string textColor { get; set; }
        public string details { get; set; }
        public string url { get; set; }
        public string employeeName { get; set; }
      

    }
}
