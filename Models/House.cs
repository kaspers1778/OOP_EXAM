using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3
{
    class House
    {
        public int HouseId { get; set; }
        public int Number { get; set; }
        public string Street { get; set; }
        public int Floors { get; set; }
        public HouseComplex HouseComplex {get;set;}
    }
}
