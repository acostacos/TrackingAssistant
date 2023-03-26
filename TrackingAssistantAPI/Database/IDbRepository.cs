using System.Data;

namespace TrackingAssistantAPI.Database
{
    internal interface IDbRepository
    {
        public DataTable ExecuteReader(string query);
    }
}
