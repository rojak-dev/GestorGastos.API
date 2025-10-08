namespace GestorGastos.API.DTO
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        public int recordsporpagina = 10;
        private readonly int cantidadMaximaRecordsPorPagina = 50;
        public int RecordsPorPagina { 
            get { return recordsporpagina; }
            set {
                //value es el valor que asigna el usuario
                recordsporpagina = (value > cantidadMaximaRecordsPorPagina) ? cantidadMaximaRecordsPorPagina: value;
            }
        }
    }
}
