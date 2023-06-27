using System.ComponentModel.DataAnnotations;

namespace Ejemplo0.Modelo.Dto
{
    public class StudianteUpdateDto
    {
        [Required]
        public int IdStudiante { get; set; }
        [Required]
        public string? NombreStudiante { get; set; }
        [Required]
        public int IdGrado { get; set; }
    }
}
