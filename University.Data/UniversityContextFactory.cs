using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data.Data;

namespace University.Data
{
    public class UniversityContextFactory : IDesignTimeDbContextFactory<UniversityContext>
    {
        public UniversityContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<UniversityContext>();
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=University.Data;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new UniversityContext(options.Options);
        }
    }
}
