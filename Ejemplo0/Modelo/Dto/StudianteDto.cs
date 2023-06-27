using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ejemplo0.Modelo.Dto
{
    public class StudianteDto
    {
        public int IdStudiante { get; set; }
        [Required]
        public string? NombreStudiante { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int IdGrado { get; set; }
    }
}
