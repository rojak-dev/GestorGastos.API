using Dapper;
using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Utilidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GestorGastos.API.Servicios
{
    public class ServicioTipoCuenta : IServicioTipoCuenta
    {
        private readonly string CadenaConexion;

        private readonly ILogger<ServicioTipoCuenta> Log;

        public ServicioTipoCuenta(ConexionBaseDatos con, ILogger<ServicioTipoCuenta> log)
        {
            CadenaConexion = con.CadenaConexionSQL;
            Log = log;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }
        public async Task DeleteTipoCuenta(int id)
        {
            SqlConnection sqlConection = conexion();
            try
            {
                sqlConection.Open();

                //Se añaden los parametros
                var param = new DynamicParameters();
                param.Add("@Id", id, DbType.Int32, ParameterDirection.Input, 50);


                await sqlConection.ExecuteScalarAsync("TipoCuentaEliminar", param, commandType: CommandType.StoredProcedure); //ejecutamos el SP
           
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo un error al borrar el tipo cuenta" + ex.Message);
            }
            finally
            {
                sqlConection.Close();
                sqlConection.Dispose();
            }
        }

        public async Task<PagedResponse<TipoCuenta>> getAllTiposCuentasPaginado(PaginacionDTO paginacion)
        {
            SqlConnection sqlConection = conexion();
            try
            {
                sqlConection.Open();
                var param = new DynamicParameters();
                param.Add("Page", paginacion.Pagina);
                param.Add("PageSize", paginacion.RecordsPorPagina);
                param.Add("TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);


                var items = (await sqlConection.QueryAsync<TipoCuenta>("TipoCuentaObtener", param ,commandType: CommandType.StoredProcedure)).AsList(); //ejecutamos el SP
                var total = param.Get<int>("TotalCount");


                
                return new PagedResponse<TipoCuenta>(total, items);
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo un error al consultar los tipos cuentas" + ex.Message);
            }
            finally
            {
                sqlConection.Close();
                sqlConection.Dispose();
            }
        }

        public async Task<IEnumerable<TipoCuenta>> getAll() {
            SqlConnection sqlConection = conexion();
            try
            {
                sqlConection.Open();
                 var listado = await sqlConection.QueryAsync<TipoCuenta>("TipoCuentaObtenerAll", commandType: CommandType.StoredProcedure);
                return listado;
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al consultar los tipos cuentas" + ex.Message);
            }
            finally
            {
                sqlConection.Close();
                sqlConection.Dispose();
            }
        }

        public async Task<TipoCuenta> getTipoCuentaById(int id)
        {
            SqlConnection sqlConection = conexion();
            TipoCuenta tipoc = null;
            try
            {
                sqlConection.Open();

                //Se añaden los parametros
                var param = new DynamicParameters();
                param.Add("@Id", id, DbType.Int32, ParameterDirection.Input, 50);


                tipoc = await sqlConection.QueryFirstOrDefaultAsync<TipoCuenta>("TipoCuentaPorId", param, commandType: CommandType.StoredProcedure); //ejecutamos el SP
                
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al buscar el tipoCuenta" + ex.Message);
            }
            finally
            {
                sqlConection.Close();
                sqlConection.Dispose();
            }

            return tipoc;
        }

        public async Task SetTipoCuenta(TipoCuenta t)
        {
            SqlConnection sqlConection = conexion();
            try
            {
                sqlConection.Open();

                //Se añaden los parametros
                var param = new DynamicParameters();
                param.Add("@Id", t.Id, DbType.Int32, ParameterDirection.Input);
                param.Add("@Nombre", t.Nombre, DbType.String, ParameterDirection.Input, 50);
                param.Add("@Orden", t.Orden, DbType.Int32, ParameterDirection.Input);


                await sqlConection.ExecuteScalarAsync("TipoCuentaModificar", param, commandType: CommandType.StoredProcedure); //ejecutamos el SP
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al modificar tipo cuenta" + ex.Message);
            }
            finally
            {
                sqlConection.Close();
                sqlConection.Dispose();
            }
        }

        public async Task NewTipoCuenta(TipoCuenta t)
        {
            SqlConnection sqlConection = conexion();
            try
            {
                sqlConection.Open();

                //Se añaden los parametros
                var param = new DynamicParameters();
                param.Add("@Nombre", t.Nombre, DbType.String, ParameterDirection.Input, 50);
                param.Add("@UsuarioId", t.UsuarioId, DbType.Int32, ParameterDirection.Input);
                param.Add("@Orden", t.Orden, DbType.Int32, ParameterDirection.Input);

                await sqlConection.ExecuteScalarAsync("TipoCuentaAlta", param, commandType: CommandType.StoredProcedure); //ejecutamos el SP
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un erro al dar de alta" + ex.Message);
            }
            finally {
                sqlConection.Close();
                sqlConection.Dispose();
            }
        }

        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            SqlConnection conn = conexion();
            try
            {
                conn.Open();

                var param = new DynamicParameters();
                param.Add("@Nombre", nombre, DbType.String, ParameterDirection.Input, 50);
                param.Add("@UsuarioId", usuarioId, DbType.Int32, ParameterDirection.Input);

                var temp = await conn.QueryFirstOrDefaultAsync<int>("TipoCuentaExiste", param, commandType: CommandType.StoredProcedure); //estructura del sp: select 1 from TiposCuentas where nombre = @Nombre and usuarioId = @Usiarioid

                return temp == 1;
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un erro en la consulta existe" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
