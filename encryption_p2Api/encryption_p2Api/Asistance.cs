using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace encryption_p2Api
{
    public class Asistance
    {
        [Key, Column("id")]
        public int ID { get; set; }
        [DataType(DataType.Date), Column("date")]
        public DateTime Date { get; set; }
        [Column("markDate")]
        public DateTime MarkDate { get; set; }
        [Column("type"), StringLength(40, ErrorMessage = "Maximo 40 caracteres")]
        public string Type { get; set; }
        [Column("employeeID")]
        public int EmployeeID { get; set; }

    }
}
