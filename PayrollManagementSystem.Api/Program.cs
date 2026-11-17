using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Payrollmanagementsystem.Models;
using PayrollManagementSystem.Api.Common;
using PayrollManagementSystem.Repositories;
using PayrollManagementSystem.Services;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

IConfiguration configuration = builder.Configuration;

builder.Services.AddDbContextPool<ApplicationDbContext>(e =>
{
    e.UseMySql(
        configuration.GetConnectionString("LocalConnection"),
        ServerVersion.AutoDetect(configuration.GetConnectionString("LocalConnection")),
        e => e.MigrationsAssembly("PayrollManagementSystem.Api")
        );
});

JwtSettings.Configure(builder);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IApplicationUser, ApplicationUserRepository>();
builder.Services.AddScoped<IDepartment, DepartmentRepository>();
builder.Services.AddScoped<IEmployee, EmployeeRepository>();
builder.Services.AddScoped<IDesignation, DesignationRepository>();
builder.Services.AddScoped<IPayroll, PayrollRepository>();
builder.Services.AddScoped<ISalaryStructure, SalaryStructureRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/", e =>
    {
        e.Theme = ScalarTheme.DeepSpace;
        e.WithTitle("arshdon`");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();