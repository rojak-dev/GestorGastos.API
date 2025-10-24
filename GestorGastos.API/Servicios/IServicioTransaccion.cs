using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Utilidades;

namespace GestorGastos.API.Servicios
{
    public interface IServicioTransaccion
    {
        public Task<PagedResponse<Transaccion>> getAllTransaccionesPaginado(PaginacionDTO paginacion);
        public Task<IEnumerable<Transaccion>> getAll();
        public Task<Transaccion> getTransaccionById(int id);
        public Task NewTransaccion(Transaccion t);
        public Task SetTransaccion(Transaccion t);
        public Task DeleteTransaccion(int id);
    }
}
