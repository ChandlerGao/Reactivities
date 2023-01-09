
using System.Runtime.CompilerServices;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
     public class DataContext : IdentityDbContext<AppUser>
     {
          public DataContext(DbContextOptions options) : base(options)
          {

          }

          public DbSet<Activity> Activities { get; set; }

          public DbSet<ActivityAttendee> ActivityAttendees { get; set; }

          //处理多对多的映射，外键
          protected override void OnModelCreating(ModelBuilder builder)
          {
               base.OnModelCreating(builder);

               builder.Entity<ActivityAttendee>(x => x.HasKey(y => new { y.AppUserId, y.ActivityId }));

               builder.Entity<ActivityAttendee>()
                      .HasOne(u => u.AppUser)
                      .WithMany(v => v.Activities)
                      .HasForeignKey(w => w.AppUserId);

               builder.Entity<ActivityAttendee>()
                    .HasOne(u => u.Activity)
                    .WithMany(v => v.Attendees)
                    .HasForeignKey(w => w.ActivityId);
          }
     }
}