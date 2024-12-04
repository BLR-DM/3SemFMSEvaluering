using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace FMSExitSlip.Infrastructure.Configuration
{
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
}
