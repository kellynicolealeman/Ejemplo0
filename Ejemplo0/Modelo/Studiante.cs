using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ejemplo0.Modelo
{
    public class Studiante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdStudiante { get; set; }
        [Required]
        public string? NombreStudiante { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int IdGrado { get; set; }
        [ForeignKey ("IdGrado")]
        public Grado Grado { get; set; }
    }
}
