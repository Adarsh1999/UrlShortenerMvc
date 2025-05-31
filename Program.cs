using Microsoft.EntityFrameworkCore;
using UrlShortenerMvc.Data;
using UrlShortenerMvc.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Suppress EF Core SQL logs
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);

// Set global minimum log level to Warning
builder.Logging.SetMinimumLevel(LogLevel.Warning);
builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
builder.Logging.AddFilter("System", LogLevel.Warning);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateUrlValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Configure PostgreSQL
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("UrlDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configure routes
app.MapControllerRoute(
    name: "redirect",
    pattern: "{code}",
    defaults: new { controller = "ShortUrls", action = "Go" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
