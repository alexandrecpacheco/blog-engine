using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Enums;
using BlogEngine.Domain.Intefaces;
using BlogEngine.Domain.Intefaces.Data.Repository;
using BlogEngine.Domain.Intefaces.Data.Service;
using Serilog;
using System.Data;

namespace BlogEngine.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorProfileRepository _authorProfileRepository;
        private readonly IDatabase _database;

        public AuthorService(IAuthorRepository authorRepository,
            IDatabase database,
            IAuthorProfileRepository authorProfileRepository)
        {
            _authorRepository = authorRepository;
            _authorProfileRepository = authorProfileRepository;
            _database = database;
        }

        public async Task<Author> AuthenticationAsync(SignInRequest signInRequest)
        {
            var author = new Author()
            {
                Email = signInRequest.Email,
                Password = signInRequest.Password
            };

            var currentUser = await _authorRepository.GetByEmailAndPasswordAsync(author);
            if (currentUser == null) return null!;

            if (currentUser.IsActive == false) return null!;

            return currentUser;
        }

        public async Task CreateAsync(AuthorRequest authorRequest)
        {
            var emailAvailable = await GetByEmailAsync(authorRequest.Email);
            if (emailAvailable != null)
            {
                Log.Information("Email is already taken");
                throw new DuplicateNameException($"Email {authorRequest.Email} is already taken");
            }

            await _database.ExecuteInTransaction(async (connection, transaction) =>
            {
                var author = new Author
                {
                    Name = authorRequest.Name.Trim(),
                    Email = authorRequest.Email.Trim(),
                    Password = authorRequest.Password.Trim(),
                    IsActive = authorRequest.IsActive
                };

                Log.Information("Insert a new author");
                var authorId = await _authorRepository.CreateAsync(author, connection, transaction);
                var profile = (ProfileEnum)Enum.Parse(typeof(ProfileEnum), authorRequest.AuthorProfile.Profile.Description);
                var authorProfile = new AuthorProfile
                {
                    ProfileId = (int)profile,
                    AuthorId = authorId
                };

                Log.Information("Create a author profile");
                await _authorProfileRepository.CreateAsync(authorProfile, connection, transaction);
            });
        }

        public async Task<Author> GetByEmailAsync(string email)
        {
            return await _authorRepository.GetByEmailAsync(email);
        }
    }
}
