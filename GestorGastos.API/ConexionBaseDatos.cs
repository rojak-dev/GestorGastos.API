namespace GestorGastos.API
{
    public class ConexionBaseDatos
    {
        private string cadenaConexionSQL;

        public string CadenaConexionSQL { get => cadenaConexionSQL; }

        public ConexionBaseDatos(string ConexionSQL)
        {
            cadenaConexionSQL = ConexionSQL;
        }
    }
}
