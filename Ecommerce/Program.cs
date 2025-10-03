
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data;
using Persistance.Data.DataSeeding;
using Persistance.Repositories;
using Services;
using Services.Abstraction;
using System.Threading.Tasks;

namespace Ecommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemplyRefrence).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDbIntializer, DpIntializer>();


            var app = builder.Build();
            await IntializeDbAsync(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
            async Task IntializeDbAsync(WebApplication app)
            {
                using var scope = app.Services.CreateScope();
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
                await dbInitializer.InitializeAsync();
            }
        }
    }
}
