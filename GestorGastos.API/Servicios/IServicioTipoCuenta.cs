using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;

namespace GestorGastos.API.Servicios
{
    public interface IServicioTipoCuenta
    {
        public IEnumerable<TipoCuenta> getAllTiposCeuntas();
        public TipoCuenta getTipoCuentaById(int id);
        public void NuevoTipoCuenta(TipoCuenta t);
        public void ModificarTipoCuenta(TipoCuenta t);
        public void BajaTipoCuenta(int id);
    }
}
