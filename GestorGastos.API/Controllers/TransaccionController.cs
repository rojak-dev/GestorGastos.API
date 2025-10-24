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
    public class TransaccionController : ControllerBase
    {
        private readonly IServicioTransaccion _servicioTransaccion;
        private readonly IMapper _mapper;

        public TransaccionController(IServicioTransaccion ser, IMapper map)
        {
            this._servicioTransaccion = ser;
            this._mapper = map;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<TransaccionDTO>>> Get([FromBody] PaginacionDTO paginacion)
        {
            var resultado = await _servicioTransaccion.getAllTransaccionesPaginado(paginacion);
            var dtos = _mapper.Map<List<TransaccionDTO>>(resultado.Items);

            var response = new PagedResponse<TransaccionDTO>(resultado.Total, dtos);

            await HttpContext.InsertarParametrosPaginacionEnCabecera(resultado.Total, paginacion.Pagina, paginacion.recordsporpagina);

            return Ok(dtos);
        }

        [HttpGet("getAllTransacciones")]
        public async Task<ActionResult<List<TransaccionDTO>>> Get()
        {
            var listTran = await _servicioTransaccion.getAll();
            var listDTOs = _mapper.Map<List<TransaccionDTO>>(listTran);

            return Ok(listDTOs);
        }

        [HttpGet("{id:int}", Name = "ObtenerTransaccionPorId")]
        public async Task<ActionResult<TransaccionDTO>> Get(int id)
        {
            var t = await _servicioTransaccion.getTransaccionById(id);
            if (t is null) return NotFound("Transaccion no encontrada");

            var dto = _mapper.Map<TransaccionDTO>(t);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TransaccionCreacionDTO t)
        {
            var transaccion = _mapper.Map<Transaccion>(t);

            await _servicioTransaccion.NewTransaccion(transaccion);

            return CreatedAtRoute("ObtenerTransaccionPorId", new { id = transaccion.Id }, transaccion);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] TransaccionCreacionDTO dto)
        {
            var transaccion = await _servicioTransaccion.getTransaccionById(id);
            if (transaccion is null) return NotFound("La transaccion que desea editar no existe");

            transaccion.UsuarioId = dto.UsuarioId;
            transaccion.FechaTransaccion = dto.FechaTransaccion;
            transaccion.Monto = dto.Monto;
            transaccion.TipoOperacionId = dto.TipoOperacionId;
            transaccion.Nota = dto.Nota;
            transaccion.CuentaId = dto.CuentaId;
            transaccion.CategoriaId = dto.CategoriaId;

            await _servicioTransaccion.SetTransaccion(transaccion);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transaccion = await _servicioTransaccion.getTransaccionById(id);
            if (transaccion is null) return NotFound("La transaccion que desea eleminar no existe");

            await _servicioTransaccion.DeleteTransaccion(id);

            return NoContent();
        }
    }
}
