using CRM.Data;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class CustomerService
    {
        private readonly Guid _userId;

        public CustomerService(Guid userId)
        {
            _userId = userId;

        }

        public bool CreateCustomer(CustomerCreate model)
        {
            var entity = new Customer()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                StreetAddress = model.StreetAddress,
                City = model.City,
                StateOfPerson = model.StateOfPerson,
                InitialDateOfService = DateTimeOffset.Now,
                StatusOfCustomer = CustomerStatus.Prospect  //new Customer gets set to prospect by default
                

            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Customers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CustomerListItem> GetCustomers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                       .Customers
                       .Where(e => e.CustomerID >= 0)
                       .Select(
                          e =>
                              new CustomerListItem
                              {
                                  CustomerID = e.CustomerID,
                                  FirstName = e.FirstName,
                                  LastName = e.LastName,
                                  PhoneNumber = e.PhoneNumber,
                                  Email = e.Email,
                                  City = e.City,
                                  InitialDateOfService = e.InitialDateOfService

                              }
                   );
                return query.ToArray();
            }
        }
        public CustomerDetail GetCustomerByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Customers
                    .Single(e => e.CustomerID == id);
                return
                    new CustomerDetail
                    {
                        CustomerID = entity.CustomerID,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        PhoneNumber = entity.PhoneNumber,
                        Email = entity.Email,
                        City = entity.City,
                        InitialDateOfService = entity.InitialDateOfService
                    };
            }
        }
    }
}
