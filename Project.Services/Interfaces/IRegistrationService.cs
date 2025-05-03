using Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Project.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<IActionResult> RegisterUserAsync(
            UserDto user,
            IUserService<UserDto> userService,
            IUserService<CounselorDto> counselorService,
            Func<UserDto, Task<UserDto?>> addCallback
        );

        Task<IActionResult> RegisterCounselorAsync(
            CounselorDto counselor,
            IUserService<CounselorDto> counselorService,
            IUserService<UserDto> userService,
            Func<CounselorDto, Task<CounselorDto?>> addCallback
        );
    }
}
