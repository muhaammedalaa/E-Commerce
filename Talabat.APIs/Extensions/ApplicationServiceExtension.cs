using Aspire.StackExchange.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Reflection.Metadata;
using System.Text;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Mapping.Basket;
using Talabat.Core.Mapping.Order;
using Talabat.Core.Mapping.Products;
using Talabat.Core.Repositories.Contarct;
using Talabat.Core.Service.Contract;
using Talabat.Repository;
using Talabat.Repository.Data.Contexts;
using Talabat.Repository.Identity.Contexts;
using Talabat.Repository.Repositories;
using Talabat.Service.Services.Cache;
using Talabat.Service.Services.Orders;
using Talabat.Service.Services.Payment;
using Talabat.Service.Services.Products;
using Talabat.Service.Services.Token;
using Talabat.Service.Services.User;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddBuiltInServices();
            services.AddBuiltInServicesSwagger();
            services.AddDbContextServices(Configuration);
            services.AddUserDefindServices();
            services.AddMappingServices(Configuration);
            services.ConfigureInvalidStateResponseServices();
            services.AddIdentityServices();
            services.AddAuthenticationServices(Configuration);
            //services.AddRedisServices(Configuration);
            // Add other application services here as needed
            return services;
        }
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();


            return services;
        }
        private static IServiceCollection AddBuiltInServicesSwagger(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddOpenApi();
            return services;
        }
        private static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnections"));

            });
            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnections"));

            });
            return services;
        }
        private static IServiceCollection AddUserDefindServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            return services;
        }
        private static IServiceCollection AddMappingServices(this IServiceCollection services, IConfiguration Configuration)
        {
           

            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(Configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            services.AddAutoMapper(M => M.AddProfile(new OrderProfile()));
           

            return services;
        }
        private static IServiceCollection ConfigureInvalidStateResponseServices(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationError
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
        //private static IServiceCollection AddRedisServices(this IServiceCollection services, IConfiguration Configuration)
        //{
        //    services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
        //    {
        //        var connection = Configuration.GetConnectionString("Redis");
        //        return ConnectionMultiplexer.Connect(connection);
        //    });

        //    return services;
        //}
        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {

            services.AddIdentity<AppUser, IdentityRole>()
                  .AddEntityFrameworkStores<StoreIdentityDbContext>();
            return services;
        }
        private static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))

                };


            });
            return services;
        }



    }
}
