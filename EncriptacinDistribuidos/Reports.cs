using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncriptacinDistribuidos
{
    public class Reports
    {
        public string algorithmName { get; set; }
        public double totalTime { get; set; }
        public double avgTime { get; set; }

        public double totalMemory { get; set; }

        public double beforeProcessor { get; set; }
        public double afterProcessor { get; set; }

    }
}
