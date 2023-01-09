using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using University.Core;

namespace University.Data.Data
{
    public class SeedData
    {
        private static Faker faker = null!;
        public static async Task InitAsync(UniversityContext db)
        {
            if (await db.Student.AnyAsync()) return;
            
            faker = new Faker("sv");

            var students = GenerateStudents(50);
            await db.AddRangeAsync(students);
            await db.SaveChangesAsync();
            
        }

        private static IEnumerable<Student> GenerateStudents(int numberOfStudents)
        {
            var students = new List<Student>();

            for (int i = 0; i < numberOfStudents; i++)
            {
                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                var avatar = faker.Internet.Avatar();
                var email = faker.Internet.Email(fName, lName, "lexicon");

                var student = new Student
                {
                    Avatar = avatar,
                    FirstName = fName,
                    LastName = lName,
                    Email = email,
                    Address = new Core.Address
                    {
                        Street = faker.Address.StreetAddress(),
                        City = faker.Address.City(),
                        ZipCode = faker.Address.ZipCode()
                    }
                };

                students.Add(student);
            }

            return students;
        }
    }
}
