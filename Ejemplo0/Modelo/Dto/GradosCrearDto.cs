using System.ComponentModel.DataAnnotations;

namespace Ejemplo0.Modelo.Dto
{
    public class GradosCrearDto
    {
        [Required]
        [StringLength(50)]
        public string? NombreGrado { get; set; }
        [Required]
        public char Seccion { get; set; }
    }
}
