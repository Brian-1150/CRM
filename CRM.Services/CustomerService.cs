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

        public CustomerService() { }
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
                InitialDateOfContact = DateTimeOffset.Now,
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
                                  StreetAddress = e.StreetAddress,
                                  City = e.City,
                                  InitialDateOfContact = e.InitialDateOfContact

                              }
                   );
                return query.ToArray();
            }
        }
        public CustomerDetail GetCustomerDetailByID(int id)
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
                        StreetAddress = entity.StreetAddress,
                        City = entity.City,
                        StateOfPerson = entity.StateOfPerson,
                        StatusOfCustomer = entity.StatusOfCustomer,
                        InitialDateOfContact = entity.InitialDateOfContact,
                        IsOnDoNotContactList = entity.IsOnDoNotContactList


                    };
            }
        }
        public bool UpdateCustomer(CustomerEdit model)
        {


            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Customers
                    .Single(e => e.CustomerID == model.CustomerID);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.PhoneNumber = model.PhoneNumber;
                entity.Email = model.Email;
                entity.StreetAddress = model.StreetAddress;
                entity.City = model.City;
                entity.StateOfPerson = model.StateOfPerson;
                entity.StatusOfCustomer = model.StatusOfCustomer;


                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCustomer(CustomerDelete model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Customers
                    .Single(e => e.CustomerID == model.CustomerID);

                entity.IsOnDoNotContactList = model.IsOnDoNotContactList;
                entity.StatusOfCustomer = model.StatusOfCustomer;

                return ctx.SaveChanges() == 1;
            }
        }

        //Helper Methods

        internal Customer GetCustomerFromDB(int customerID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Customers
                    .Single(e => e.CustomerID == customerID);
                return entity;
            }
        }
        internal List<Customer>GetCustomerFromDB()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Customers
                    .Where(e => e.CustomerID >= 0)
                     .Select(
                        e =>
                        new Customer { });

                return query.ToList();
            }
        }

    }
}
