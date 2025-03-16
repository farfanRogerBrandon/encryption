using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncriptacinDistribuidos.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public byte[] FullName { get; set; }
        public byte[] Department { get; set; }

        public DateTime? RegistrationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte? Status { get; set; }
    }
}
