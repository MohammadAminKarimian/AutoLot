rem add packages for AutoLot.Services
dotnet add AutoLot.Services package Microsoft.Extensions.Hosting.Abstractions -v 6.0.0
dotnet add AutoLot.Services package Microsoft.VisualStudio.Threading.Analyzers -v 17.0.64
dotnet add AutoLot.Services package Microsoft.Extensions.Options -v 6.0.0
dotnet add AutoLot.Services package Serilog.AspNetCore -v 4.1.0
dotnet add AutoLot.Services package Serilog.Enrichers.Environment -v 2.2.0
dotnet add AutoLot.Services package Serilog.Settings.Configuration -v 3.3.0
dotnet add AutoLot.Services package Serilog.Sinks.Console -v 4.0.1
dotnet add AutoLot.Services package Serilog.Sinks.File -v 5.0.0
dotnet add AutoLot.Services package Serilog.Sinks.MSSqlServer -v 5.6.1
dotnet add AutoLot.Services package System.Text.Json -v 6.0.0

rem add packages for AutoLot.Api
dotnet add AutoLot.Api package AutoMapper -v 10.1.1
dotnet add AutoLot.Api package Microsoft.VisualStudio.Threading.Analyzers -v 17.0.64
rem dotnet add AutoLot.Api package Swashbuckle.AspNetCore 
dotnet add AutoLot.Api package Swashbuckle.AspNetCore.Annotations
dotnet add AutoLot.Api package Swashbuckle.AspNetCore.Swagger
dotnet add AutoLot.Api package Swashbuckle.AspNetCore.SwaggerGen
dotnet add AutoLot.Api package Swashbuckle.AspNetCore.SwaggerUI
dotnet add AutoLot.Api package Microsoft.VisualStudio.Web.CodeGeneration.Design -v 6.0.0
dotnet add AutoLot.Api package Microsoft.EntityFrameworkCore.SqlServer -v 6.0.0
dotnet add AutoLot.Api package Microsoft.EntityFrameworkCore.Design -v 6.0.0
dotnet add AutoLot.Api package System.Text.Json -v 6.0.0
rem dotnet add AutoLot.Api package Microsoft.AspNetCore.Mvc.Versioning -v 5.0.0
rem dotnet add AutoLot.Api package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer -v 5.0.0

rem add packages for AutoLot.Mvc
dotnet add AutoLot.Mvc package AutoMapper -v 10.1.1
dotnet add AutoLot.Mvc package Microsoft.VisualStudio.Threading.Analyzers -v 17.0.64
dotnet add AutoLot.Mvc package System.Text.Json -v 6.0.0
dotnet add AutoLot.Mvc package LigerShark.WebOptimizer.Core -v 3.0.330
dotnet add AutoLot.Mvc package Microsoft.Web.LibraryManager.Build -v 2.1.113
dotnet add AutoLot.Mvc package Microsoft.EntityFrameworkCore.SqlServer -v 6.0.0
dotnet add AutoLot.Mvc package Microsoft.EntityFrameworkCore.Design -v 6.0.0
dotnet add AutoLot.Mvc package Microsoft.VisualStudio.Web.CodeGeneration.Design -v 6.0.0

rem add packages for AutoLot.Web
dotnet add AutoLot.Web package AutoMapper -v 10.1.1
dotnet add AutoLot.Web package Microsoft.VisualStudio.Threading.Analyzers -v 17.0.64
dotnet add AutoLot.Web package System.Text.Json -v 6.0.0
dotnet add AutoLot.Web package LigerShark.WebOptimizer.Core -v 3.0.330
dotnet add AutoLot.Web package Microsoft.Web.LibraryManager.Build -v 2.1.113
dotnet add AutoLot.Web package Microsoft.EntityFrameworkCore.SqlServer -v 6.0.0 
dotnet add AutoLot.Web package Microsoft.EntityFrameworkCore.Design -v 6.0.0 
dotnet add AutoLot.Web package Microsoft.VisualStudio.Web.CodeGeneration.Design -v 6.0.0

pause