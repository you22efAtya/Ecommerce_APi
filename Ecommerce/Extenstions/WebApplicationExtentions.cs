using Domain.Contracts;
using Ecommerce.Middlewares;

namespace Ecommerce.Extenstions
{
    public static class WebApplicationExtentions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
            await dbInitializer.InitializeAsync();
            return app;
        }
        public static WebApplication UseCustomeMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
