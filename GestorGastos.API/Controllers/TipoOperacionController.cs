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
    public class TipoOperacionController : ControllerBase
    {
        private readonly IServicioTipoOperacion _servicioTipoOperacion;
        private readonly IMapper _mapper;

        public TipoOperacionController( IServicioTipoOperacion so, IMapper map)
        {
            this._servicioTipoOperacion = so;
            this._mapper = map;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<TipoOperacionDTO>>> Get([FromBody] PaginacionDTO paginacion)
        {
            var resultado = await _servicioTipoOperacion.getAllTiposOperacionPaginado(paginacion);
            var dtos = _mapper.Map<List<TipoOperacionDTO>>(resultado.Items);

            var response = new PagedResponse<TipoOperacionDTO>(resultado.Total, dtos);

            await HttpContext.InsertarParametrosPaginacionEnCabecera(resultado.Total, paginacion.Pagina, paginacion.recordsporpagina);

            return Ok(response);
        }

        [HttpGet("getAllTiposOperaciones")]
        public async Task<ActionResult<List<TipoOperacionDTO>>> Get()
        {
            var to = await _servicioTipoOperacion.getAll();
            var dtos = _mapper.Map<List<TipoOperacionDTO>>(to);

            return Ok(dtos);
        }

        [HttpGet("{id:int}", Name = "ObtenerTipoOperacionPorId")]
        public async Task<ActionResult<TipoOperacionDTO>> Get(int id)
        {
            var to = await _servicioTipoOperacion.getTipoOperacionById(id);
            if (to is null) return NotFound("Tipo operacion no encotrada");

            var dto = _mapper.Map<TipoOperacionDTO>(to);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> post([FromBody] TipoOperacionCreacionDTO t)
        {
            var to = _mapper.Map<TipoOperacion>(t);

            await _servicioTipoOperacion.NewTipoOperacion(to);

            return CreatedAtRoute("ObtenerTipoOperacionPorId", new { id = to.Id}, to);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> put(int id, [FromBody] TipoOperacionCreacionDTO t)
        {
            var to = await _servicioTipoOperacion.getTipoOperacionById(id);
            if (to is null) return NotFound("El tipo operacion que desea modificar no existe");

            to.Descripcion = t.Descripcion;

            await _servicioTipoOperacion.SetTipoOperacion(to);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> delete(int id)
        {
            var to = await _servicioTipoOperacion.getTipoOperacionById(id);
            if (to is null) return NotFound("el tipo operacion que desea eliminar no existe");

            await _servicioTipoOperacion.DeleteTipoOperacion(id);

            return NoContent();
        }
    }
}
