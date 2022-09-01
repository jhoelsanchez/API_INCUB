using Dapper;
using InClub.Application.Interfaces;
using InClub.Common.Util;
using InClub.Core.Models;
using InClub.Infraestructure.Context;

namespace InClub.Infraestructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RepositoryContext context;

        public UserRepository(RepositoryContext context)
        {
            this.context = context;
        }

        public async Task<int> AddAsync(User entity)
        {
            entity.Password = Cryptography.Encrypt(entity.Password);
            entity.Created = DateTime.Now;
            var sql = "INSERT INTO Users (Name, Email, Password, Created, Status) VALUES (@Name, @Email, @Password, @Created, @Status)";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Users WHERE Id = @Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM Users";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.QueryAsync<User>(sql);
                return result.ToList();
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(User entity)
        {
            entity.Updated = DateTime.Now;
            var sql = "UPDATE Users SET Name = @Name, Email = @Email, Password = @Password, Updated = @Updated  WHERE Id = @Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}