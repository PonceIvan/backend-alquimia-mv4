using System.Net;
using System.Text.Json;

namespace backendAlquimia.Middlewares  // Ajustá el namespace según tu estructura
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Continua con la siguiente parte del pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se produjo una excepción no controlada.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode;
            string mensaje;

            switch (exception)
            {
                case ArgumentNullException:
                    statusCode = (int)HttpStatusCode.BadRequest; // 400
                    mensaje = "Faltan datos requeridos en la solicitud.";
                    break;

                case ArgumentException:
                    statusCode = (int)HttpStatusCode.BadRequest; // 400
                    mensaje = "Parámetros inválidos en la solicitud.";
                    break;

                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized; // 401
                    mensaje = "Acceso no autorizado.";
                    break;

                case KeyNotFoundException:
                case NullReferenceException:
                    statusCode = (int)HttpStatusCode.NotFound; // 404
                    mensaje = "El recurso solicitado no fue encontrado.";
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError; // 500
                    mensaje = "Error interno del servidor.";
                    break;
            }

            var response = new
            {
                status = statusCode,
                error = mensaje
            };

            var json = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(json);
        }
    }
}
