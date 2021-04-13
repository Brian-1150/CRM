using CRM.Data;
using CRM.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class EmployeeService
    {
        private readonly Guid _userId;

        public EmployeeService() { }
        public EmployeeService(Guid userId)
        {
            _userId = userId;

        }
        public bool CreateEmployee(EmployeeCreate model)
        {
            var entity = new Employee()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                StreetAddress = model.StreetAddress,
                City = model.City,
                StateOfPerson = model.StateOfPerson,
                HireDate = model.HireDate,
                Current = true
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Employees.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }

        public IEnumerable<EmployeeListItem> GetEmployees()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Employees
                    .Where(e => e.EmployeeID >= 0)
                    .Select(
                        e =>
                        new EmployeeListItem
                        {
                            EmployeeID = e.EmployeeID,
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            PhoneNumber = e.PhoneNumber,
                            Current = e.Current

                        }
                        );
                return query.ToArray();
            }
        }
        public EmployeeDetail GetEmployeeByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Employees
                    .Single(e => e.EmployeeID == id);
                return new EmployeeDetail
                {
                    EmployeeID = entity.EmployeeID,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    PhoneNumber = entity.PhoneNumber,
                    Email = entity.Email,
                    StreetAddress = entity.StreetAddress,
                    City = entity.City,
                    StateOfPerson = entity.StateOfPerson,
                    Current = entity.Current,
                    HireDate = entity.HireDate
                    
                };

            }
        }
        public bool UpdateEmployee(EmployeeEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Employees
                    .Single(e => e.EmployeeID == model.EmployeeID);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.PhoneNumber = model.PhoneNumber;
                entity.Email = model.Email;
                entity.StreetAddress = model.StreetAddress;
                entity.City = model.City;
                entity.StateOfPerson = model.StateOfPerson;
                entity.Current = model.Current;
                entity.HireDate = model.HireDate;





                return ctx.SaveChanges() > 0;
            }
        }
        //Delete method needs fixed ticket#18
        public bool DeleteEmployee(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.
                    Employees
                    .Single(e => e.EmployeeID == id);

                entity.Current = false;
                return ctx.SaveChanges() > 0;
            }
        }
    }

}

