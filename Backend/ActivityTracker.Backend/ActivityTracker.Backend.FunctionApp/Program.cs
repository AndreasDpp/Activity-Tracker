using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ActivityTracker.Backend.Repository.Interfaces;
using ActivityTracker.Backend.Repository.Repositories;
using ActivityTracker.Backend.Repository.Domain.Settings;
using ActivityTracker.Backend.Service.Services;
using ActivityTracker.Backend.Service.Inferfaces;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        
        #region AutoMapper

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        #endregion

        #region MongoDB Settings

        services.AddSingleton(new MongoDbSettings
        {
            DatabaseName = Environment.GetEnvironmentVariable("MongoDB:DatabaseName"),
            ConnectionString = Environment.GetEnvironmentVariable("MongoDB:ConnectionString")
        });

        #endregion

        #region Repositories

        services.AddScoped<IActivityRepository, ActivityRepository>();

        #endregion

        #region Services

        services.AddScoped<IActivityService, ActivityService>();

        #endregion

    })
    .Build();

host.Run();
