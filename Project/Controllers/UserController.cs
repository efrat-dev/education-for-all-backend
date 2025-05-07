using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Project.Services.Services;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService<UserDto> service;
        private readonly IUserService<CounselorDto> counselorService;
        private readonly IRegistrationService registrationService;

        public UserController(IUserService<UserDto> service, IUserService<CounselorDto> counselorService, IRegistrationService registrationService)
        {
            this.service = service;
            this.counselorService = counselorService;
            this.registrationService = registrationService;
        }

        [HttpGet("{id}")]
        public async Task<UserDto> Get(int id)
        {
            return await service.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<List<UserDto>> GetAll()
        {
            return await service.GetAllAsync();
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromForm] UserDto entity)
        {
            return await registrationService.RegisterUserAsync(
                entity,
                service,
                counselorService,
                service.AddAsync
            );
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<UserDto> Put(UserDto user)
        {
            await service.UpdateAsync(user);
            return user;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteByIdAsync(id);
        }
    }
}

