using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Koolitused.Models
{
    public class Kasutaja
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Kasutajanimi { get; set; }
        public string Kasutajasalasona { get; set; }
        public int Roll { get; set; }
    }
}
