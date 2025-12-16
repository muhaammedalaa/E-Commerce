using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis")
    .WithRedisCommander();
builder.AddProject<Projects.Talabat_APIs>("talabat-apis").WithExternalHttpEndpoints().WithReference(redis);

builder.Build().Run();
