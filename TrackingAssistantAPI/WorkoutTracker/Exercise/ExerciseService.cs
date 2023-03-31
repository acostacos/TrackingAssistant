using Npgsql;
using System.Data;
using TrackingAssistantAPI.Database;
using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.Exercise.DTOs;
using TrackingAssistantAPI.WorkoutTracker.Exercise.Interfaces;
using TrackingAssistantAPI.WorkoutTracker.Exercise.Messages;

namespace TrackingAssistantAPI.WorkoutTracker.Exercise
{
    public class ExerciseService : IExerciseService
    {
        private readonly IDbRepository _dbRepository;
        public ExerciseService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public GetAllExercisesResponse GetAllExercises()
        {
            try
            {
                var query = "SELECT exercise_id, name, target_muscles FROM exercise";
                var result = _dbRepository.ExecuteReader(query);

                var exercises = new List<ExerciseDto>();
                foreach (DataRow row in result.Rows)
                {
                    exercises.Add(new ExerciseDto()
                    {
                        ExerciseId = Convert.ToInt32(row["exercise_id"]),
                        Name = row["name"]?.ToString(),
                        TargetMuscles = row["target_muscles"].ToString(),
                    });
                }

                return new GetAllExercisesResponse()
                {
                    Exercises = exercises,
                };
            }
            catch (Exception ex)
            {
                return new GetAllExercisesResponse(ex);
            }
        }

        public GetExerciseResponse GetExercise(int id)
        {
            try
            {
                var query = "SELECT exercise_id, name, target_muscles FROM exercise WHERE exercise_id=@id";
                var parameters = new IDbDataParameter[] { new NpgsqlParameter("id", id) };
                var result = _dbRepository.ExecuteReader(query, parameters);

                ExerciseDto? exercise = null;
                if (result != null && result.Rows.Count > 0)
                {
                    var row = result.Rows[0];
                    exercise = new ExerciseDto()
                    {
                        ExerciseId = Convert.ToInt32(row["exercise_id"]),
                        Name = row["name"]?.ToString(),
                        TargetMuscles = row["target_muscles"].ToString(),
                    };
                }
                return new GetExerciseResponse()
                {
                    Exercise = exercise,
                };
            }
            catch (Exception ex)
            {
                return new GetExerciseResponse(ex);
            }
        }

        public CreateExerciseResponse CreateExercise(CreateExerciseRequest request)
        {
            try
            {
                var query = "INSERT INTO exercise(name, target_muscles) VALUES(@name, @target_muscles) RETURNING exercise_id";
                var parameters = new IDbDataParameter[] { new NpgsqlParameter("name", request.Name), new NpgsqlParameter("target_muscles", request.TargetMuscles) };
                var id = _dbRepository.ExecuteNonQuery(query, parameters);
                return new CreateExerciseResponse()
                {
                    CreatedId = id,
                };
            }
            catch (Exception ex)
            {
                return new CreateExerciseResponse(ex);
            }
        }

        public ServiceResponseBase UpdateExercise(UpdateExerciseRequest request)
        {
            try
            {
                var checkerQuery = "SELECT 1 FROM exercise WHERE exercise_id=@id";
                var checkerParams = new IDbDataParameter[] { new NpgsqlParameter("id", request.ExerciseId) };
                var result = _dbRepository.ExecuteScalar(checkerQuery, checkerParams);
                if (result == null) throw new Exception("Not Found");

                var updateQuery = "UPDATE exercise SET name=@name, target_muscles=@target_muscles WHERE exercise_id=@id";
                var updateParams = new IDbDataParameter[] {
                    new NpgsqlParameter("id", request.ExerciseId),
                    new NpgsqlParameter("name", request.Name),
                    new NpgsqlParameter("target_muscles", request.TargetMuscles)
                };
                _dbRepository.ExecuteNonQuery(updateQuery, updateParams);

                return new ServiceResponseBase();
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase(ex);
            }
        }

        public ServiceResponseBase DeleteExercise(int id)
        {
            try
            {
                var checkerQuery = "SELECT 1 FROM exercise WHERE exercise_id=@id";
                var checkerParams = new IDbDataParameter[] { new NpgsqlParameter("id", id) };
                var result = _dbRepository.ExecuteScalar(checkerQuery, checkerParams);
                if (result == null) throw new Exception("Not Found");

                var deleteQuery = "DELETE FROM exercise WHERE exercise_id=@id";
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
