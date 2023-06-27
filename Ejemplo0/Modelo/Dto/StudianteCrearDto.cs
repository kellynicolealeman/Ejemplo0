using System.ComponentModel.DataAnnotations;

namespace Ejemplo0.Modelo.Dto
{
    public class StudianteCrearDto
    {
        [Required]
        public string? NombreStudiante { get; set; }
        [Required]
        public int IdGrado { get; set; }
    }
}
