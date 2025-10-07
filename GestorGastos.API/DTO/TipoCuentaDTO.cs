using System.ComponentModel.DataAnnotations;

namespace GestorGastos.API.DTO
{
    public class TipoCuentaDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public int Orden { get; set; }
    }
}
