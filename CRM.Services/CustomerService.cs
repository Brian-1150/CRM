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
    }
}
