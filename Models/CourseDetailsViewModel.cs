using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koolitused.Models
{
    public class CourseDetailsViewModel
    {
        public string CourseName { get; set; }
        public string CourseDate { get; set; }
        public string TeacherName { get; set; }
        public List<RegKursile> Students { get; set; }
    }
}
