using Microsoft.Extensions.DependencyInjection;

namespace Gateway.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddReverseProxy();
            services.AddHttpClient<IUserService, UserService>(client =>
            {
                client.BaseAddress = new Uri(configuration
                    .GetSection("ReverseProxy:Clusters:fmsdataserver-cluster:Destinations:destination1:Address").Value);
            });

            return services;
        }
    }
}
