using Mamba.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]);
});


var app = builder.Build();

app.MapControllerRoute("admin",
    "{area:exists}/{controller=home}/{action=index}/{id?}");

app.MapControllerRoute("default",
    "{controller=home}/{action=index}/{id?}");

app.UseStaticFiles();
app.UseRouting();

app.Run();
