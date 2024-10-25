using Asp.NetCoreMastery.Data;
using Asp.NetCoreMastery.Models;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Reflection;
using FirstDemo;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
.MinimumLevel.Debug()
.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
.Enrich.FromLogContext() //format change kora hocche 
.ReadFrom.Configuration(builder.Configuration)); //

// Add services to the container.
try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        //containerBuilder.RegisterModule(new ApplicationModule());
        //containerBuilder.RegisterModule(new InfrastructureModule(connectionString,
        //    migrationAssembly));
        containerBuilder.RegisterModule(new WebModule());
    });


    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();

    //builder.Services.AddScoped<IEmailSender, HtmlEmailSender>();
    //builder.Services.AddTransient<IEmailSender, HtmlEmailSender>();
    builder.Services.AddSingleton<IEmailSender, HtmlEmailSender>(); // Once create an instance , it will keep running until stop


    var app = builder.Build(); 

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
       name: "default",
       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
       );

    app.MapRazorPages();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    app.Run();

    Log.Information("Application Starting");
}
catch(Exception ex)
{ 
    Log.Fatal(ex.ToString(),"Failed to start application");
}
finally
{
    Log.CloseAndFlush();
}
