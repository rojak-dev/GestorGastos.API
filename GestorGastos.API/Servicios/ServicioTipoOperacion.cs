using Dapper;
using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Utilidades;
using Microsoft.Data.SqlClient;

namespace GestorGastos.API.Servicios
{
    public class ServicioTipoOperacion : IServicioTipoOperacion
    {
        private readonly string _cadenaConexion;
        private readonly ILogger<ServicioTipoOperacion> _log;

        public ServicioTipoOperacion(ConexionBaseDatos cn, ILogger<ServicioTipoOperacion> log)
        {
            this._cadenaConexion = cn.CadenaConexionSQL;
            this._log = log;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(this._cadenaConexion);
        }

        public async Task DeleteTipoOperacion(int id)
        {
            SqlConnection con = conexion();

            try
            {
                con.Open();

                var param = new DynamicParameters();
                param.Add("@Id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                await con.ExecuteScalarAsync("TipoOperacionEliminar", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Error al eliminar el tipo Operación");
            }
            finally 
            {
                con.Close();
                con.Dispose();
            }
        }

        public async Task<IEnumerable<TipoOperacion>> getAll()
        {
            SqlConnection cnn = conexion();
            try
            {
                cnn.Open();

                var listado = await cnn.QueryAsync<TipoOperacion>("TipoOperacionObtenerAll", commandType: System.Data.CommandType.StoredProcedure);

                return listado;
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo un error al obtener los tipos operación");
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        public async Task<PagedResponse<TipoOperacion>> getAllTiposOperacionPaginado(PaginacionDTO paginacion)
        {
            SqlConnection cnn = conexion();

            try
            {
                cnn.Open();

                var param = new DynamicParameters();
                param.Add("Page", paginacion.Pagina, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("PageSize", paginacion.RecordsPorPagina, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("TotalCount", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                var items = (await cnn.QueryAsync<TipoOperacion>("TipoOperacionObtener", param, commandType: System.Data.CommandType.StoredProcedure)).AsList();
                var total = param.Get<int>("TotalCount");

                return new PagedResponse<TipoOperacion>(total, items);
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo en erro al obtener los tipos operación paginado");
            }
            finally
            {
                cnn.Open();
                cnn.Dispose();
            }
        }

        public async Task<TipoOperacion> getTipoOperacionById(int id)
        {
            SqlConnection cnn = conexion();
            TipoOperacion to = null;
            try
            {
                cnn.Open();

                var param = new DynamicParameters();
                param.Add("@Id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                to = await cnn.QueryFirstOrDefaultAsync<TipoOperacion>("TipoOperacionPorId", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo un erro al buscar el tipo operacion por id "+ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }

            return to;
        }

        public async Task NewTipoOperacion(TipoOperacion t)
        {
            SqlConnection cnn = conexion();

            try
            {
                cnn.Open();

                var param = new DynamicParameters();
                param.Add("@Descripcion", t.Descripcion, System.Data.DbType.String, System.Data.ParameterDirection.Input);

                await cnn.ExecuteScalarAsync("TipoOperacionAlta", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo un error al crear el tipo operacion "+ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        public async Task SetTipoOperacion(TipoOperacion t)
        {
            SqlConnection cnn = conexion();

            try
            {
                cnn.Open();

                var param = new DynamicParameters();
                param.Add("@Id", t.Id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@Descripcion", t.Descripcion, System.Data.DbType.String, System.Data.ParameterDirection.Input);

                await cnn.ExecuteScalarAsync("TipoOperacion", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo un error al modificar el tipo operacion "+ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }
    }
}
