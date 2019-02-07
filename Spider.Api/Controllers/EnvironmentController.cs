using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spider.Application.Environments.Commands.CreateEnvironment;
using Spider.Application.Environments.Commands.UpdateEnvironment;
using Spider.Service.Environment;

namespace Spider.Api.Controllers
{
    [Route("api/environment")]
    public class EnvironmentController : Controller
    {
        private readonly IEnvironmentService _environmentService;

        public EnvironmentController(IEnvironmentService environmentService)
        {
            _environmentService = environmentService;
        }

        [HttpGet("{applicationId}")]
        public async Task<IActionResult> GetEnvironments(Guid applicationId)
        {
            var environments = await _environmentService.GetAll(applicationId);
            return Ok(environments);
        }

        [HttpGet("environment/{environmentId}")]
        public async Task<IActionResult> Get(Guid environmentId)
        {
            var environment = await _environmentService.Get(environmentId);
            return Ok(environment);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEnvironmentDto environment)
        {
            await _environmentService.Create(environment);
            return Ok("Environment added successfully.");
        }

        [HttpDelete("{environmentId}")]
        public async Task<IActionResult> Delete(Guid environmentId)
        {
            await _environmentService.Delete(environmentId);
            return Ok();
        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody]UpdateEnvironmentDto environment)
        {
            await _environmentService.Update(environment);
            return Ok();
        }
    }
}