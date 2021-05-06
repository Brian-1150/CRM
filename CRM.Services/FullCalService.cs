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

                var query = ctx.CalendarEvents.Include("Job").Include("Employee").Where(e => e.CalEventID >= 0).Select(e =>
                 new FullCalEvent
                 {
                     id = e.CalEventID,
                     allDay = true,
                     start = e.Start,
                     end = (DateTimeOffset)e.End,
                     color = e.ColorOfEvent.ToString(),
                     title = e.Title,
                     details = e.Details,
                     url = e.Location,
                     employeeName = e.Job.Employee.FirstName,
                     textColor = "black"

                 });
                return query.ToList();
            }
        }

        public FullCalEvent GetByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                string emp = "";

                var list = ctx.CalendarEvents.ToList();
                var f = list.Find(e => e.CalEventID == id);
                if (f.TypeOfEvent == EventType.Job)
                    emp = f.Job.Employee.FullName;
                return new FullCalEvent
                {
                    id = f.CalEventID,
                    allDay = true,
                    start = f.Start,
                    end = (DateTimeOffset)f.End,
                    color = f.ColorOfEvent.ToString(),
                    title = f.Title,
                    details = f.Details,
                    url = f.Location,
                    employeeName = emp,
                    textColor = "black"
                };
            }
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
