using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encryption_p2Api
{
    public class Employee
    {
        [Key, Column("id")]
        public int ID { get; set; }
        [StringLength(10, ErrorMessage = "Debe ser máximo 10 caracteres")]
        [Column("code")]
        public string Code { get; set; }
        [StringLength(50, ErrorMessage = "Debe ser máximo 50 caracteres")]
        [Column("fullName")]
        public byte[] FullName { get; set; }
        [Column("registrationDate")]
        public DateTime RegistrationDate { get; set; }
        [Column("updateDate")]
        public DateTime UpdateDate { get; set; }
        [Column("status")]
        public byte Status { get; set; }
        [Column("department")]
        public byte[] Department { get; set; }


        public IEnumerable<Asistance> asistances { get; set; }
    }
}
