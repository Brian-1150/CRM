using CRM.Data;
using CRM.Models.CalendarEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class CalendarEventService
    {
        private readonly Guid _userId;

        public CalendarEventService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCalendarEvent(CalendarEventCreate model)
        {
            DateTimeOffset? endDefault;
            if (model.End != null)
                endDefault = model.End;
            else endDefault = model.Start.AddDays(1);
            var entity = new CalendarEvent()
            {
                CustomerID = model.CustomerID,
                
                EmployeeID = model.EmployeeID,
                Start = model.Start,
                End = endDefault,
                Title = model.Title,
                ColorOfEvent = model.ColorOfEvent,
                Details = model.Details

            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.CalendarEvents.Add(entity);
                return ctx.SaveChanges() == 1;
            }
            
        }

        public IEnumerable<CalendarEventListItem> GetCalendarEvents()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .CalendarEvents
                    .Where(e => e.CalEventID >= 0)
                    .Select(
                        e =>
                        new CalendarEventListItem
                        {
                            CalEventID = e.CalEventID,
                            CustomerID = e.CustomerID,
                            Location = e.Location,
                            EmployeeID = e.EmployeeID,
                            Start = e.Start,
                            End = e.End,
                            ColorOfEvent = e.ColorOfEvent
                        });
                return query.ToArray();
            }
        }
    }


}
