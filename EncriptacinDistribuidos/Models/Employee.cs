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
        public string FullName { get; set; }
        public string department { get; set; }

        public DateTime? RegistrationDate { get; set; }
        public string? UpdateDate { get; set; }
        public byte? Status { get; set; }
        public int DepartmentID { get; set; }

        public Department? Department { get; set; }
    }
}
