using HooPulse.Application.Abstractions.Persistence;
using HooPulse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HooPulse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<HooPulseDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("HooPulseDb"));
        });

        services.AddScoped<IApplicationDbContext>(
            provider => provider.GetRequiredService<HooPulseDbContext>());

        return services;
    }
}