using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace InClub.Infraestructure.Context
{
    public class RepositoryContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public RepositoryContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}