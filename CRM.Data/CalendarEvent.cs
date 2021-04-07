using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data
{
  public  class CalendarEvent
    {
        [Key]
        public long CalEventID { get; set; }

    }
}
