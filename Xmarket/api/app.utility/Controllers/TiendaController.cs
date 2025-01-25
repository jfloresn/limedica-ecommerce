using System;
using System.Reflection;
using System.Web.Http;
using log4net;
using Web.Xmarket.model;

public class TiendaController : ApiController
{
    private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    [HttpPost]
    [Route("api/tienda/suggestion")]
    public IHttpActionResult GetSuggestion([FromBody] SuggestRequest request)
    {
        log.Info("Solicitud recibida en GetSuggestion.");

        if (request == null)
        {
            log.Warn("El request es nulo.");
            return BadRequest("El request no puede ser nulo.");
        }

        try
        {
            var response = new { Mensaje = "Respuesta exitosa" };
            log.Debug($"Respuesta generada: {response}");
            return Ok(response);
        }
        catch (Exception ex)
        {
            log.Error("Error en GetSuggestion", ex);
            return InternalServerError();
        }
    }
}
