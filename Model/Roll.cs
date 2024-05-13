using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Koolitused.Model
{
    public class Roll
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Rollnimi { get; set; }
    }
}
