using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncriptacinDistribuidos.Models
{
    public class Assistance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime MarkDate { get; set; }
        public string Type { get; set; }
        public int EmployeeID { get; set; }

        public Employee? Employee { get; set; }
    }
}
