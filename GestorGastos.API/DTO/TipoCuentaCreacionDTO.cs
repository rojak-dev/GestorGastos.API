using System.ComponentModel.DataAnnotations;

namespace GestorGastos.API.DTO
{
    public class TipoCuentaCreacionDTO
    {
        
        [Required(ErrorMessage = "El campo {0} es requerido")] //net.core para validar en API
        [StringLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Orden { get; set; }
    }
}
