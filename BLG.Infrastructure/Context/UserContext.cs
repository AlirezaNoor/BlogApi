using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BLG.Infrastructure.Context;

public class UserContext : IdentityDbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var writerid = "7c1d8f4c-eb44-4016-b2b8-ac46a5702a56";
        var riderid = "f9eb43fe-b04e-4b13-b51d-4c612cc69de7";

        var role = new List<IdentityRole>
        {
            new IdentityRole()
            {
                Id = writerid,
                Name = "Writer",
                ConcurrencyStamp = writerid,
                NormalizedName = "Writer".ToUpper(),
            },
            new IdentityRole()
            {
                Id = riderid,
                Name = "Reader",
                ConcurrencyStamp = riderid,
                NormalizedName = "Reader".ToUpper(),
            }
        };
        builder.Entity<IdentityRole>().HasData(role);

        var adminid = "bc5295fb-8385-4e10-baf6-508aa0ac8c09";
        var admin = new IdentityUser()
        {
            Id = adminid,
            UserName = "alirezang@gmail.com",
            Email = "alirezang@gmail.com",
            NormalizedUserName = "alirezang@gmail.com".ToUpper(),
            NormalizedEmail = "alirezang@gmail.com".ToUpper(),
        };
        admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Adim@@123456");

        var adminrole = new List<IdentityUserRole<String>>()
        {
            new IdentityUserRole<string>()
            {
                RoleId = writerid,
                UserId = adminid
            },
            new IdentityUserRole<string>()
            {
                RoleId = riderid,
                UserId = adminid
            }
        };
        builder.Entity<IdentityUserRole<String>>().HasData(adminrole);
    }
}