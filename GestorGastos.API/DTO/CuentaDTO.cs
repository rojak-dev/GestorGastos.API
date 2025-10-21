namespace GestorGastos.API.DTO
{
    public class CuentaDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int TipoCuentaId { get; set; }
        public double Balance { get; set; }
        public string Descripcion { get; set; }
    }
}
