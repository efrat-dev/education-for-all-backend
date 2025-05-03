using AutoMapper;
using Common.Dto;
using Project.Repositories.Entities;
using Project.Repositories.Interfaces;
using Project.Services.Interfaces;

namespace Project.Services.Services
{
    public class UserService : IUserService<UserDto>
    {
        private readonly IUserRepository<User> userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<UserDto?> AddAsync(UserDto entity)
        {
            var user = await userRepository.AddAsync(mapper.Map<User>(entity));
            return user != null ? mapper.Map<UserDto>(user) : null;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await userRepository.DeleteByIdAsync(id);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            return mapper.Map<UserDto>(user);

        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            return await mapper.Map<Task<List<UserDto>>>(mapper.Map<Task<List<User>>>(userRepository.GetAllUsersAsync()));
        }

        public async Task<UserDto> UpdateAsync(UserDto user)
        {
            await userRepository.UpdateAsync(mapper.Map<User>(user));
            return user;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await userRepository.EmailExistsAsync(email);
        }

        public async Task<bool> UserNameExists(string userName)
        {
            return await userRepository.UserNameExistsAsync(userName);
        }
    }
}
