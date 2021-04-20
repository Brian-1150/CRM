using CRM.Data;
using CRM.Models.FullCal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
   public class FullCalService
    {
        private CalendarEventService _calEventSvc = new CalendarEventService();


        public List<FullCalEvent> GetFullCalEvents()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.CalendarEvents.Where(e => e.CalEventID >= 0).Select(e =>
                 new FullCalEvent
                 {
                     id = e.CalEventID,
                     allDay = true,
                     start = e.Start,
                     end = e.Start,
                     title = e.Title

                 });
                return query.ToList();
            }
        }
    }
}
