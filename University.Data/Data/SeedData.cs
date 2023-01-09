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
using System.Globalization;

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

            var courses = GenerateCourses(20);
            await db.AddRangeAsync(courses);

            var enrollments = GenerateEnrollments(courses, students);
            await db.AddRangeAsync(enrollments);

            await db.SaveChangesAsync();
            
        }

        private static IEnumerable<Enrollment> GenerateEnrollments(IEnumerable<Course> courses, IEnumerable<Student> students)
        {
            var rnd = new Random();

            var enrollments = new List<Enrollment>();

            foreach (var student in students) 
            {
                foreach (var course in courses)
                {
                    if (rnd.Next(0, 5) == 0)
                    {
                        var enrollment = new Enrollment
                        {
                            Course = course,
                            Student = student,
                            Grade = rnd.Next(1, 6)
                        };
                            
                        enrollments.Add(enrollment);
                    }

                }
            }

            return enrollments;
        }

        private static IEnumerable<Course> GenerateCourses(int numberOfCourses)
        {
            var courses = new List<Course>();

            for (int i = 0; i < numberOfCourses; i++)
            {
                // "hej jag heter david" => "Hej Jag Heter David"
                var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Company.Bs());
                var course = new Course { Title = title };
                courses.Add(course);
            }

            return courses;
        }

        private static IEnumerable<Student> GenerateStudents(int numberOfStudents)
        {
            var students = new List<Student>();

            for (int i = 0; i < numberOfStudents; i++)
            {
                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                var avatar = faker.Internet.Avatar();
                var email = faker.Internet.Email(fName, lName, "lexicon.se");

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
