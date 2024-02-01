var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("AutoLot");
builder.Services.AddDbContextPool<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString,
        ssOptions => ssOptions.EnableRetryOnFailure().CommandTimeout(60)));

builder.Services.AddRepositories();

builder.Services.AddDataServices(builder.Configuration);
// Using options pattern : page 1377
builder.Services.Configure<DealerInfo>(builder.Configuration.GetSection(nameof(DealerInfo)));
// Using options pattern wrapped in extenstion method.
builder.Services.ConfigureApiServiceWrapper(builder.Configuration);
//Registering Serilog
builder.ConfigureSerilog();
builder.Services.RegisterLoggingInterfaces();

//Enable CSS isolation in a non-deployed session
if (!builder.Environment.IsDevelopment())
{
    builder.WebHost.UseWebRoot("wwwroot");
    builder.WebHost.UseStaticWebAssets();
}

if (builder.Environment.IsDevelopment())
{
    //builder.Services.AddWebOptimizer(false,false);
    builder.Services.AddWebOptimizer(options =>
    {
        options.MinifyCssFiles("AutoLot.Mvc.styles.css");
        options.MinifyCssFiles("/css/site.css");
        options.MinifyJsFiles("/js/site.js");

        //options.AddJavaScriptBundle("/js/validationCode.js", "/js/validations/**/*.js");
        options.AddJavaScriptBundle("/js/validationCode.js", "js/validations/validators.js", "js/validations/errorFormatting.js");
    });
}
else
{
    builder.Services.AddWebOptimizer(options =>
    {
        //options.MinifyCssFiles(); //Minifies all CSS files
        //options.MinifyJsFiles(); //Minifies all JS files
        //options.MinifyCssFiles("css/site.cs"); //Minifies the site.css file
        //options.MinifyCssFiles("lib/**/*.cs"); //Minifies all CSS files
        //options.MinifyJsFiles("js/site.js"); //Minifies the site.js file
        //options.MinifyJsFiles("lib/**/*.js"); //Minifies all JavaScript file under the wwwroot/lib directory

        //options.MinifyJsFiles("js/site.js");
        //options.MinifyJsFiles("lib/**/*.js");

        options.MinifyCssFiles("AutoLot.Mvc.styles.css");
        options.MinifyCssFiles("cs/site.cs");
        options.MinifyJsFiles("js/site.js");

        //options.AddJavaScriptBundle("js/validations/validationCode.js", "js/validations/**/*.js");
        //options.AddJavaScriptBundle("js/validations/validationCode.js", "js/validations/validators.js", "js/validations/errorFormatting.js");
    });
}

// To create an instance of urlFactory for creating custom tag helpers.
builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

// Add cookie policy support.
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is
    // needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

// The TempData provider cookie is not essential. Make it essential
// so TempData is functional when tracking is disabled.
builder.Services.Configure<CookieTempDataProviderOptions>(options => { options.Cookie.IsEssential = true; });
builder.Services.AddSession(options => { options.Cookie.IsEssential = true; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    if (app.Configuration.GetValue<bool>("RebuildDatabase"))
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        SampleDataInitializer.ClearAndReseedDatabase(dbContext);
    }
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseWebOptimizer();

app.UseStaticFiles();

// Add cookie policy support to http pipeline
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

// For Attribute routing
app.MapControllers();

// Not needed when we are using attribute routing.
//app.MapAreaControllerRoute(
//name: "AdminArea",
//areaName: "Admin",
//pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

// Default route
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
