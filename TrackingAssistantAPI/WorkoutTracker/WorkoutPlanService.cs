using Npgsql;
using System.Data;
using TrackingAssistant.Service.WorkoutTracker.Interfaces;
using TrackingAssistant.Service.WorkoutTracker.Messages;
using TrackingAssistantAPI.Database;
using TrackingAssistantAPI.WorkoutTracker.Models;

namespace TrackingAssistant.Service.WorkoutTracker
{
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly IDbRepository _dbRepository;
        public WorkoutPlanService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        // TODO: Handle Exceptions

        public List<WorkoutPlan> GetAllWorkoutPlan()
        {
            try
            {
                var query = "SELECT workoutplanid, name, createdate FROM workoutplan";
                var result = _dbRepository.ExecuteReader(query);

                var workoutPlans = new List<WorkoutPlan>();
                foreach (DataRow row in result.Rows)
                {
                    workoutPlans.Add(new WorkoutPlan()
                    {
                        WorkoutPlanId = Convert.ToInt32(row["workoutplanid"]),
                        Name = row["name"]?.ToString(),
                        CreateDate = Convert.ToDateTime(row["createdate"]),
                    }); ;
                }

                return workoutPlans;
            } catch
            {
                throw;
            }
        }

        public WorkoutPlan? GetWorkoutPlan(int id)
        {
            try
            {
                var query = "SELECT workoutplanid, name, createdate FROM workoutplan WHERE workoutplanid=@id";
                var parameters = new IDbDataParameter[] { new NpgsqlParameter("id", id) };
                var result = _dbRepository.ExecuteReader(query, parameters);

                WorkoutPlan? workoutPlan = null;
                if (result != null && result.Rows.Count > 0)
                {
                    var row = result.Rows[0];
                    workoutPlan = new WorkoutPlan()
                    {
                        WorkoutPlanId = Convert.ToInt32(row["workoutplanid"]),
                        Name = row["name"]?.ToString(),
                        CreateDate = Convert.ToDateTime(row["createdate"]),
                    };
                }
                return workoutPlan;
            }
            catch
            {
                throw;
            }
        }

        public int CreateWorkoutPlan(CreateWorkoutPlanRequest request)
        {
            try
            {
                var query = "INSERT INTO workoutplan(name, createdate) VALUES(@name, now()) RETURNING workoutplanid";
                var parameters = new IDbDataParameter[] { new NpgsqlParameter("name", request.Name) };
                return _dbRepository.ExecuteNonQuery(query, parameters);
            }
            catch
            {
                throw;
            }
        }

        public void DeleteWorkoutPlan(int id)
        {
            try
            {
                var parameters = new IDbDataParameter[] { new NpgsqlParameter("id", id) };
                var checkerQuery = "SELECT 1 FROM workoutplan WHERE workoutplanid=@id";
                var result = _dbRepository.ExecuteScalar(checkerQuery, parameters);
                if (result == null) throw new Exception("Not Found");

                var deleteQuery = "DELETE FROM workoutplan WHERE workoutplanid=@id";
                _dbRepository.ExecuteNonQuery(deleteQuery, parameters);
            }
            catch
            {
                throw;
            }
        }
    }
}
