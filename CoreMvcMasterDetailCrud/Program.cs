using CoreMvcMasterDetailCrud.Contracts;
using CoreMvcMasterDetailCrud.Models;
using CoreMvcMasterDetailCrud.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("con")));
//builder.Services.AddScoped<IStudentRepository, StudentRepository>();
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(name: "Default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//app.MapGet("/", () => "Hello World!");

app.Run();
