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
    public class CategoriaController : ControllerBase
    {
        private readonly IServicioCategoria _servicioCategoria;
        private readonly IMapper _mapper;

        public CategoriaController(IServicioCategoria servicioCategoria, IMapper mapper)
        {
            this._servicioCategoria = servicioCategoria;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<CategoriaDTO>>> Get([FromBody] PaginacionDTO paginacion)
        {
            var resultado = await _servicioCategoria.getAllCategoriaPaginado(paginacion);
            var dtos = _mapper.Map<List<CategoriaDTO>>(resultado.Items);

            var response = new PagedResponse<CategoriaDTO>(resultado.Total, dtos);

            await HttpContext.InsertarParametrosPaginacionEnCabecera(resultado.Total, paginacion.Pagina, paginacion.recordsporpagina);

            return Ok(response);
        }

        [HttpGet("getAllCategorias")]
        public async Task<ActionResult<List<CategoriaDTO>>> Get()
        {
            var categorias = await _servicioCategoria.getAll();
            var dtos = _mapper.Map<List<CategoriaDTO>>(categorias);

            return Ok(dtos);
        }

        [HttpGet("{id:int}", Name = "ObtenerCategoriaPorId")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoria = await _servicioCategoria.getCategoriaById(id);
            if (categoria is null) return NotFound("Categoria no encontrada");

            var dto = _mapper.Map<CategoriaDTO>(categoria);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> post([FromBody] CategoriaCreacionDTO c)
        {
            var categoria = _mapper.Map<Categoria>(c);

            await _servicioCategoria.NewCategoria(categoria);

            return CreatedAtRoute("ObtenerCategoriaPorId", new { id = categoria.Id}, categoria);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> put(int id, [FromBody] CategoriaCreacionDTO c)
        {
            var categoria = await _servicioCategoria.getCategoriaById(id);
            if (categoria is null) return NotFound("La categoria que desea editar no existe");

            categoria.Nombre = c.Nombre;
            categoria.TipoOperacionId = c.TipoOperacionId;
            categoria.UsuarioId = c.UsuarioId;

            await _servicioCategoria.SetCategoria(categoria);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> delete(int id)
        {
            var categoria = await _servicioCategoria.getCategoriaById(id);
            if (categoria is null) return NotFound("La cetgoria que desea eliminar no existe");

            await _servicioCategoria.DeleteCategoria(id);

            return NoContent();
        }
    }
}
