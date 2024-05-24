using GraduateWork.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraduateWork.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ToDoItem>(b =>
            {
                b.ToTable("ToDoItem", t => t.ExcludeFromMigrations());

                b.Property(p => p.Id)
                    .HasColumnName("id")
                    .IsRequired();

                b.Property(p => p.Description)
                    .HasColumnName("description")
                    .IsRequired();

                b.HasKey(b => b.Id);

                b.HasOne(p => p.User)
                    .WithMany()
                    .HasForeignKey("userId")
                    .IsRequired();
            });

            builder.Entity<Reminder>(b =>
            {
                b.ToTable("Reminders", t => t.ExcludeFromMigrations());

                b.Property(r => r.Id)
                .HasColumnName("id")
                .IsRequired();

                b.HasOne(r => r.ToDoItem)
               .WithMany()
               .HasForeignKey(r => r.ToDoItemId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

                b.Property(r => r.ReminderDate)
                .HasColumnName("reminderDate")
                .IsRequired();

                b.Property(r => r.Completed)
                    .HasColumnName("completed");

                b.Property(r => r.CreatedAt)
                .HasColumnName("createdAt")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

                b.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .IsRequired();
            });


        }
    }
}