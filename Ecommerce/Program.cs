
using Domain.Contracts;
using Ecommerce.Extenstions;
using Ecommerce.Factories;
using Ecommerce.Middlewares;
using Microsoft.AspNetCore.Mvc;
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

            builder.Services.AddPresentationServices();

            builder.Services.AddCoreServices(builder.Configuration);

            builder.Services.AddInfrastructureServics(builder.Configuration);
            
            
            


            var app = builder.Build();
            
            await app.SeedDbAsync();
            app.UseCustomeMiddlewares();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
