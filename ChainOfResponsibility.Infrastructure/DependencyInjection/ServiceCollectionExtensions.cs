using ChainOfResponsibility.Application.Abstractions.Persistence.Repositories;
using ChainOfResponsibility.Application.Abstractions.Persistence.UnitOfWork;
using ChainOfResponsibility.Infrastructure.Persistence;
using ChainOfResponsibility.Infrastructure.Persistence.Repositories;
using ChainOfResponsibility.Infrastructure.Persistence.UnitOfWork;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChainOfResponsibility.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var cs = config.GetConnectionString("Default")
                 ?? throw new InvalidOperationException("Missing ConnectionStrings:Default");

        services.AddScoped<DbContext>(sp => sp.GetRequiredService<AppDbContext>());
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(cs));
        services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
        services.AddScoped< IUnitOfWork,UnitOfWork>();

        return services;
    }
}
