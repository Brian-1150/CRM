# Small Business CRM and Scheduler Web App

> This is a web app designed to help small service based businesses stay organized, connect with customers, and track pay, revenue, and expenses.


## Table of contents
* [General info](#general-info)
* [Launch the App](#status)
* [Technologies](#technologies)
* [Setup](#setup)
* [Highlights](#highlights)
* [Sample Views](#sample-views)
* [Resources](#resources)
* [Contact](#contact)

## General info
This app has been designed for a small house cleaning business, although it can work for any small service-based business.  The main function of the app
is to be used as a scheduler.  Events can be added, edited, deleted, etc. just like any typical calendar or scheduler app.  There are three categories for events:
job, estimate, and communication.  Job calendar events are assigned a customer and an employee as well as carrying with it data such as the customer charge and 
employee pay.  The event is assigned the address based on the customer and the other values are added to the database to easily track pay and revenue.  Paychecks and 
invoices can be easily figured based on all of the connecting tables.  


## Technologies used
* ASP.NET WEB MVC, C#, jQuery, HTML, CSS, JavaScript, RazorPages
* Microsoft Entity Framework, Asp.Net Identity.Core, XML, Json, Linq
* Third-party implementaion:  FullCalendar

## Setup
The project can be cloned from GitHub and opened with Visual Studio 2019.  Restore NuGet Packages.  Enable Migrations and update database to load the seed data.  The 
seed data includes a sample of fictional customers, employees, events, paychecks and invoices.  Login info for one admin and one employee are included in the startup.cs.  You
may want to update the seed events in the configuration.cs to the current month for ease of seeing and testing the data.  

## Sample Views
<img src="https://github.com/Brian-1150/CRM/blob/master/Img/landing.png" width="300" height="100">
<img src="https://github.com/Brian-1150/CRM/blob/master/Img/emp.png" width="500" height="300">
<img src="https://github.com/Brian-1150/CRM/blob/master/Img/cal.png" width="800" height="500">

## Highlights

* Calendar view color coordinated based on employee and type of event
* Event dates can be edited direction in calendar view by with drag/drop feature
* Search and sort list view for customers

## Upcoming Features
* Add search and sort to other list views
* Add editing capabilities other than dates from the calendar view
* Add templates for estimates and job details that automatically pass through to recurring jobs
* Add auto generating weekly or monthly invoices and paychecks
* ...

## Status
Project is deployed on Azure [@here](https://crmlasttry.azurewebsites.net)<br />
*Disclaimer* This project is still in the test phase and is not yet available for adding real data.  You may login:<br />
username:  BusinessOwner<br />
password:  Test1!<br />
But please do not add any sensitive data or any real customer info.  Feel free to add, edit, and delete fictional data for testing purposes.  

## Resources
In general, this project relied on StackOverflow, Microsoft DOCS, and Google searches for quick references.
Below is a list of specific articles and blogs that were particularly useful:
* [@A.J. Saulsberry](https://www.pluralsight.com/guides/asp.net-mvc-getting-default-data-binding-right-for-hierarchical-views)
* [@Marghoob Suleman](http://www.marghoobsuleman.com/jquery-image-dropdown)
* [@ Vithal Wadje](https://www.c-sharpcorner.com/article/managing-multiple-submit-buttons-on-single-view-in-asp-net-mvc-5/)
* [@Krishna Jariwala](https://www.toshalinfotech.com/Blogs/ID/115/How-to-Integrate-Full-calendar-with-MVC-application)
* [@ Mudassar Khan](https://www.aspsnippets.com/Articles/Populate-one-DropDownList-based-on-another-DropDownList-selected-value-in-ASPNet-MVC.aspx)

## Contact
Created by [@brianCampassi](https://brian-1150.github.io/) - feel free to contact me!
