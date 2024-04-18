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


        }
    }
}