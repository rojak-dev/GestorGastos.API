using AutoMapper;
using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Servicios;
using GestorGastos.API.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace GestorGastos.API.Controllers
{
    [Route("api/tipoCuenta")]
    [ApiController]
    public class TipoCuentaController : ControllerBase //para obtener metodos auxiliares pata trabajar con webapi
    {
        private readonly IConfiguration configuration;
        private readonly IServicioTipoCuenta servicioTipoCuenta;
        private readonly IMapper mapper;

        public TipoCuentaController(IConfiguration configuration, IServicioTipoCuenta servicioTipoCuenta, IMapper mapper)
        {
            this.configuration = configuration;
            this.servicioTipoCuenta = servicioTipoCuenta;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<TipoCuentaDTO>>> Get([FromQuery] PaginacionDTO paginacion)
        {
            var resultado = await servicioTipoCuenta.getAllTiposCuentas(paginacion);
            var dtoItems = mapper.Map<List<TipoCuentaDTO>>(resultado.Items);

            var response = new PagedResponse<TipoCuentaDTO>(resultado.Total, dtoItems);

            await HttpContext.InsertarParametrosPaginacionEnCabecera(resultado.Total, paginacion.Pagina, paginacion.recordsporpagina);

            var tipoCuentaDTO = mapper.Map<List<TipoCuentaDTO>>(resultado.Items);

            
            return Ok(response);
        }

        [HttpGet("{id}", Name = "ObtenerTipoCuentaPorId")]
        public async Task<ActionResult<TipoCuentaDTO>> Get(int id)
        {
            var tipoCuenta = (await servicioTipoCuenta.getTipoCuentaById(id)).convertirDTO(); //otra forma de convertir a DTO
            if (tipoCuenta is null)
            {
                return NotFound("Tipo cuenta no encontrada"); //404 recurso no encontrado
            }

            return Ok(tipoCuenta);
        }

        [HttpPost]
        public async Task<ActionResult> post([FromBody] TipoCuentaCreacionDTO t)
        {
            var yaExiste = await servicioTipoCuenta.Existe(t.Nombre,t.UsuarioId);

            if (yaExiste)
            {
                // Construimos ValidationProblemDetails con la estructura esperada por el cliente
                var vpd = new ValidationProblemDetails(new Dictionary<string, string[]>
                {
                    [nameof(TipoCuentaCreacionDTO.Nombre)] = new[]
                    {
                        "Ya existe un tipo de cuenta con ese nombre para este usuario."
                    }
                })
                {
                    Title = "Conflicto al crear el recurso",
                    Status = StatusCodes.Status409Conflict,
                    Instance = HttpContext?.Request?.Path
                };

                // 409 con application/problem+json y propiedad 'errors'
                return new ObjectResult(vpd) { StatusCode = StatusCodes.Status409Conflict };
            }

            var tipoCuenta = mapper.Map<TipoCuenta>(t);
            await servicioTipoCuenta.NewTipoCuenta(tipoCuenta);
            return CreatedAtRoute("ObtenerTipoCuentaPorId", new { id = tipoCuenta.Id }, tipoCuenta);
        }

        [HttpPut]
        public async Task<ActionResult> put([FromBody] TipoCuentaCreacionDTO t)
        {
            var tipocuenta =  await servicioTipoCuenta.getTipoCuentaById(t.Id);
            if (tipocuenta is null) return NotFound();

            tipocuenta.Nombre = t.Nombre;
            tipocuenta.Orden = t.Orden;

            await servicioTipoCuenta.SetTipoCuenta(tipocuenta);
            return CreatedAtRoute("ObtenerTipoCuentaPorId", new { id = tipocuenta.Id}, tipocuenta);
        }

        [HttpDelete]
        public async Task<ActionResult> delete(int id)
        {
            var tipoAux = await servicioTipoCuenta.getTipoCuentaById(id);
            if (tipoAux is null) return NotFound();

            await servicioTipoCuenta.DeleteTipoCuenta(tipoAux.Id);
            return Ok();
        }

    }
}
