using System.ComponentModel.DataAnnotations;

namespace GestorGastos.API.Entidades
{
    public class Cuenta
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")] //net.core para validar en API
        [StringLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int TipoCuentaId { get; set; }
        public double Balance { get; set; }
        [StringLength(1000, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        public string Descripcion { get; set; }
    }
}
