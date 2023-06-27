using System.ComponentModel.DataAnnotations;

namespace Ejemplo0.Modelo.Dto
{
    public class GradosUpdateDto
    {
        [Required]
        public int IdGrados { get; set; }
        [Required]
        [StringLength(50)]
        public string? NombreGrado { get; set; }
        [Required]
        public char Seccion { get; set; }
    }
}
