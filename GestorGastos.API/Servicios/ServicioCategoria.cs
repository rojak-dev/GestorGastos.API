using Dapper;
using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Utilidades;
using Microsoft.Data.SqlClient;

namespace GestorGastos.API.Servicios
{
    public class ServicioCategoria : IServicioCategoria
    {
        private readonly string cadenaConexion;
        private readonly ILogger<ServicioCategoria> Log;

        public ServicioCategoria(ConexionBaseDatos con, ILogger<ServicioCategoria> log)
        {
            this.cadenaConexion = con.CadenaConexionSQL;
            Log = log;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(this.cadenaConexion);
        }

        public async Task DeleteCategoria(int id)
        {
            SqlConnection conn = conexion();

            try
            {
                conn.Open();

                var param = new DynamicParameters();
                param.Add("@Id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                await conn.ExecuteScalarAsync("CategoriaEliminar", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al eliminar la categoria " + ex.Message);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<Categoria>> getAll()
        {
            SqlConnection conn = conexion();

            try
            {
                conn.Open();

                var list = await conn.QueryAsync<Categoria>("CategoriaObtenerAll", commandType: System.Data.CommandType.StoredProcedure);
                return list;
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al traer las categorias " + ex.Message);
            }
            finally 
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<PagedResponse<Categoria>> getAllCategoriaPaginado(PaginacionDTO paginacion)
        {
            SqlConnection conn = conexion();

            try
            {
                conn.Open();

                var param = new DynamicParameters();
                param.Add("Page", paginacion.Pagina, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("PageSize", paginacion.recordsporpagina, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("TotalCount", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                var items = (await conn.QueryAsync<Categoria>("CategoriaObtener", param, commandType: System.Data.CommandType.StoredProcedure)).AsList();
                var total = param.Get<int>("TotalCount");

                return new PagedResponse<Categoria>(total, items);
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Ocurrio un error al obtener las categorias Paginadas "+ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<Categoria> getCategoriaById(int id)
        {
            SqlConnection conn = conexion();
            Categoria c = null;
            try
            {
                conn.Open();

                var param = new DynamicParameters();
                param.Add("@Id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                c = await conn.QueryFirstAsync<Categoria>("CategoriaPorId", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo un error al obtener la categoria");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return c;
        }

        public async Task NewCategoria(Categoria c)
        {
            SqlConnection cnn = conexion();

            try
            {
                cnn.Open();

                var param = new DynamicParameters();
                param.Add("@Nombre", c.Nombre, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("@TipoOperacionId", c.TipoOperacionId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@UsuarioId", c.UsuarioId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                await cnn.ExecuteScalarAsync("CategoriaAlta", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al agregar una caterogira " + ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        public async Task SetCategoria(Categoria c)
        {
            SqlConnection cnn = conexion();

            try
            {
                cnn.Open();

                var param = new DynamicParameters();
                param.Add("@Id", c.Id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@Nombre", c.Nombre, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("@TipoOperacionId", c.TipoOperacionId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@UsuarioId", c.UsuarioId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                await cnn.ExecuteScalarAsync("CategoriaAlta", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al etidar la caterogira " + ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }
    }
}
