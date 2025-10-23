using System.ComponentModel.DataAnnotations;

namespace GestorGastos.API.DTO
{
    public class TipoOperacionCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] //net.core para validar en API
        [StringLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        public required string Descripcion { get; set; }
    }
}
