using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FMSEvaluering.Infrastructure.Configuration;

public static class AuthorizationService
{
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Student", policy =>
                policy.RequireClaim("usertype", "student"));

            options.AddPolicy("Teacher", policy =>
                policy.RequireClaim("usertype", "teacher"));
        });

        return services;
    }
}