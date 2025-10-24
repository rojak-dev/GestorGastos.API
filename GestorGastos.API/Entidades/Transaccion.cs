using System.ComponentModel.DataAnnotations;

namespace GestorGastos.API.Entidades
{
    public class Transaccion
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaTransaccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Monto { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int TipoOperacionId { get; set; }
        [StringLength(1000, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        public string Nota { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int CuentaId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int CategoriaId { get; set; }
    }
}
