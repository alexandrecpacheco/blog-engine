using BlogEngine.Domain.Intefaces;
using BlogEngine.Domain.Intefaces.Data.Repository;
using BlogEngine.Domain.Intefaces.Data.Service;

namespace BlogEngine.Service
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IDatabase _database;

        public PostsService(IPostsRepository postsRepository, IDatabase database)
        {
            _postsRepository = postsRepository;
            _database = database;
        }
    }
}
