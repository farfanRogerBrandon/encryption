using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encryption_p2Api
{
    public class Log
    {
        [Column("id")]
        public int ID { get; set; }
        [Column("employeeID")]
        public int employeeID { get; set; }
        [Column("action"), StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Action { get; set; }
        [Column("registrationDate")]
        public DateTime RegistrationDate { get; set; }
    }
}
