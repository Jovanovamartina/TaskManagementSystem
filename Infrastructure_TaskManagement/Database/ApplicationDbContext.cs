
using Core_TaskManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_TaskManagement.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<LogTime> LogTimes { get; set; }
        public DbSet<TaskReminder> TaskReminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //ApplicationUser
            modelBuilder.Entity<ApplicationUser>(entity =>
            {

                entity.HasMany(u => u.Comments)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);

                entity.HasMany(u => u.LogTimes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);

                modelBuilder.Entity<ApplicationUser>()
                    .Property(u => u.FirstName)
                    .HasMaxLength(20);

                modelBuilder.Entity<ApplicationUser>()
                    .Property(u => u.LastName)
                    .HasMaxLength(20);
            });

            //Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();

                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description).IsRequired().HasMaxLength(100);

                entity.Property(p => p.StartDate).IsRequired();
                entity.Property(p => p.EndDate).IsRequired();

                entity.Property(p => p.Status).HasConversion<string>();
                entity.Property(p => p.Priority).HasConversion<string>();

                //relationships

                entity.HasMany(p => p.Issues)
                      .WithOne(i => i.Project)
                      .HasForeignKey(i => i.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.AssignedTeamMembers)
                      .WithMany(u => u.AssignedProjects)
                      .UsingEntity<Dictionary<string, object>>(
               "ProjectApplicationUser",
               j => j.HasOne<ApplicationUser>()
                     .WithMany()
                     .HasForeignKey("UserId")
                     .OnDelete(DeleteBehavior.Cascade),
               j => j.HasOne<Project>()
                     .WithMany()
                     .HasForeignKey("ProjectId")
                     .OnDelete(DeleteBehavior.Cascade));

                entity.HasMany(p => p.News)
                     .WithOne(i => i.Project)
                     .HasForeignKey(i => i.ProjectId)
                     .OnDelete(DeleteBehavior.Cascade);
            });

            //Comment
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(c => c.CommentText)
                      .HasMaxLength(1000)
                      .IsRequired(false);

                entity.HasOne(c => c.Issue)
                       .WithMany(i => i.Comments)
                       .HasForeignKey(c => c.IssueId)
                       .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.Author)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(c => c.AuthorId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            //Issue
            modelBuilder.Entity<Issue>(entity =>
            {
                entity.HasIndex(i => i.Code)
                .IsUnique();

                entity.Property(i => i.TaskType)
                .HasConversion<string>();

                entity.Property(i => i.Status)
                .HasConversion<string>();

                entity.Property(i => i.Priority)
                .HasConversion<string>();

                entity.Property(i => i.Module)
                .HasConversion<string>();

                entity.Property(i => i.Title)
                .HasMaxLength(150);

                entity.Property(i => i.Description)
                .HasMaxLength(1000);


                //relationship
                entity.HasOne(i => i.Project)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(i => i.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(i => i.CreatedBy)
                .WithMany()
                .HasForeignKey(i => i.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(i => i.AssignedUser)
                .WithMany()
                .HasForeignKey(i => i.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<Issue>()
                    .HasMany(i => i.Comments)
                    .WithOne(c => c.Issue)
                    .HasForeignKey(c => c.IssueId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Issue>()
                    .HasMany(i => i.LogTimes)
                    .WithOne(lt => lt.Issue)
                    .HasForeignKey(lt => lt.IssueId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //Logtime
            modelBuilder.Entity<LogTime>(entity =>
            {

                entity.Property(p => p.Comment).IsRequired().HasMaxLength(100);

                entity.Property(p => p.Activity).HasConversion<string>();
                entity.Property(p => p.WorkFrom).HasConversion<string>();

                modelBuilder.Entity<LogTime>()
                    .HasOne(lt => lt.Issue)
                    .WithMany(i => i.LogTimes)
                    .HasForeignKey(lt => lt.IssueId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<LogTime>()
                    .HasOne(lt => lt.User)
                    .WithMany(u => u.LogTimes)
                    .HasForeignKey(lt => lt.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //News
            modelBuilder.Entity<News>(entity =>
            {

                entity.Property(p => p.Title).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description).IsRequired().HasMaxLength(1000);

                entity.HasOne(n => n.Project)
                .WithMany(p => p.News)
                .HasForeignKey(n => n.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(n => n.CreatedByUser)
                .WithMany()
                .HasForeignKey(n => n.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            //TaskReminder
            modelBuilder.Entity<TaskReminder>(entity =>
            {
                entity.Property(tr => tr.Title)
                .HasMaxLength(100);

                entity.Property(tr => tr.Description)
                .HasMaxLength(500);

                entity.HasOne(tr => tr.User)
                   .WithMany()
                   .HasForeignKey(tr => tr.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}


