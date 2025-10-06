using Dapper;
using GestorGastos.API.DTO;
using GestorGastos.API.Entidades;
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
        public async Task BajaTipoCuenta(int id)
        {
            SqlConnection sqlConection = conexion();
            try
            {
                sqlConection.Open();

                //Se añaden los parametros
                var param = new DynamicParameters();
                param.Add("@Id", id, DbType.Int32, ParameterDirection.Input, 50);


                await sqlConection.ExecuteScalarAsync("sp_name", param, commandType: CommandType.StoredProcedure); //ejecutamos el SP
           
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

        public async Task<IEnumerable<TipoCuenta>> getAllTiposCeuntas()
        {
            SqlConnection sqlConection = conexion();
            List<TipoCuenta> tiposcuentas = new List<TipoCuenta>();
            try
            {
                sqlConection.Open();



               var r = await sqlConection.QueryAsync<TipoCuenta>("sp_name", commandType: CommandType.StoredProcedure); //ejecutamos el SP
               return r;
            }
            catch (Exception ex)
            {
                Log.LogError("ERROR: "+ex.ToString());
                throw new Exception("Se produjo un erro al consultar los tipos cuentas" + ex.Message);
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


                tipoc = await sqlConection.QueryFirstOrDefaultAsync<TipoCuenta>("sp_name", param, commandType: CommandType.StoredProcedure); //ejecutamos el SP
                return tipoc;
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
        }

        public async Task ModificarTipoCuenta(TipoCuenta t)
        {
            SqlConnection sqlConection = conexion();
            try
            {
                sqlConection.Open();

                //Se añaden los parametros
                var param = new DynamicParameters();
                param.Add("@Nombre", t.Nombre, DbType.String, ParameterDirection.Input, 50);


                await sqlConection.ExecuteScalarAsync("sp_name", param, commandType: CommandType.StoredProcedure); //ejecutamos el SP
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

        public async Task NuevoTipoCuenta(TipoCuenta t)
        {
            SqlConnection sqlConection = conexion();
            try
            {
                sqlConection.Open();

                //Se añaden los parametros
                var param = new DynamicParameters();
                param.Add("@Nombre", t.Nombre, DbType.String, ParameterDirection.Input, 50);


                await sqlConection.ExecuteScalarAsync("sp_name", param, commandType: CommandType.StoredProcedure); //ejecutamos el SP
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
    }
}
