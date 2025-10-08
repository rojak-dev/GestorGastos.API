using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Utilidades;

namespace GestorGastos.API.Servicios
{
    public interface IServicioTipoCuenta
    {
        public Task<PagedResponse<TipoCuenta>> getAllTiposCuentas(PaginacionDTO paginacion);
        public Task<TipoCuenta> getTipoCuentaById(int id);
        public Task NewTipoCuenta(TipoCuenta t);
        public Task SetTipoCuenta(TipoCuenta t);
        public Task DeleteTipoCuenta(int id);
        Task<bool> Existe(string nombre, int usuarioId);
    }
}
