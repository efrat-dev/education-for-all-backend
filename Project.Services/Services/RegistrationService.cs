using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Project.Services.Interfaces;

namespace Project.Services.Services
{
    public class RegistrationService : IRegistrationService
    {
        // Registers a new user asynchronously by checking for conflicts (email and username)
        public async Task<IActionResult> RegisterUserAsync(
            UserDto user,
            IUserService<UserDto> userService,
            IUserService<CounselorDto> counselorService,
            Func<UserDto, Task<UserDto?>> addCallback
        )
        {
            // Check if the email or username already exists in either the user or counselor services.
            bool emailExists = await userService.EmailExists(user.Email)
                            || await counselorService.EmailExists(user.Email);

            bool nameExists = await userService.UserNameExists(user.Name)
                            || await counselorService.UserNameExists(user.Name);

            // Get a conflict result (if any) regarding email or username.
            var conflict = GetConflictResult(emailExists, nameExists);
            if (conflict != null)
                return conflict;

            // Hash the password using BCrypt for secure storage.
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var newUser = await addCallback(user);
            if (newUser == null)
                return new StatusCodeResult(500);

            return new OkObjectResult(newUser.Id);
        }

        public async Task<IActionResult> RegisterCounselorAsync(
            CounselorDto counselor,
            IUserService<CounselorDto> counselorService,
            IUserService<UserDto> userService,
            Func<CounselorDto, Task<CounselorDto?>> addCallback
        )
        {
            bool emailExists = await counselorService.EmailExists(counselor.Email)
                            || await userService.EmailExists(counselor.Email);

            bool nameExists = await counselorService.UserNameExists(counselor.Name)
                            || await userService.UserNameExists(counselor.Name);

            var conflict = GetConflictResult(emailExists, nameExists);
            if (conflict != null)
                return conflict;

            counselor.Password = BCrypt.Net.BCrypt.HashPassword(counselor.Password);

            var newCounselor = await addCallback(counselor);
            if (newCounselor == null || newCounselor.Id == 0)
                return new StatusCodeResult(500);

            return new OkObjectResult(newCounselor.Id);
        }

        // Hash the password using BCrypt for secure storage.
        private IActionResult? GetConflictResult(bool emailExists, bool nameExists)
        {
            if (emailExists && nameExists)
                return new ConflictObjectResult(new { message = "Email and Username already exist" });
            if (emailExists)
                return new ConflictObjectResult(new { message = "Email already exists" });
            if (nameExists)
                return new ConflictObjectResult(new { message = "Username already exists" });
            return null;
        }
    }
}
