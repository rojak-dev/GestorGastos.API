using AutoMapper;
using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Servicios;
using GestorGastos.API.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorGastos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IServicioCuenta ServicioCuenta;
        private readonly IMapper Mapper;

        public CuentaController(IConfiguration configuration, IServicioCuenta servicioCuenta, IMapper mapper)
        {
            this.Configuration = configuration;
            this.ServicioCuenta = servicioCuenta;
            this.Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<CuentaDTO>>> Get([FromQuery] PaginacionDTO paginacion)
        {
            var resultado = await ServicioCuenta.getAllCuentasPaginado(paginacion);
            var dtos = Mapper.Map<List<CuentaDTO>>(resultado.Items);

            var response = new PagedResponse<CuentaDTO>(resultado.Total, dtos);

            await HttpContext.InsertarParametrosPaginacionEnCabecera(resultado.Total, paginacion.Pagina, paginacion.recordsporpagina);

            return Ok(response);
        }

        [HttpGet("getAllCuentas")]
        public async Task<ActionResult<List<CuentaDTO>>> Get()
        {
            var cuenta = await ServicioCuenta.getAll();
            var dtos = Mapper.Map<List<CuentaDTO>>(cuenta);

            return Ok(dtos);
        }

        [HttpGet("{id:int}", Name = "ObtenerCuentaPorId")]
        public async Task<ActionResult<CuentaDTO>> Get(int id)
        {
            var cuenta = await ServicioCuenta.getCuentaById(id);

            if (cuenta is null)
            {
                return NotFound("La cuenta no fue encontrada");
            }
            var dto = Mapper.Map<CuentaDTO>(cuenta);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> post([FromBody] CuentaCreacionDTO ct)
        {
            var cuenta = Mapper.Map<Cuenta>(ct);

            await ServicioCuenta.NewCuenta(cuenta);

            return CreatedAtRoute("ObtenerCuentaPorId", new { id = cuenta.Id}, cuenta);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> put(int id, [FromBody] CuentaCreacionDTO dto)
        {
            var cuenta = await ServicioCuenta.getCuentaById(id);
            if (cuenta is null) return NotFound("La cuenta que desea modificar no existe");

            cuenta.Nombre = dto.Nombre;
            cuenta.TipoCuentaId = dto.TipoCuentaId;
            cuenta.Balance = dto.Balance;
            cuenta.Descripcion = dto.Descripcion;

            await ServicioCuenta.SetCuenta(cuenta);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> delete(int id)
        {
            var cuenta = await ServicioCuenta.getCuentaById(id);
            if (cuenta is null) return NotFound("La cuenta que desea eliminar no existe");

            await ServicioCuenta.DeleteCuenta(id);

            return NoContent();
        }
    }
}
