# AutoLot Learning Project
This is a project I created to learn ASP.NET, RestApi and EF Core(ORM). It contains 2 web application (MVC, Razor) that allows users to create, read, update, and delete data from a database (CRUD) Using a service component. The application is built using the ASP.NET framework, EF Core and C#. Views are implemented by html, css, javascript but they are very basic and i'm working to improve that so that i can also boost my front skills.
Project is mainly based on a book project (Pro C# 10 with .NET 6 book Project by Andrew Troelsen and Phil Japikse)
![AutoLot General](https://github.com/MohammadAminKarimian/AutoLot/assets/42168296/9a27024e-671c-4af1-b8f7-30513a7092a9)
## Components
The AutoLot data access layer consists of two projects, one to hold the EF Core–specific code (the derived
DbContext, context factory, repositories, migrations, etc.) and another one to hold the entities and view
models.
### AutoLot.Models
hold entities and viewModels which get used in dal or other components.
![Models](https://github.com/MohammadAminKarimian/AutoLot/assets/42168296/f43d4e37-c3b4-4376-9a6c-96655739d429)
### AutoLot.Dal
hold all EF Core specific code (the derivedDbContext, context factory, repositories, migrations, dataInits etc.)
  #### Repositories
  provide encapsulated create, read, update, and delete (CRUD) access to the database.
  #### Data Initialization
  add data initialization code to provide sample data which is a common practice for testing data access layer.
  ![DAL](https://github.com/MohammadAminKarimian/AutoLot/assets/42168296/98f89758-cee5-454a-94b7-66eb3a4ac493)
### AutoLot.Services 
#### can work in 2 ways:
##### 1- Directly use DAL(Data access layer) component.
##### 2- Use api service as an Intermediary component between service and DAL component.
![services](https://github.com/MohammadAminKarimian/AutoLot/assets/42168296/3c641085-5498-470a-847b-d075b7369ee3)
### AutoLot.Api
is a ASP.NET Web API which is designed to be a service-based framework for building
REpresentational State Transfer (RESTful) services. It is based on the MVC framework minus the V (view),
with optimizations for creating headless services. These services can be called by any technology, not just
those under the Microsoft umbrella. Calls to a Web API service are based on the core HTTP verbs (Get, Put,
Post, Delete) through a uniform resource identifier (URI)
#### this api receive data from, and send data back to, clients using JSON
### AutoLot Clients
#### AutoLot.Mvc (Model View Controller)
is an ASP.NET Core web application using the Model-View-Controller pattern 
#### AutoLot.Web (Razor pages)
is an ASP.NET Core web application using Razor pages
