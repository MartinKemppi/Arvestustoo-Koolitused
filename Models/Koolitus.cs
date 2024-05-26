using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Koolitused.Models
{
    public class Koolitus
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Koolitusnimi { get; set; }
        public int OpetajaId { get; set; }
        public string Kuupaev { get; set; }
        public int Hind {  get; set; }

        [Ignore]
        public string Opetajanimi { get; set; }
    }
}
