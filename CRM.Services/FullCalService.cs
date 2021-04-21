using CRM.Data;
using CRM.Models.CalendarEvent;
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
                     end = (DateTimeOffset)e.End,
                     title = e.Title

                 });
                return query.ToList();
            }
        }

        public FullCalEvent GetByID(int id)
        {
            return GetFullCalEvents().Find(d => d.id == id);
        }

        public void UpdateEvent(string id, DateTimeOffset start, DateTimeOffset end)
        {
            var fullCalEvent = GetByID(Convert.ToInt32(id));
            var calEventEdit = new CalendarEventEdit();
            calEventEdit.CalEventID = fullCalEvent.id;
            calEventEdit.Start = start;
            calEventEdit.End = end;
            calEventEdit.Title = fullCalEvent.title;

            _calEventSvc.UpdateCalendarEvent(calEventEdit);

        }
    }
}
