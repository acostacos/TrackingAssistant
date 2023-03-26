using Npgsql;
using System.Data;
using TrackingAssistantAPI.Shared;

namespace TrackingAssistantAPI.Database
{
    public class PostgresDbRepository : IDbRepository
    {
        private readonly string _connectionString;
        public PostgresDbRepository(IConfigurationSettings configurationSettings)
        {
            _connectionString = configurationSettings.ConnectionString;
        }

        public DataTable ExecuteReader(string query, IDbDataParameter[]? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                using var command = new NpgsqlCommand(query, connection);
                if (parameters != null) command.Parameters.AddRange(parameters);
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

        public object? ExecuteScalar(string query, IDbDataParameter[]? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                using var command = new NpgsqlCommand(query, connection);
                if (parameters != null) command.Parameters.AddRange(parameters);
                return command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new NpgsqlException(ex.Message, ex);
            }
        }

        public int ExecuteNonQuery(string query, IDbDataParameter[]? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                using var command = new NpgsqlCommand(query, connection);
                if (parameters != null) command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new NpgsqlException(ex.Message, ex);
            }
        }
    }
}
