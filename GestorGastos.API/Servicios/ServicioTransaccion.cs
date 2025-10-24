using Dapper;
using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Utilidades;
using Microsoft.Data.SqlClient;

namespace GestorGastos.API.Servicios
{
    public class ServicioTransaccion : IServicioTransaccion
    {
        private readonly string _cadenaConexion;
        private readonly ILogger<ServicioTransaccion> _log;

        public ServicioTransaccion(ConexionBaseDatos con, ILogger<ServicioTransaccion> log)
        {
            _cadenaConexion = con.CadenaConexionSQL;
            _log = log;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(_cadenaConexion);
        }

        public async Task DeleteTransaccion(int id)
        {
            SqlConnection cnn = conexion();

            try
            {
                cnn.Open();

                var param = new DynamicParameters();
                param.Add("@Id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                await cnn.ExecuteScalarAsync("TransaccionEliminar", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Error al borrar la transaccion "+ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        public async Task<IEnumerable<Transaccion>> getAll()
        {
            SqlConnection cnn = conexion();
            try
            {
                cnn.Open();

                var list = await cnn.QueryAsync<Transaccion>("TransaccionObtenerAll", commandType: System.Data.CommandType.StoredProcedure);
                return list;
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo un erro al obtener el listado de transacciones "+ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        public async Task<PagedResponse<Transaccion>> getAllTransaccionesPaginado(PaginacionDTO paginacion)
        {
            SqlConnection cnn = conexion();

            try
            {
                cnn.Open();

                var param = new DynamicParameters();
                param.Add("Page", paginacion.Pagina, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("PageSize", paginacion.recordsporpagina, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("TotalCount", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                var items = (await cnn.QueryAsync<Transaccion>("TransaccionObtener", param, commandType: System.Data.CommandType.StoredProcedure)).AsList();
                var total = param.Get<int>("TotalCount");

                return new PagedResponse<Transaccion>(total, items);
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al obtener las transacciones paginadas "+ex.Message);
            }
            finally
            {
                cnn.Open();
                cnn.Close();
            }
        }

        public async Task<Transaccion> getTransaccionById(int id)
        {
            SqlConnection cnn = conexion();

            try
            {
                cnn.Open();

                var param = new DynamicParameters();
                param.Add("@Id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                var t = await cnn.QueryFirstOrDefaultAsync<Transaccion>("TransaccionPorId", param, commandType: System.Data.CommandType.StoredProcedure);
                return t;
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo un erro al obtener la transaccion "+ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        public async Task NewTransaccion(Transaccion t)
        {
            SqlConnection cnn = conexion();

            try
            {
                cnn.Open();

                var param = new DynamicParameters();
                param.Add("@UsuarioId", t.UsuarioId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@FechaTransaccion", t.FechaTransaccion, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
                param.Add("@Monto", t.Monto, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
                param.Add("@TipoOperacionId", t.TipoOperacionId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@Nota", t.Nota, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("@CuentaId", t.CuentaId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@CategoriaId", t.CategoriaId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                await cnn.ExecuteScalarAsync("TransaccionAlta", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo un error al crear la transacción "+ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        public async Task SetTransaccion(Transaccion t)
        {
            SqlConnection cnn = conexion();

            try
            {
                cnn.Open();

                var param = new DynamicParameters();
                param.Add("@Id", t.Id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@UsuarioId", t.UsuarioId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@FechaTransaccion", t.FechaTransaccion, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
                param.Add("@Monto", t.Monto, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
                param.Add("@TipoOperacionId", t.TipoOperacionId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@Nota", t.Nota, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("@CuentaId", t.CuentaId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@CategoriaId", t.CategoriaId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                await cnn.ExecuteScalarAsync("TransaccionModificar", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo un error al editar la transacción "+ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }
    }
}
