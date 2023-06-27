using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ejemplo0.Modelo
{
    public class Grado
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdGrados { get; set; }
        [Required]
        [StringLength(50)]
        public string? NombreGrado { get; set; }
        [Required]
        public char Seccion { get; set; }
    }
}
