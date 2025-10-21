using Dapper;
using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
using GestorGastos.API.Utilidades;
using Microsoft.Data.SqlClient;

namespace GestorGastos.API.Servicios
{
    public class ServicioCuenta : IServicioCuenta
    {
        private readonly string CadenaConexion;
        private readonly ILogger<ServicioCuenta> Log;

        public ServicioCuenta(ConexionBaseDatos con, ILogger<ServicioCuenta> log)
        {
            this.CadenaConexion = con.CadenaConexionSQL;
            this.Log = log;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(this.CadenaConexion);
        }

        public async Task DeleteCuenta(int id)
        {
            SqlConnection sqlConnection = conexion();

            try
            {
                sqlConnection.Open();

                var param = new DynamicParameters();
                param.Add("@Id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                await sqlConnection.ExecuteScalarAsync("CuentaEliminar", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al borrar la cuenta " + ex.Message);
            }
            finally { 
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public Task<bool> Existe(string nombre, int usuarioId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Cuenta>> getAll()
        {
            SqlConnection sqlcon = conexion();

            try
            {
                sqlcon.Open();

                var listado = await sqlcon.QueryAsync<Cuenta>("CuentaObtenerAll", commandType: System.Data.CommandType.StoredProcedure);
                return listado;
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al consultar las cuentas " + ex.Message);
            }
            finally
            {
                sqlcon.Close();
                sqlcon.Dispose();
            }
        }

        public async Task<PagedResponse<Cuenta>> getAllCuentasPaginado(PaginacionDTO paginacion)
        {
            SqlConnection sqlConnection = conexion();

            try
            {
                sqlConnection.Open();

                var param = new DynamicParameters();
                param.Add("Page", paginacion.Pagina, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("PageSize", paginacion.recordsporpagina, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("TotalCount", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                var items = (await sqlConnection.QueryAsync<Cuenta>("CuentaObtener", param, commandType: System.Data.CommandType.StoredProcedure)).AsList();
                var total = param.Get<int>("TotalCount");

                return new PagedResponse<Cuenta>(total, items);
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al consultar las cuentas " + ex.Message);
            }
            finally 
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public async Task<Cuenta> getCuentaById(int id)
        {
            SqlConnection con = conexion();
            Cuenta c = null;

            try
            {
                con.Open();

                var param = new DynamicParameters();
                param.Add("@Id", id, System.Data.DbType.UInt32, System.Data.ParameterDirection.Input);

                c = await con.QueryFirstAsync<Cuenta>("CuentaPorId", param, commandType: System.Data.CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al buscar la cuenta " + ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return c;
        }

        public async Task NewCuenta(Cuenta t)
        {
            SqlConnection con = conexion();

            try
            {
                con.Open();

                var param = new DynamicParameters();
                param.Add("@Nombre", t.Nombre, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("@TipoCuentaId", t.TipoCuentaId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@Balance", t.Balance, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
                param.Add("@Descripcion", t.Descripcion, System.Data.DbType.String, System.Data.ParameterDirection.Input);

                await con.ExecuteScalarAsync("CuentaAlta", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al dar de alta la cuenta " + ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        public async Task SetCuenta(Cuenta t)
        {
            SqlConnection con = conexion();

            try
            {
                con.Open();

                var param = new DynamicParameters();
                param.Add("@Nombre", t.Nombre, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("@TipoCuentaId", t.TipoCuentaId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("@Balance", t.Balance, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
                param.Add("@Descripcion", t.Descripcion, System.Data.DbType.String, System.Data.ParameterDirection.Input);

                await con.ExecuteScalarAsync("CuentaModificar", param, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al modificar la cuenta " + ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
    }
}
