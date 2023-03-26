using Npgsql;
using System.Data;
using TrackingAssistantAPI.Shared;

namespace TrackingAssistantAPI.Database
{
    internal class PostgresDbRepository : IDbRepository
    {
        private readonly string _connectionString;
        public PostgresDbRepository(IConfigurationSettings configurationSettings)
        {
            _connectionString = configurationSettings.ConnectionString;
        }

        public DataTable ExecuteReader(string query)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                using var command = new NpgsqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                var result = new DataTable();
                result.Load(reader);

                return result;
            }
            catch (Exception ex)
            {
                throw new NpgsqlException(ex.Message, ex);
            }
        }
    }
}
