using alquimia.Services.Models;
using System.Net;
using System.Text.Json;

namespace alquimia.Api.Middlewares
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
            int status;
            string error;

            switch (exception)
            {
                case ArgumentNullException:
                    status = (int)HttpStatusCode.BadRequest; // 400
                    error = "Faltan datos requeridos en la solicitud.";
                    break;

                case ArgumentException:
                    status = (int)HttpStatusCode.BadRequest; // 400
                    error = "Parámetros inválidos en la solicitud.";
                    break;

                case UnauthorizedAccessException:
                    status = (int)HttpStatusCode.Unauthorized; // 401
                    error = "Acceso no autorizado.";
                    break;

                case KeyNotFoundException:
                case NullReferenceException:
                    status = (int)HttpStatusCode.NotFound; // 404
                    error = "El recurso solicitado no fue encontrado.";
                    break;

                default:
                    status = (int)HttpStatusCode.InternalServerError; // 500
                    error = "Error interno del servidor.";
                    break;
            }

            var response = new ErrorResponseDTO
            {
                Status = status,
                Error = error
            };

            var json = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;
            return context.Response.WriteAsync(json);
        }
    }
}
