
using Aspire.StackExchange.Redis; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Threading.Tasks;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Middlewares;
using Talabat.Core;
using Talabat.Core.Mapping.Products;
using Talabat.Core.Service.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Contexts;
using Talabat.Service.Services.Products;
namespace Talabat.APIs;

public class Program
{
    public static async Task Main(string[] args)
    {
        //StoreContext dbContext = new StoreContext();
        //await dbContext.Database.MigrateAsync();
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.AddRedisClient("redis");

        var app = builder.Build();
        await app.ConfigureMiddlewareAsync();
        app.Run();
    }
}
