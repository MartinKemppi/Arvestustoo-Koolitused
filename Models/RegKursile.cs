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
        public string Nimi { get; set; }
        public string Perenimi { get; set; }
        public int kursId { get; set; }
        public virtual Koolitus kursid { get; set; }
        public string KasutajaNimi { get; set; }
    }
}
