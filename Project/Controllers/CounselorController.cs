using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Project.Services.Services;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounselorController : ControllerBase
    {
        private readonly IUserService<CounselorDto> service;
        private readonly IUserService<UserDto> userService;
        private readonly IRegistrationService registrationService;

        public CounselorController(IUserService<CounselorDto> service, IUserService<UserDto> userService, IRegistrationService registrationService)
        {
            this.service = service;
            this.userService = userService;
            this.registrationService = registrationService;
        }

        [HttpGet("{id}")]
        public async Task<CounselorDto> GetById(int id)
        {
            return await service.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<List<CounselorDto>> GetAll()
        {
            Console.WriteLine("בתוך גטאול של יועצים");
            return await service.GetAllAsync();
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromForm] CounselorDto entity)
        {
            return await registrationService.RegisterCounselorAsync(
                entity,
                service,
                userService,
                service.AddAsync
            );
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<CounselorDto> Put(CounselorDto counselor)
        {
            await service.UpdateAsync(counselor);
            return counselor;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteByIdAsync(id);
        }
    }
}
