﻿using System;
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
        }

        public DbSet<University.Core.Course> Course { get; set; } = default!;

        public DbSet<University.Core.Student> Student { get; set; } = default!;

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
        }
    }
}
