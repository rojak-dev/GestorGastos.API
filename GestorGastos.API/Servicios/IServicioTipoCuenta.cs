using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;

namespace GestorGastos.API.Servicios
{
    public interface IServicioTipoCuenta
    {
        public Task<IEnumerable<TipoCuenta>> getAllTiposCeuntas();
        public Task<TipoCuenta> getTipoCuentaById(int id);
        public Task NuevoTipoCuenta(TipoCuenta t);
        public Task ModificarTipoCuenta(TipoCuenta t);
        public Task BajaTipoCuenta(int id);
    }
}
