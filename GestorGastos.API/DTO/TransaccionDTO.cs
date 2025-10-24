using System.ComponentModel.DataAnnotations;

namespace GestorGastos.API.DTO
{
    public class TransaccionDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public decimal Monto { get; set; }
        public int TipoOperacionId { get; set; }
        public string Nota { get; set; }
        public int CuentaId { get; set; }
        public int CategoriaId { get; set; }
    }
}
