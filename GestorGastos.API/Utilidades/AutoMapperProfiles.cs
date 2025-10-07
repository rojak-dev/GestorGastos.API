using AutoMapper;
using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;

namespace GestorGastos.API.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() {
            ConfigurarMapeoTipoCeunta();
        }

        
        private void ConfigurarMapeoTipoCeunta()
        {
            // de TipoCuentaCreacionDTO -> TipoCuenta
            CreateMap<TipoCuentaCreacionDTO, TipoCuenta>();
            // de tipocuenta -> tipocuentaDTO
            CreateMap<TipoCuenta, TipoCuentaDTO>();
        }
    }
}
