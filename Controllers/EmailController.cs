using Evaluation.Portal.API.Helpers;
using Evaluation.Portal.API.Models;
using Evaluation.Portal.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Evaluation.Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService mailService;
        public EmailController(IEmailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("notify")]
        public async Task<IActionResult> SendMail([FromBody] EmailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost("welcome")]
        public async Task<IActionResult> SendWelcomeEmailAsync([FromForm] WelcomeRequest request)
        {
            try
            {
                CryptoEngine.Encrypt("Srikanth", "sblw-s528-sqy538");
                await mailService.SendWelcomeEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
