using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAppCamilla.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<UserAddress> UserAddresses { get; set; }

    public DbSet<UserProfile> UserProfiles { get; set; }
    public virtual DbSet<UserCoursesEntity> UserCourses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserCoursesEntity>()
           .HasKey(x => new { x.UserId, x.CourseId });
    }
}
