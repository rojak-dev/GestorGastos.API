namespace GestorGastos.API.Utilidades
{
    public static class HttpContextExtensions
    {
        //iqueryable es un tipo al que se le pueden hacer querys
        public async static Task InsertarParametrosPaginacionEnCabecera(this HttpContext httpContext, int totalRegistros, int pagina, int registrosPorPagina)
        {
            if(registrosPorPagina  <= 0) registrosPorPagina = 10;

            var totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);

            httpContext.Response.Headers["X-Total-Count"] = totalRegistros.ToString();
            httpContext.Response.Headers["X-Page"] = pagina.ToString();
            httpContext.Response.Headers["X-Page-Size"] = registrosPorPagina.ToString();
            httpContext.Response.Headers["X-Total-Pages"] = totalPaginas.ToString();

        }
    }
}
