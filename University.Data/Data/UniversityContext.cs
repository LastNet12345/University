using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using University.Core;

namespace University.Data.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext (DbContextOptions<UniversityContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<University.Core.Course> Course { get; set; } = default!;
        public DbSet<University.Core.Student> Student { get; set; } = default!;
        public DbSet<University.Core.Address> Address { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FluentAPI goes here
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity<Enrollment>(
                    e => e.HasOne(e => e.Course).WithMany(c => c.Enrollments),
                    e => e.HasOne(e => e.Student).WithMany(s => s.Enrollments));

            // Changed column name
            modelBuilder.Entity<Course>().Property(c => c.Title).HasColumnName("Course Name");

            // Defined composite key for junction entity (kopplingstabell)
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.CourseId, e.StudentId });

            // Shadow Property
            modelBuilder.Entity<Student>().Property<DateTime>("Edited");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Student>().Where(e => e.State == EntityState.Modified))
            {
                entry.Property("Edited").CurrentValue = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
