using FinalExam_B14.DAL;
using FinalExam_B14.Models;
using FinalExam_B14.Repositories.Implementations;
using FinalExam_B14.Repositories.Interfaces;
using FinalExam_B14.Services.Implementations;
using FinalExam_B14.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//addScoppeds
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

var app = builder.Build();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
