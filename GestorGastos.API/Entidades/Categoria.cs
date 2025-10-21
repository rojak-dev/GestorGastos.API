using System.ComponentModel.DataAnnotations;

namespace GestorGastos.API.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")] //net.core para validar en API
        [StringLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int TipoOperacionId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int UsuarioId { get; set; }
    }
}
