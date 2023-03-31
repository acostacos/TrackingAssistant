using Npgsql;
using System.Data;
using TrackingAssistantAPI.Database;
using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.DTOs;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.Interfaces;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.Messages;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlan
{
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly IDbRepository _dbRepository;
        public WorkoutPlanService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public GetAllWorkoutsResponse GetAllWorkoutPlan()
        {
            try
            {
                var query = "SELECT workoutplan_id, name, createdate FROM workoutplan";
                var result = _dbRepository.ExecuteReader(query);

                var workoutPlans = new List<WorkoutPlanDto>();
                foreach (DataRow row in result.Rows)
                {
                    workoutPlans.Add(new WorkoutPlanDto()
                    {
                        WorkoutPlanId = Convert.ToInt32(row["workoutplan_id"]),
                        Name = row["name"]?.ToString(),
                        CreateDate = Convert.ToDateTime(row["createdate"]),
                    });
                }

                return new GetAllWorkoutsResponse()
                {
                    WorkoutPlans = workoutPlans,
                };
            }
            catch (Exception ex)
            {
                return new GetAllWorkoutsResponse(ex);
            }
        }

        public GetWorkoutPlanResponse GetWorkoutPlan(int id)
        {
            try
            {
                var query = "SELECT workoutplan_id, name, createdate FROM workoutplan WHERE workoutplan_id=@id";
                var parameters = new IDbDataParameter[] { new NpgsqlParameter("id", id) };
                var result = _dbRepository.ExecuteReader(query, parameters);

                WorkoutPlanDto? workoutPlan = null;
                if (result != null && result.Rows.Count > 0)
                {
                    var row = result.Rows[0];
                    workoutPlan = new WorkoutPlanDto()
                    {
                        WorkoutPlanId = Convert.ToInt32(row["workoutplan_id"]),
                        Name = row["name"]?.ToString(),
                        CreateDate = Convert.ToDateTime(row["createdate"]),
                    };
                }
                return new GetWorkoutPlanResponse()
                {
                    WorkoutPlan = workoutPlan,
                };
            }
            catch (Exception ex)
            {
                return new GetWorkoutPlanResponse(ex);
            }
        }

        public CreateWorkoutPlanResponse CreateWorkoutPlan(CreateWorkoutPlanRequest request)
        {
            try
            {
                var query = "INSERT INTO workoutplan(name, createdate) VALUES(@name, now()) RETURNING workoutplan_id";
                var parameters = new IDbDataParameter[] { new NpgsqlParameter("name", request.Name) };
                var id = _dbRepository.ExecuteNonQuery(query, parameters);
                return new CreateWorkoutPlanResponse()
                {
                    CreatedId = id,
                };
            }
            catch (Exception ex)
            {
                return new CreateWorkoutPlanResponse(ex);
            }
        }

        public ServiceResponseBase UpdateWorkoutPlan(UpdateWorkoutPlanRequest request)
        {
            try
            {
                var checkerQuery = "SELECT 1 FROM workoutplan WHERE workoutplan_id=@id";
                var checkerParams = new IDbDataParameter[] { new NpgsqlParameter("id", request.WorkoutPlanId) };
                var result = _dbRepository.ExecuteScalar(checkerQuery, checkerParams);
                if (result == null) throw new Exception("Not Found");

                var updateQuery = "UPDATE workoutplan SET name=@name WHERE workoutplan_id=@id";
                var updateParams = new IDbDataParameter[] { new NpgsqlParameter("id", request.WorkoutPlanId), new NpgsqlParameter("name", request.Name) };
                _dbRepository.ExecuteNonQuery(updateQuery, updateParams);

                return new ServiceResponseBase();
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase(ex);
            }
        }

        public ServiceResponseBase DeleteWorkoutPlan(int id)
        {
            try
            {
                var checkerQuery = "SELECT 1 FROM workoutplan WHERE workoutplan_id=@id";
                var checkerParams = new IDbDataParameter[] { new NpgsqlParameter("id", id) };
                var result = _dbRepository.ExecuteScalar(checkerQuery, checkerParams);
                if (result == null) throw new Exception("Not Found");

                var deleteQuery = "DELETE FROM workoutplan WHERE workoutplan_id=@id";
                var deleteParams = new IDbDataParameter[] { new NpgsqlParameter("id", id) };
                _dbRepository.ExecuteNonQuery(deleteQuery, deleteParams);

                return new ServiceResponseBase();
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase(ex);
            }
        }
    }
}
