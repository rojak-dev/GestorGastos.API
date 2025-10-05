using GestorGastos.API.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace GestorGastos.API.Controllers
{
    [Route("api/tipoCuenta")]
    [ApiController]
    public class TipoCuentaController : ControllerBase //para obtener metodos auxiliares pata trabajar con webapi
    {
        private readonly IConfiguration configuration;

        public TipoCuentaController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public List<TipoCuenta> Get()
        {
            return [];
        }

        [HttpGet("{id}")]
        public ActionResult<TipoCuenta> Get(int id)
        {
            var tipoCuenta = new TipoCuenta() { Nombre = "shshshs" };
            if (tipoCuenta is null)
            {
                return NotFound(); //404 recurso no encontrado
            }

            return tipoCuenta;
        }

    }
}
