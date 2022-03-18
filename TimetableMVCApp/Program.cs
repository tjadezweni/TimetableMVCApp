using Microsoft.EntityFrameworkCore;
using TimetableMVCApp.Helpers;
using TimetableMVCApp.Models;
using TimetableMVCApp.Repositories;
using TimetableMVCApp.Repositories.Impl;
using TimetableMVCApp.Uow;
using TimetableMVCApp.Uow.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SchoolDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<ITimeRepository, TimeRepository>();
builder.Services.AddScoped<IDayRepository, DayRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton<ModuleIdList>();

builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
