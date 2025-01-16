using HACS.Data;
using HACS.Interfaces;
using HACS.Models;
using HACS.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HACS;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

        builder.Services.AddDbContext<ApplicationDBContext>(options =>
        {
            // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            options.UseSqlServer(
                "Server=localhost;Database=hacs;User Id=SA;Password=SqlServer1!;TrustServerCertificate=True;Encrypt=false;");
        });

        builder.Services.AddScoped<IRepository<Donation>, DonationRepository>();
        builder.Services.AddScoped<IRepository<Donor>, DonorRepository>();
        builder.Services.AddScoped<IRepository<DonationAdmin>, DonationAdminRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}