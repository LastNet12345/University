using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Core
{
    public class Enrollment
    {
        //Bad idea in this case
        //public int Id { get; set; }


        public int Grade { get; set; } // This is a payload

        // Foreign Keys
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        // Nav props
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
