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
        }

        public DbSet<University.Core.Course> Course { get; set; } = default!;

        public DbSet<University.Core.Student> Student { get; set; } = default!;
    }
}
