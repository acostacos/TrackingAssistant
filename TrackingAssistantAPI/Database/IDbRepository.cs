using System.Data;

namespace TrackingAssistantAPI.Database
{
    public interface IDbRepository
    {
        public DataTable ExecuteReader(string query, IDbDataParameter[]? parameters = null);
        public object? ExecuteScalar(string query, IDbDataParameter[]? parameters = null);
        public int ExecuteNonQuery(string query, IDbDataParameter[]? parameters = null);
    }
}
