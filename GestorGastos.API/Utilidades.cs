using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;

namespace GestorGastos.API
{
    public static class Utilidades
    {
        public static TipoCuentaDTO convertirDTO(this TipoCuenta t) //this servira para aplicar como extension de la clase TipoCuenta
        {
            if (t != null)
            {
                return new TipoCuentaDTO
                {
                    Nombre = t.Nombre,
                    Orden = t.Orden
                };
            }

            return null;
        }
    }
}
