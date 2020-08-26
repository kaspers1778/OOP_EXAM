using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3
{
    class Contract
    {
        public int ContractId { get; set; }
        public Citzen Rentor { get; set; }
        public Flat Flat { get; set; }
        public DateTime Date { get; set; }
        public bool isDebt { get; set; }
        public int? Debt { get; set; }

    }
}
