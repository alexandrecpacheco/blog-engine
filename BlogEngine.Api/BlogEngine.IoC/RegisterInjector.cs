using BlogEngine.Domain.Intefaces;
using BlogEngine.Domain.Intefaces.Data.Repository;
using BlogEngine.Domain.Intefaces.Data.Service;
using BlogEngine.Infrastructure;
using BlogEngine.Infrastructure.Data;
using BlogEngine.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BlogEngine.IoC
{
    [ExcludeFromCodeCoverage]
    public static class RegisterInjector
    {
        public static void RegisterDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IDatabase, Database>();

            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPostsService, PostsService>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorProfileRepository, AuthorProfileRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
        }
    }
}
