
using ChainOfResponsibility.Domain.Approvals;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChainOfResponsibility.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration config)
    {

        services.AddTransient<IPurchaseApprover, EmployeeApprover>();
        services.AddTransient<IPurchaseApprover, SupervisorApprover>();
        services.AddTransient<IPurchaseApprover, GeneralManagerApprover>();
        services.AddTransient<ApprovalChain>();

        return services;
    }
}
