using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Koolitused.Models
{
    public class Opetaja
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Opetajanimi { get; set; }
        public int Roll { get; set; }
    }
}
