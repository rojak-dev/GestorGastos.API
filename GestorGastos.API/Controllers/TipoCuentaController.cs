using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestorGastos.API.Controllers
{
    [Route("api/tipoCuenta")]
    [ApiController]
    public class TipoCuentaController : ControllerBase //para obtener metodos auxiliares pata trabajar con webapi
    {
        private readonly IConfiguration configuration;
        private readonly IServicioTipoCuenta servicioTipoCuenta;

        public TipoCuentaController(IConfiguration configuration, IServicioTipoCuenta servicioTipoCuenta)
        {
            this.configuration = configuration;
            this.servicioTipoCuenta = servicioTipoCuenta;
        }

        [HttpGet]
        public IEnumerable<TipoCuentaDTO> Get()
        {
            var list = servicioTipoCuenta.getAllTiposCeuntas().Select(e => e.convertirDTO());
            return list;
        }

        [HttpGet("{id}")]
        public ActionResult<TipoCuentaDTO> Get(int id)
        {
            var tipoCuenta = servicioTipoCuenta.getTipoCuentaById(id).convertirDTO();
            if (tipoCuenta is null)
            {
                return NotFound("Tipo cuenta no encontrada"); //404 recurso no encontrado
            }

            return tipoCuenta;
        }

        [HttpPost]
        public ActionResult<TipoCuenta> post(TipoCuenta t)
        {
            servicioTipoCuenta.NuevoTipoCuenta(t);
            return Ok();
        }

        [HttpPut]
        public ActionResult<TipoCuenta> put(TipoCuenta t)
        {
            var tipoAux = servicioTipoCuenta.getTipoCuentaById(t.Id);
            if (tipoAux is null) return NotFound();

            tipoAux.Nombre = t.Nombre;

            servicioTipoCuenta.ModificarTipoCuenta(tipoAux);
            return Ok();
        }

        [HttpDelete]
        public ActionResult delete(int id)
        {
            var tipoAux = servicioTipoCuenta.getTipoCuentaById(id);
            if (tipoAux is null) return NotFound();

            servicioTipoCuenta.BajaTipoCuenta(tipoAux.Id);
            return Ok();
        }

    }
}
