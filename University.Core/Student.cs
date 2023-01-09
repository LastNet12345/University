using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace University.Core
{
#nullable disable
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Avatar { get; set; }
        public string Email { get; set; }


        // Navigation properties
        public Address Address { get; set; }

        public ICollection<Course> Courses { get; set; }

    }
}
