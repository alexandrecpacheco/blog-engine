﻿using BlogEngine.Domain.Entities;
using System.Data.Common;

namespace BlogEngine.Domain.Intefaces.Data.Repository
{
    public interface IPostsRepository
    {
        Task<int> Create(PostsEntity posts, DbConnection dbConnection, DbTransaction dbTransaction);
        Task<IEnumerable<PostsEntity>> GetPosts();
    }
}
