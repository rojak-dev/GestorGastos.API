using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Utilidades;

namespace GestorGastos.API.Servicios
{
    public interface IServicioCuenta
    {
        public Task<PagedResponse<Cuenta>> getAllCuentasPaginado(PaginacionDTO paginacion);
        public Task<IEnumerable<Cuenta>> getAll();
        public Task<Cuenta> getCuentaById(int id);
        public Task NewCuenta(Cuenta t);
        public Task SetCuenta(Cuenta t);
        public Task DeleteCuenta(int id);
        Task<bool> Existe(string nombre, int usuarioId);
    }
}
