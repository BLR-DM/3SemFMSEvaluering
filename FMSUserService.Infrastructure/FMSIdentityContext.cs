using FMSUserService.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FMSUserService.Infrastructure;

public class FMSIdentityContext : IdentityDbContext<AppUser>
{
    public FMSIdentityContext(DbContextOptions<FMSIdentityContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}