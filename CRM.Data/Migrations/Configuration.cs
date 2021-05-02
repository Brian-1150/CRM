namespace CRM.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CRM.Data.ApplicationDbContext>
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CRM.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
           
            context.Employees.AddOrUpdate(x => x.LastName,
                new Employee()
                {
                    FirstName = "Employee",
                    LastName = "One",
                    Email = "employee@yourBusiness.com",
                    StreetAddress = "1234 Work Blvd",
                    StateOfPerson = PersonState.IN,
                    ZipCode = 46034,
                    PhoneNumber = "3171234567",
                    Current = true,
                    HireDate = new DateTime(2020, 12, 20)
                },
                new Employee()
                {
                    FirstName = "Employee",
                    LastName = "Two",
                    Email = "employee2@yourBusiness.com",
                    StreetAddress = "4321 Work Blvd",
                    StateOfPerson = PersonState.IN,
                    ZipCode = 46034,
                    PhoneNumber = "3173458888",
                    Current = true,
                    HireDate = new DateTime(2019, 12, 19)
                },
                new Employee()
                {
                    FirstName = "Employee",
                    LastName = "Three",
                    Email = "employee3@yourBusiness.com",
                    StreetAddress = "9876 Street Rd",
                    StateOfPerson = PersonState.IN,
                    ZipCode = 46250,
                    PhoneNumber = "7659957878",
                    Current = true,
                    HireDate = new DateTime(2020, 12, 20)
                },
                new Employee()
                {
                    FirstName = "Fourth",
                    LastName = "Employee",
                    Email = "employee4@yourBusiness.com",
                    StreetAddress = "555 Jackson Street",
                    StateOfPerson = PersonState.IN,
                    ZipCode = 46060,
                    PhoneNumber = "3171234567",
                    Current = true,
                    HireDate = new DateTime(2015, 12, 20)
                });
            context.Customers.AddOrUpdate(x => x.LastName,
                new Customer()
                {
                    FirstName = "Abe",
                    LastName = "Lincoln",
                    Email = "abe@customers.com",
                    StreetAddress = "123 Jackson Street",
                    StateOfPerson = PersonState.IN,
                    ZipCode = 46060,
                    PhoneNumber = "3191234567",
                    InitialDateOfContact = new DateTime(2015, 12, 20),
                    StatusOfCustomer = CustomerStatus.Active
                },
            new Customer()
            {
                FirstName = "Bill",
                LastName = "Clinton",
                Email = "bill@customers.com",
                StreetAddress = "999 Clinton Street",
                StateOfPerson = PersonState.IN,
                ZipCode = 46060,
                PhoneNumber = "2191234565",
                InitialDateOfContact = new DateTime(2016, 12, 20),
                StatusOfCustomer = CustomerStatus.Active
            },
            new Customer()
            {
                FirstName = "George",
                LastName = "Bush",
                Email = "george@customers.com",
                StreetAddress = "545 Washington Street",
                StateOfPerson = PersonState.IN,
                ZipCode = 46060,
                PhoneNumber = "2224564599",
                InitialDateOfContact = new DateTime(2018, 12, 20),
                StatusOfCustomer = CustomerStatus.Inactive
            },
            new Customer()
            {
                FirstName = "Mother",
                LastName = "Teresa",
                Email = "mother@customers.com",
                StreetAddress = "2133 Jackson Street",
                StateOfPerson = PersonState.IN,
                ZipCode = 46060,
                PhoneNumber = "7774561234",
                InitialDateOfContact = new DateTime(2020, 12, 20),
                StatusOfCustomer = CustomerStatus.Active
            }, new Customer()
            {
                FirstName = "Mahatma",
                LastName = "Gandhi",
                Email = "mgandhi@customers.com",
                StreetAddress = "311 Fast Street",
                StateOfPerson = PersonState.IN,
                ZipCode = 46060,
                PhoneNumber = "1181239567",
                InitialDateOfContact = new DateTime(2021, 3, 20),
                StatusOfCustomer = CustomerStatus.Prospect
            },

                new Customer()
                {
                    FirstName = "Nelson",
                    LastName = "Mandela",
                    Email = "nelson@customers.com",
                    StreetAddress = "4567 S Africa Street",
                    StateOfPerson = PersonState.IN,
                    ZipCode = 46060,
                    PhoneNumber = "3199876567",
                    InitialDateOfContact = new DateTime(2020, 12, 29),
                    StatusOfCustomer = CustomerStatus.Active
                }, new Customer()
                {
                    FirstName = "Rosa",
                    LastName = "Parks",
                    Email = "rosa@customers.com",
                    StreetAddress = "3232 Buster Blvd",
                    StateOfPerson = PersonState.IN,
                    ZipCode = 46060,
                    PhoneNumber = "7178985555",
                    InitialDateOfContact = new DateTime(2019, 2, 20),
                    StatusOfCustomer = CustomerStatus.Active
                }, new Customer()
                {
                    FirstName = "Oprah",
                    LastName = "Winfrey",
                    Email = "oprah@customers.com",
                    StreetAddress = "920 Winding Lane",
                    StateOfPerson = PersonState.IN,
                    ZipCode = 46250,
                    PhoneNumber = "2224561234",
                    InitialDateOfContact = new DateTime(2015, 12, 20),
                    StatusOfCustomer = CustomerStatus.Active
                },

                new Customer()
                {
                    FirstName = "Deepak",
                    LastName = "Chopra",
                    Email = "deepak@customers.com",
                    StreetAddress = "123 Zen Street",
                    StateOfPerson = PersonState.IN,
                    ZipCode = 46060,
                    PhoneNumber = "9876543217",
                    InitialDateOfContact = new DateTime(2015, 12, 20),
                    StatusOfCustomer = CustomerStatus.Active
                });

            context.CalendarEvents.AddOrUpdate(x => x.CalEventID,
                new CalendarEvent()
                {
                    CalEventID = 1001,
                    Start = new DateTime(2021, 4, 20),
                    End = new DateTime(2021, 4, 21),
                    Location = "123 Zen Street",
                    Title = "HouseCleaning",
                    ColorOfEvent = Color.Blue,
                });
        }

        
    }
}