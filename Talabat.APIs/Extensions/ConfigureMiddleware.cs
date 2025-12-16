using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Contexts;
using Talabat.Repository.Identity;
using Talabat.Repository.Identity.Contexts;

namespace Talabat.APIs.Extensions
{
    public static class ConfigureMiddleware
    {
        public async static Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var _dbContext = service.GetRequiredService<StoreContext>();
            var identityContext = service.GetRequiredService<StoreIdentityDbContext>();
            var userManger = service.GetRequiredService<UserManager<AppUser>>();

            var loggerfactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbContext);
                await identityContext.Database.MigrateAsync();
                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManger);

            }
            catch (Exception ex)
            {

                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occured During Apply Migration");
            }

            app.MapDefaultEndpoints();
            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
            return app;
        }
    }
}
