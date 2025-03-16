using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncriptacinDistribuidos.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? UpdateDate { get; set; }
        public byte? Status { get; set; }
    }

}
