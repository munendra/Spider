using System;
using Microsoft.AspNetCore.Mvc;
using Spider.Application.Applications.Commands.Model;
using Spider.Service.Application;
using System.Threading.Tasks;

namespace Spider.Api.Controllers
{
    [ApiController]
    [Route("api/application")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateApplicationDto createApplication)
        {
            var savedApplication = await _applicationService.Create(createApplication);
            return Ok(savedApplication);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var applications = await _applicationService.GetAll();
            return Ok(applications);
        }

        [HttpGet]
        [Route("{applicationId}")]
        public async Task<IActionResult> GetApplication(Guid applicationId)
        {
            var application = await _applicationService.GetApplicationDetail(applicationId);
            return Ok(application);
        }

        [HttpDelete]
        [Route("{applicationId}")]
        public async Task<IActionResult> DeleteApplication(Guid applicationId)
        {
            await _applicationService.DeleteApplication(applicationId);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateApplication(UpdateApplicationDto application)
        {
            var updatedApplication = await _applicationService.UpdateApplication(application);
            return Ok(updatedApplication);
        }
    }
}