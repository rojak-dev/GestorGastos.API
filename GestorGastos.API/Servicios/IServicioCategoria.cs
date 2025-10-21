using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Utilidades;

namespace GestorGastos.API.Servicios
{
    public interface IServicioCategoria
    {
        public Task<PagedResponse<Categoria>> getAllCategoriaPaginado(PaginacionDTO paginacion);
        public Task<IEnumerable<Categoria>> getAll();
        public Task<Categoria> getCategoriaById(int id);
        public Task NewCategoria(Categoria c);
        public Task SetCategoria(Categoria c);
        public Task DeleteCategoria(int id);
    }
}
