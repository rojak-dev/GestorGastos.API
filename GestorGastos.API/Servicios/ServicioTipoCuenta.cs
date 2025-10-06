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

        public ServicioTipoCuenta(ConexionBaseDatos con)
        {
            CadenaConexion = con.CadenaConexionSQL;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }
        public void BajaTipoCuenta(int id)
        {
            SqlConnection sqlConection = conexion();
            try
            {
                sqlConection.Open();

                //Se añaden los parametros
                var param = new DynamicParameters();
                param.Add("@Id", id, DbType.Int32, ParameterDirection.Input, 50);


                sqlConection.ExecuteScalar("sp_name", param, commandType: CommandType.StoredProcedure); //ejecutamos el SP
           
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al borrar el tipo cuenta" + ex.Message);
            }
            finally
            {
                sqlConection.Close();
                sqlConection.Dispose();
            }
        }

        public IEnumerable<TipoCuenta> getAllTiposCeuntas()
        {
            SqlConnection sqlConection = conexion();
            List<TipoCuenta> tiposcuentas = new List<TipoCuenta>();
            try
            {
                sqlConection.Open();



               var r = sqlConection.Query<TipoCuenta>("sp_name", commandType: CommandType.StoredProcedure); //ejecutamos el SP
               return r;
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un erro al consultar los tipos cuentas" + ex.Message);
            }
            finally
            {
                sqlConection.Close();
                sqlConection.Dispose();
            }
        }

        public TipoCuenta getTipoCuentaById(int id)
        {
            SqlConnection sqlConection = conexion();
            TipoCuenta tipoc = null;
            try
            {
                sqlConection.Open();

                //Se añaden los parametros
                var param = new DynamicParameters();
                param.Add("@Id", id, DbType.Int32, ParameterDirection.Input, 50);


                tipoc = sqlConection.QueryFirstOrDefault<TipoCuenta>("sp_name", param, commandType: CommandType.StoredProcedure); //ejecutamos el SP
                if (tipoc != null)
                    return tipoc;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al buscar el tipoCuenta" + ex.Message);
            }
            finally
            {
                sqlConection.Close();
                sqlConection.Dispose();
            }
        }

        public void ModificarTipoCuenta(TipoCuenta t)
        {
            SqlConnection sqlConection = conexion();
            try
            {
                sqlConection.Open();

                //Se añaden los parametros
                var param = new DynamicParameters();
                param.Add("@Nombre", t.Nombre, DbType.String, ParameterDirection.Input, 50);


                sqlConection.ExecuteScalar("sp_name", param, commandType: CommandType.StoredProcedure); //ejecutamos el SP
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al modificar tipo cuenta" + ex.Message);
            }
            finally
            {
                sqlConection.Close();
                sqlConection.Dispose();
            }
        }

        public void NuevoTipoCuenta(TipoCuenta t)
        {
            SqlConnection sqlConection = conexion();
            try
            {
                sqlConection.Open();

                //Se añaden los parametros
                var param = new DynamicParameters();
                param.Add("@Nombre", t.Nombre, DbType.String, ParameterDirection.Input, 50);


                sqlConection.ExecuteScalar("sp_name", param, commandType: CommandType.StoredProcedure); //ejecutamos el SP
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un erro al dar de alta" + ex.Message);
            }
            finally {
                sqlConection.Close();
                sqlConection.Dispose();
            }
        }
    }
}
