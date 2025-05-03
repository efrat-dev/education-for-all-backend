using AutoMapper;
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using Project.Repositories.Entities;
using Project.Repositories.Interfaces;
using Project.Services.Interfaces;

namespace Project.Services.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
        }
        public async Task<PostDto?> AddAsync(PostDto entity)
        {
            var post = await postRepository.AddAsync(mapper.Map<Post>(entity));
            return post != null ? mapper.Map<PostDto>(post) : null;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await postRepository.DeleteByIdAsync(id);
        }

        public async Task<List<PostDto>> GetAllAsync()
        {
            var posts = await postRepository.GetAllAsync();
            return mapper.Map<List<PostDto>>(posts);

        }

        public async Task<PostDto> GetByIdAsync(int id)
        {
            return await mapper.Map<Task<PostDto>>(postRepository.GetByIdAsync(id));
        }

        public async Task<PostDto> UpdateAsync(PostDto post)
        {
            await postRepository.UpdateAsync(mapper.Map<Post>(post));
            return post;
        }

        public async Task<List<PostDto>> GetPostsByTopicIdAsync(int topicId)
        {
            var posts = await postRepository.GetPostsByTopicIdAsync(topicId);
            return mapper.Map<List<PostDto>>(posts);
        }

        public async Task<int> LikePostAsync(int postId)
        {
            var post = await postRepository.GetByIdAsync(postId);
            if (post == null)
            {
                throw new ArgumentException("Post not found");
            }

            post.Likes++;

            await postRepository.UpdateAsync(post);

            return post.Likes;
        }
    }
}
