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
        private CustomerService _custSvc = new CustomerService();
        public CalendarEventService() { }

        public CalendarEventCreate CalendarEventCreateView(DateTime start)
        {
            return new CalendarEventCreate

            {
                Start = start
            };
        }
        public CalendarEventCreate CalendarEventCreateView()
        {
            return new CalendarEventCreate();
        }

        public CalendarEventCreate FullCalendarEventCreateView(DateTime start)
        {
            var newEvent = new CalendarEventCreate
            {
                Start = start,

            };
            return newEvent;
        }

        public bool CreateCalendarEvent(CalendarEventCreate model)
        {
            //set color for estimate/communication.  Will get overridden later if event is type Job
            var color = model.TypeOfEvent == EventType.Communication ? Color.Fuchsia : Color.GreenYellow;

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
                TypeOfEvent = model.TypeOfEvent,
                ColorOfEvent = color,
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
                            ColorOfEvent = e.ColorOfEvent,
                            TypeOfEvent = e.TypeOfEvent
                        });
                return query.ToArray();
            }
        }

        public IEnumerable<CalendarEventListItem> GetCalendarEventsByCustomerID(int customerId)
        {
            var tempList = new List<CalendarEventListItem>();
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .CalendarEvents
                    .Where(e => e.Job.Customer.CustomerID == customerId)
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
        public IEnumerable<CalendarEventListItem> GetCalendarEventsByEmpID(int empId)
        {
            var tempList = new List<CalendarEventListItem>();
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .CalendarEvents
                    .Where(e => e.Job.Employee.EmployeeID == empId)
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
                if (model.Location != null)
                { entity.Location = model.Location; }
                entity.Start = model.Start;
                entity.End = (DateTimeOffset)model.End;
                entity.Details = model.Details;
                entity.Title = model.Title;


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
                        ColorOfEvent = entity.ColorOfEvent,
                        TypeOfEvent = entity.TypeOfEvent
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

            }
        }

        public void DeleteEvent(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CalendarEvents.Find(id);
                ctx.CalendarEvents.Remove(entity);
                ctx.SaveChanges();
            }
        }

        internal void AssignColorToCalEvent(int employeeID, int calEventID)
        {

            using (var ctx = new ApplicationDbContext())
            {
                ctx.CalendarEvents.Find(calEventID).ColorOfEvent = ctx.Employees.Find(employeeID).ColorOfEmployee;
                ctx.SaveChanges();
            }
        }

        internal void SetCalEventLocationAndTitle(int customerID, int calEventID)
        {
            var cust = _custSvc.GetCustomerDetailByID(customerID);
            var location = cust.FullAddress;
            var title = cust.LastName;
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CalendarEvents.Find(calEventID);
                entity.Location = location;
                entity.Title = title;
                ctx.SaveChanges();
            }
        }

    }
}



