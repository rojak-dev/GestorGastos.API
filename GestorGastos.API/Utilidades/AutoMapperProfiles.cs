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

            //de CuentaCreacionDTO -> Cuenta
            CreateMap<CuentaCreacionDTO, Cuenta>();
            //de Cuenta -> CuentaDTO
            CreateMap<Cuenta, CuentaDTO>();

            //de CategoriaCreacionDTO -> Categoria
            CreateMap<CategoriaCreacionDTO, Categoria>();
            //de Categoria a CategoriaDTO
            CreateMap<Categoria, CategoriaDTO>();

            //de TransaccionCreacionDTO -> Transaccion
            CreateMap<TransaccionCreacionDTO, Transaccion>();
            //de Transaccion -> TransaccionDTO
        }
    }
}
