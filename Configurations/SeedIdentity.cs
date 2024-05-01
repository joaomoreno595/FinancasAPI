using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Configurations;

public static class SeedIdentity
{
    public static void Seed(this ModelBuilder builder)
    {
        string roleUserId = Guid.NewGuid().ToString();
        builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = roleUserId, Name = "User", NormalizedName = "USER" });

        string roleAdminId = Guid.NewGuid().ToString();
        builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = roleAdminId, Name = "Admin", NormalizedName = "ADMIN" });

        string userId = Guid.NewGuid().ToString();
        IdentityUser user = new()
        {
            Id = userId,
            UserName = "Admin",
            Email = "admin@admin.com",
            NormalizedUserName = "ADMIN",
            NormalizedEmail = "ADMIN@ADMIN.COM"
        };
        PasswordHasher<IdentityUser> passwordHasher = new();
        user.PasswordHash = passwordHasher.HashPassword(user, "Admin*123");
        builder.Entity<IdentityUser>().HasData(user);

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>() { RoleId = roleUserId, UserId = userId });
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>() { RoleId = roleAdminId, UserId = userId });
    }
}
