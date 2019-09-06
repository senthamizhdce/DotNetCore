using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCore.Controllers
{
    //https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-2
    //<PackageReference Include = "NLog.Web.AspNetCore" Version="4.8.0" />
    //<PackageReference Include = "NLog" Version="4.5.11" />

    //For ClearProviders / SetMinimumLevel
    //using NLog.Web;
    //using Microsoft.Extensions.Logging;

    //The Logging configuration specified in appsettings.json overrides any call to SetMinimumLevel. So either remove "Default": or adjust it correctly to your needs.

    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger<LoggingController> _logger;

        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Log")]
        public ActionResult<dynamic> Log()
        {
            _logger.LogTrace("Index page says hello");
            _logger.LogInformation("Index page says hello");
            _logger.LogWarning("Index page says hello");
            _logger.LogDebug("Index page says hello");
            _logger.LogError("Index page says hello");
            _logger.LogCritical("Index page says hello");

            return new { MyName = "Senthamizh" };
        }
    }
}