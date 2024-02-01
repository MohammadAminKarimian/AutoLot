using Microsoft.Build.Framework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(config =>
{
    //Adding Exception Filter at application level.
    config.Filters.Add(new CustomExceptionFilterAttribute(builder.Environment));
})
    .AddJsonOptions(option =>
    {
        option.JsonSerializerOptions.PropertyNamingPolicy = null;
        option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        option.JsonSerializerOptions.WriteIndented = true;
        option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        //suppress automatic model state binding errors (http 400)
        options.SuppressModelStateInvalidFilter = true;
        //suppress all binding inference
        //options.SuppressInferBindingSourcesForParameters= true;
        //suppress multipart/form-data content type inference
        //options.SuppressConsumesConstraintForFormFileParameters = true;
        //Don't create a problem details error object if set to true
        options.SuppressMapClientErrors = false;
        options.ClientErrorMapping[StatusCodes.Status404NotFound].Link = "https://httpstatuses.com/404";
        options.ClientErrorMapping[StatusCodes.Status404NotFound].Title = "Invalid location";
    });

//Register ApplicationDbContext for injection.
var connectionString = builder.Configuration.GetConnectionString("AutoLot");
builder.Services.AddDbContextPool<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString,
        ssOptions => ssOptions.EnableRetryOnFailure().CommandTimeout(60)));

builder.Services.AddRepositories();
//Registering Serilog
builder.ConfigureSerilog();
builder.Services.RegisterLoggingInterfaces();
//Enable API versioning
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoLotApiVersionConfiguration(new ApiVersion(1, 0));

builder.Services.AddAndConfigureSwagger(
    builder.Configuration,
    Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"),
    true);

//Injects IOptionMonitor instance to BasicAuthenticationHandler to get security settings from json.
builder.Services.Configure<SecuritySettings>(builder.Configuration.GetSection(nameof(SecuritySettings)));
//Setting default schema for authorization via DI services.
builder.Services.AddAuthentication("BasicAuthentication")
.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication",
null);

//In a Real Application it's not appropriate to allow all.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

//Register LoggingResponseHeaderFilter in Container.
//builder.Services.AddScoped(typeof(ILogger<>));
builder.Services.AddScoped<LoggingResponseHeaderFilter>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Initialize the database
    if (app.Configuration.GetValue<bool>("RebuildDatabase"))
    {
        using var scope = app.Services.CreateScope();
        //Get context from ServiceProvider without injection.
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        SampleDataInitializer.ClearAndReseedDatabase(dbContext);
    }
}

app.UseSwagger();
app.UseSwaggerUI(
    options =>
    {
        using var scope = app.Services.CreateScope();
        var versionProvider = scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();
        // build a swagger endpoint for each discovered API version
        foreach (var description in versionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });

app.UseHttpsRedirection();

app.UseCors("AllowAll");

//enable authorization checks
app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    Console.WriteLine("Hello from custom middleware before endRoute.");
    await next.Invoke();
    Console.WriteLine("Hello again but after endRoute.");
});

app.MapControllers().RequireAuthorization();

app.Run();
