using Dapper;
using InsuranceUsers.ORM;
using InsuranceUsers.Repository.Models;
using System.Data;

namespace InsuranceUsers.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDetails>> GetAllUsers()
        {
            var query = "SELECT * FROM [InsurancePlatform].[dbo].[User]";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<UserDetails>(query);
            }
        }

        public async Task<IEnumerable<UserDetails>> GetUserByUsername(string Username)
        {
            var query = "SELECT * FROM [InsurancePlatform].[dbo].[User] WHERE Username=@Username";
            var parameters = new DynamicParameters();
            parameters.Add("Username", Username, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<UserDetails>(query, parameters);
            }
        }

        public async Task <int> Register(UserDetails userDetails)
        {
            var query = "INSERT INTO [InsurancePlatform].[dbo].[User] VALUES (@Title, @Name, @Surname, @EmailAddress, @Username, @PasswordSalt, @PasswordHash)";
            var parameters = new DynamicParameters();
            parameters.Add("Title", userDetails.Title, DbType.String);
            parameters.Add("Name", userDetails.Name, DbType.String);
            parameters.Add("Surname", userDetails.Surname, DbType.String);
            parameters.Add("EmailAddress", userDetails.EmailAddress, DbType.String);
            parameters.Add("Username", userDetails.Username, DbType.String);
            parameters.Add("PasswordSalt", userDetails.PasswordSalt, DbType.String);
            parameters.Add("PasswordHash", userDetails.PasswordHash, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, parameters);
                return result;
            }
        }
    }
}
