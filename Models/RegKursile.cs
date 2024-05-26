using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koolitused.Models
{
    public class RegKursile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AdditionalInfo { get; set; }
        public string Username { get; set; }
        public int CourseId { get; set; }

        [Ignore]
        public string CourseName { get; set; }
    }
}
