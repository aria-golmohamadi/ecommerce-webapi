using Application.Contracts;
using Identity.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Exceptions;
using Persistence.Repositories;

namespace Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
        {
            string connectionString = configuration.GetConnectionString("Application") ?? throw new ApplicationConnectionStringNotFoundException();
            optionsBuilder.UseSqlServer(connectionString);
        });

        services.AddIdentity()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));

        return services;
    }
}
