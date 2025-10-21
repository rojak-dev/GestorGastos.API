namespace GestorGastos.API.DTO
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int TipoOperacionId { get; set; }
        public required int UsuarioId { get; set; }
    }
}
