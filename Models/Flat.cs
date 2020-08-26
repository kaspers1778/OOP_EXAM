using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3
{
    class Flat
    {
        public int FlatId { get; set; }
        public int Number { get; set; }
        public House House { get; set; }
        public Entrance Entrance { get; set; }
        public int Rooms { get; set; }
        public int Square { get; set; }
        public int UsefulSquare { get; set; }
    }
}
