using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Services.Interfaces;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ContactController : ControllerBase
    {
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;

        public ContactController(IEmailService emailService, IConfiguration configuration)
        {
            this.emailService = emailService;
            this.configuration = configuration;
        }

        [Authorize]
        [HttpPost("counselor")]
        public async Task<IActionResult> ContactCounselor([FromBody] ContactCounselorRequestDto request)
        {
            await emailService.SendContactCounselorEmailAsync(request.CounselorName, request.CounselorEmail, request.UserName, request.UserEmail, request.Message);
            return Ok("Email sent to counselor.");
        }

        [Authorize]
        [HttpPost("general")]
        public async Task<IActionResult> ContactGeneral([FromBody] ContactRequestDto request)
        {
            string? siteManagerEmail = configuration["EmailSettings:SiteManagerEmail"];
            string? siteManagerName = configuration["EmailSettings:siteManagerName"];
            if (string.IsNullOrEmpty(siteManagerEmail))
            {
                throw new InvalidOperationException("Missing configuration: EmailSettings:SiteManagerEmail");
            }

            if (string.IsNullOrEmpty(siteManagerName))
            {
                throw new InvalidOperationException("Missing configuration: EmailSettings:siteManagerName");
            }

            await emailService.SendContactEmailAsync(siteManagerName, siteManagerEmail, request.UserName, request.UserEmail, request.Message);
            return Ok("Email sent to general contact.");
        }
    }
}
