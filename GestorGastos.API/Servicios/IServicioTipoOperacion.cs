using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Utilidades;

namespace GestorGastos.API.Servicios
{
    public interface IServicioTipoOperacion
    {
        public Task<PagedResponse<TipoOperacion>> getAllTiposOperacionPaginado(PaginacionDTO paginacion);
        public Task<IEnumerable<TipoOperacion>> getAll();
        public Task<TipoOperacion> getTipoOperacionById(int id);
        public Task NewTipoOperacion(TipoOperacion t);
        public Task SetTipoOperacion(TipoOperacion t);
        public Task DeleteTipoOperacion(int id);
    }
}
