using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ErrorHandling.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index(
            [FromServices] IHostEnvironment hostEnvironment)
        {
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context is null)
            {
                return Problem();
            }

            _logger.LogError(context.Error, context.Path, context.RouteValues);

            if (context.Error is not InvalidOperationException)
            {
                throw context.Error;
            }

            if (!hostEnvironment.IsDevelopment())
            {
                return Problem(title: context.Error.Message, detail: context.Error.StackTrace);
            }

            return Problem(title: context.Error.Message);
        }
    }
}
