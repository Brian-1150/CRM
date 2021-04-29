using CRM.Data;
using CRM.Models;
using CRM.Models.CalendarEvent;
using CRM.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class CalendarEventService
    {
        public CalendarEventService() { }

        public CalendarEventCreate CalendarEventCreateView()
        {

            return new CalendarEventCreate

            //ticket #22
            {//remove unnecessary call to method to return view.  Method has been changed
            };
        }
        public bool CreateCalendarEvent(CalendarEventCreate model)
        {

            DateTimeOffset? endDefault;
            if (model.End != null)
                endDefault = model.End;
            else endDefault = model.Start.AddDays(1);
            var entity = new CalendarEvent()
            {

                Location = model.Location,
                Start = model.Start,
                End = (DateTimeOffset)endDefault,
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
            var tempList = new List<CalendarEventListItem>();
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
                            CalendarEventID = e.CalEventID,
                            Location = e.Location,
                            Start = e.Start,
                            End = e.End,
                            ColorOfEvent = e.ColorOfEvent
                        });
                return query.ToArray();
            }
        }



        public IEnumerable<CalendarEventListItem> GetCalendarEvents(int id)
        {
            var tempList = new List<CalendarEventListItem>();
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .CalendarEvents
                    .Where(e => e.Job.CalendarEventID >= id)
                    .Select(
                        e =>
                        new CalendarEventListItem
                        {
                            CalendarEventID = e.CalEventID,
                            Location = e.Location,
                            Start = e.Start,
                            End = e.End,
                            ColorOfEvent = e.ColorOfEvent
                        });
                return query.ToArray();
            }
        }
        public bool UpdateCalendarEvent(CalendarEventEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .CalendarEvents.Find(model.CalEventID);
                entity.Start = model.Start;
                entity.End = (DateTimeOffset)model.End;
                entity.Details = model.Details;
                entity.Title = model.Title;
                entity.Location = model.Location;
                entity.ColorOfEvent = model.ColorOfEvent;

                return ctx.SaveChanges() == 1;
            }
        }

        public CalendarEventDetail GetEventById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .CalendarEvents
                    .Single(e => e.CalEventID == id);
                return
                    new CalendarEventDetail
                    {
                        CalEventID = entity.CalEventID,
                        Start = entity.Start,
                        End = entity.End,
                        Details = entity.Details,
                        Location = entity.Location,
                        Title = entity.Title,
                        ColorOfEvent = entity.ColorOfEvent
                    };
            }
        }

        //Helper Methods

        internal List<CalendarEventListItem> GetAvailableCalEvents()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .CalendarEvents
                    .Where(e => e.Job == null)
                    .Select(
                        e =>
                        new CalendarEventListItem
                        {
                            CalendarEventID = e.CalEventID,
                            Location = e.Location,
                            Start = e.Start,
                            End = e.End,
                            ColorOfEvent = e.ColorOfEvent
                        });
                return query.ToList();
            }
        }

        internal int GetLastCalEventID()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CalendarEvents.OrderByDescending(c => c.CalEventID).FirstOrDefault();
                return entity.CalEventID;

                //return ctx.CalendarEvents.Last().CalEventID;
            }
        }
    }
}



