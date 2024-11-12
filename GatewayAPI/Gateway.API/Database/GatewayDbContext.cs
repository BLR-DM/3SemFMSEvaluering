using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Gateway.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gateway.API.Database
{
    public class GatewayDbContext : IdentityDbContext<AppUser>
    {
        public GatewayDbContext(DbContextOptions<GatewayDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
