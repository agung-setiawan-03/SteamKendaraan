using Microsoft.Data.SqlClient;
using System.Data;

namespace SteamKendaraan.Data
{
    public class SteamKendaraanDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connection;

        public SteamKendaraanDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connection);
    }
}
