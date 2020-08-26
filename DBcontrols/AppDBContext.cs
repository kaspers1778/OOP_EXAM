using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3
{
    class AppDBContext : DbContext
    {
        public DbSet<HouseComplex> HouseComplexs { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Entrance> Entrances { get; set; }
        public DbSet<Flat> Flats { get; set; }
        public DbSet<Citzen> Citzens{ get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public AppDBContext() : base("DB")
        {

        }
    }
}
