using Hahn.Application.Interfaces;
using Hahn.Application.Mappers;
using Hahn.Application.Services;
using Hahn.Domain.Interfaces;
using Hahn.Infrastructure.Contexts;
using Hahn.Infrastructure.Repositories;
using Hahn.Jobs.DataJobs;
using Hahn.Jobs.Interfaces;
using Hahn.Worker;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHangfire(configuration => configuration
            .UseSqlServerStorage(hostContext.Configuration.GetConnectionString("DefaultConnection"))
        );
        services.AddHangfireServer();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));

        services.AddAutoMapper(typeof(DepartmentMappingProfile));
        services.AddAutoMapper(typeof(ArtObjectMappingProfile));

        services.AddScoped<IMuseumApiService, MuseumApiService>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IArtObjectRepository, ArtObjectRepository>();
        services.AddScoped<IUpdateMuseumDataJob, UpdateMuseumDataJob>();

        services.AddHttpClient<IMuseumApiService, MuseumApiService>(client =>
        {
            var baseUrl = hostContext.Configuration["MuseumApi:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("Base URL for Museum API is not configured.");
            }
            client.BaseAddress = new Uri(baseUrl);
        });

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();