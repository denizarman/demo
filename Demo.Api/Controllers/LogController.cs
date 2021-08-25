using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;

namespace Demo.Api.Controllers
{
    [ApiController]
    [Route("log")]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpGet("random")]
        public ActionResult<int> GetRandomNumber()
        {
            var random = new Random();
            var randomValue = random.Next(0, 100);
            _logger.LogInformation($"Random Value is {randomValue}");
            return Ok(randomValue);
        }

        [HttpGet("logmessage")]
        public ActionResult LogMessage(int id)
        {
            try
            {
                if (id < 0)
                    throw new Exception($"id cannot be less than or equal to o. value passed is {id}");
                else if (id == 0)
                    _logger.LogWarning($"id cannot be 0");
                else
                    _logger.LogInformation($"id is {0}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return Ok();
        }
    }
}
