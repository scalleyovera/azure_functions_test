using DemoAzureFunctions.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DemoAzureFunctions.Functions
{
    public class HealthGreetingFunction
    {
        private readonly ILogger<HealthGreetingFunction> _logger;
        private readonly IGreeterService _greeterService;

        public HealthGreetingFunction(ILogger<HealthGreetingFunction> logger, IGreeterService greeterService)
        {
            _logger = logger;
            _greeterService = greeterService;
        }

        [Function("GetSaludador")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/greeting")] HttpRequest req)
        {
            _logger.LogInformation("Iniciando el saludo.");

            try
            {
                string name = req.Query["name"].FirstOrDefault() ?? "mundo";

                var message = _greeterService.GetGreeting(name);

                return new OkObjectResult(new
                {
                    success = true,
                    data = message,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la función.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}