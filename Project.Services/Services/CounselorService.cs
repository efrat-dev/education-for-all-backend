using AutoMapper;
using Common.Dto;
using Project.Repositories.Entities;
using Project.Repositories.Interfaces;
using Project.Services.Interfaces;

namespace Project.Services.Services
{
    public class CounselorService : IUserService<CounselorDto>
    {
        private readonly IUserRepository<Counselor> counselorRepository;
        private readonly IMapper mapper;

        public CounselorService(IUserRepository<Counselor> counselorRepository, IMapper mapper)
        {
            this.counselorRepository = counselorRepository;
            this.mapper = mapper;
        }
        public async Task<CounselorDto?> AddAsync(CounselorDto entity)
        {
            var counselor = await counselorRepository.AddAsync(mapper.Map<Counselor>(entity));
            return counselor != null ? mapper.Map<CounselorDto>(counselor) : null;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await counselorRepository.DeleteByIdAsync(id);
        }

        public async Task<CounselorDto> GetByIdAsync(int id)
        {
            return await mapper.Map<Task<CounselorDto>>(counselorRepository.GetByIdAsync(id));
        }

        public async Task<CounselorDto> UpdateAsync(CounselorDto counselor)
        {
            await counselorRepository.UpdateAsync(mapper.Map<Counselor>(counselor));
            return counselor;
        }

        public async Task<List<CounselorDto>> GetAllAsync()
        {
            var x = await mapper.Map<Task<List<CounselorDto>>>(counselorRepository.GetAllUsersAsync());
            return x;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await counselorRepository.EmailExistsAsync(email);
        }

        public async Task<bool> UserNameExists(string userName)
        {
            return await counselorRepository.UserNameExistsAsync(userName);
        }
    }
}
