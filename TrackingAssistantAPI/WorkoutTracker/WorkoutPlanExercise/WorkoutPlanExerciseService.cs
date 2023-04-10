using Npgsql;
using System.Data;
using TrackingAssistantAPI.Database;
using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.DTOs;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.DTOs;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.Interfaces;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.Messages;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise
{
    public class WorkoutPlanExerciseService : IWorkoutPlanExerciseService
    {
        private readonly IDbRepository _dbRepository;
        public WorkoutPlanExerciseService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public GetAllWorkoutPlansWithExercisesResponse GetAllWorkoutPlansWithExercises()
        {
            try
            {
                var query = @"SELECT
	wpe.workoutplan_exercise_id AS wpe_workoutplan_exercise_id,
	wpe.start_set_range AS wpe_start_set_range,
	wpe.end_set_range AS wpe_end_set_range,
	wpe.start_rep_range AS wpe_start_rep_range,
	wpe.end_rep_range AS wpe_end_rep_range,
	wpe.day AS wpe_day,
	wpe.day_name AS wpe_day_name,
	wp.workoutplan_id AS wp_id,
	wp.name AS wp_name,
	wp.createdate AS wp_create_date,
	e.exercise_id AS e_id,
	e.name AS e_name,
	e.target_muscles AS e_target_muscles
FROM workoutplan_exercise wpe
INNER JOIN workoutplan wp
ON wpe.workoutplan_id = wp.workoutplan_id
INNER JOIN exercise e
ON wpe.exercise_id = e.exercise_id";
                var result = _dbRepository.ExecuteReader(query);

                var workoutPlanExercises = new List<WorkoutPlanExerciseResponseDto>();
                foreach (DataRow row in result.Rows)
                {
                    var newDto = new WorkoutPlanExerciseDto()
                    {
                        WorkoutPlanExerciseId = Convert.ToInt32(row["wpe_workoutplan_exercise_id"]),
                        WorkoutPlanExerciseStartSetRange = Convert.ToInt32(row["wpe_start_set_range"]),
                        WorkoutPlanExerciseEndSetRange = Convert.ToInt32(row["wpe_end_set_range"]),
                        WorkoutPlanExerciseStartRepRange = Convert.ToInt32(row["wpe_start_rep_range"]),
                        WorkoutPlanExerciseEndRepRange = Convert.ToInt32(row["wpe_end_rep_range"]),
                        WorkoutPlanExerciseDay = Convert.ToInt32(row["wpe_day"]),
                        WorkoutPlanExerciseDayName = row["wpe_day_name"].ToString(),
                        WorkoutPlanId = Convert.ToInt32(row["wp_id"]),
                        WorkoutPlanName = row["wp_name"].ToString(),
                        WorkoutPlanCreateDate = Convert.ToDateTime(row["wp_create_date"]),
                        ExerciseId = Convert.ToInt32(row["e_id"]),
                        ExerciseName = row["e_name"].ToString(),
                        ExerciseTargetMuscles = row["e_target_muscles"].ToString(),
                    };

                    var workoutParent = workoutPlanExercises.FirstOrDefault(x => x.WorkoutPlan?.WorkoutPlanId == newDto.WorkoutPlanId);
                    if (workoutParent == null)
                    {
                        workoutPlanExercises.Add(new WorkoutPlanExerciseResponseDto()
                        {
                            WorkoutPlan = new WorkoutPlanDto()
                            {
                                WorkoutPlanId = newDto.WorkoutPlanId,
                                Name = newDto.WorkoutPlanName,
                                CreateDate = newDto.WorkoutPlanCreateDate,
                            },
                            Exercises = new List<PlanExerciseDto>()
                            {
                                new PlanExerciseDto()
                                {
                                    WorkoutPlanExerciseId = newDto.WorkoutPlanExerciseId,
                                    ExerciseId = newDto.ExerciseId,
                                    Name = newDto.ExerciseName,
                                    TargetMuscles = newDto.ExerciseTargetMuscles,
                                    StartSetRange = newDto.WorkoutPlanExerciseStartSetRange,
                                    EndSetRange = newDto.WorkoutPlanExerciseEndSetRange,
                                    StartRepRange = newDto.WorkoutPlanExerciseStartRepRange,
                                    EndRepRange = newDto.WorkoutPlanExerciseEndRepRange,
                                    Day = newDto.WorkoutPlanExerciseDay,
                                    DayName = newDto.WorkoutPlanExerciseDayName,
                                }
                            },
                        });
                    }
                    else
                    {
                        workoutParent.Exercises?.Add(new PlanExerciseDto()
                        {
                            WorkoutPlanExerciseId = newDto.WorkoutPlanExerciseId,
                            ExerciseId = newDto.ExerciseId,
                            Name = newDto.ExerciseName,
                            TargetMuscles = newDto.ExerciseTargetMuscles,
                            StartSetRange = newDto.WorkoutPlanExerciseStartSetRange,
                            EndSetRange = newDto.WorkoutPlanExerciseEndSetRange,
                            StartRepRange = newDto.WorkoutPlanExerciseStartRepRange,
                            EndRepRange = newDto.WorkoutPlanExerciseEndRepRange,
                            Day = newDto.WorkoutPlanExerciseDay,
                            DayName = newDto.WorkoutPlanExerciseDayName,
                        });
                    }
                }

                return new GetAllWorkoutPlansWithExercisesResponse()
                {
                    WorkoutPlanExercises = workoutPlanExercises,
                };
            }
            catch (Exception ex)
            {
                return new GetAllWorkoutPlansWithExercisesResponse(ex);
            }
        }

        public GetWorkoutPlanWithExercisesResponse GetWorkoutPlanWithExercises(int id)
        {
            try
            {
                var query = @"SELECT
	wpe.workoutplan_exercise_id AS wpe_workoutplan_exercise_id,
	wpe.start_set_range AS wpe_start_set_range,
	wpe.end_set_range AS wpe_end_set_range,
	wpe.start_rep_range AS wpe_start_rep_range,
	wpe.end_rep_range AS wpe_end_rep_range,
	wpe.day AS wpe_day,
	wpe.day_name AS wpe_day_name,
	wp.workoutplan_id AS wp_id,
	wp.name AS wp_name,
	wp.createdate AS wp_create_date,
	e.exercise_id AS e_id,
	e.name AS e_name,
	e.target_muscles AS e_target_muscles
FROM workoutplan_exercise wpe
INNER JOIN workoutplan wp
ON wpe.workoutplan_id = wp.workoutplan_id
INNER JOIN exercise e
ON wpe.exercise_id = e.exercise_id
WHERE wp.workoutplan_id=@id";
                var parameters = new IDbDataParameter[] { new NpgsqlParameter("id", id) };
                var result = _dbRepository.ExecuteReader(query, parameters);
                WorkoutPlanExerciseResponseDto? workoutPlanExercise = null;
                if (result != null && result.Rows.Count > 0)
                {
                    var firstRow = result.Rows[0];
                    workoutPlanExercise = new WorkoutPlanExerciseResponseDto()
                    {
                        WorkoutPlan = new WorkoutPlanDto()
                        {
                            WorkoutPlanId = Convert.ToInt32(firstRow["wp_id"]),
                            Name = firstRow["wp_name"].ToString(),
                            CreateDate = Convert.ToDateTime(firstRow["wp_create_date"]),
                        },
                        Exercises = new List<PlanExerciseDto>(),
                    };
                    foreach (DataRow row in result.Rows)
                    {
                        workoutPlanExercise.Exercises?.Add(new PlanExerciseDto()
                        {
                            WorkoutPlanExerciseId = Convert.ToInt32(row["wpe_workoutplan_exercise_id"]),
                            ExerciseId = Convert.ToInt32(row["e_id"]),
                            Name = row["e_name"].ToString(),
                            TargetMuscles = row["e_target_muscles"].ToString(),
                            StartSetRange = Convert.ToInt32(row["wpe_start_set_range"]),
                            EndSetRange = Convert.ToInt32(row["wpe_end_set_range"]),
                            StartRepRange = Convert.ToInt32(row["wpe_start_rep_range"]),
                            EndRepRange = Convert.ToInt32(row["wpe_end_rep_range"]),
                            Day = Convert.ToInt32(row["wpe_day"]),
                            DayName = row["wpe_day_name"].ToString(),
                        });
                    }
                }

                return new GetWorkoutPlanWithExercisesResponse()
                {
                    WorkoutPlanExercise = workoutPlanExercise,
                };
            }
            catch (Exception ex)
            {
                return new GetWorkoutPlanWithExercisesResponse(ex);
            }
        }

        public ServiceResponseBase AddExerciseToWorkoutPlan(AddExerciseToWorkoutPlanRequest request)
        {
            try
            {
                var query = @"INSERT INTO workoutplan_exercise(workoutplan_id, exercise_id, start_set_range, end_set_range, start_rep_range, end_rep_range, day, day_name)
VALUES(@workoutplan_id, @exercise_id, @start_set_range, @end_set_range, @start_rep_range, @end_rep_range, @day, @day_name)";
                var parameters = new IDbDataParameter[] {
                    new NpgsqlParameter("workoutplan_id", request.WorkoutId),
                    new NpgsqlParameter("exercise_id", request.ExerciseId),
                    new NpgsqlParameter("start_set_range", request.StartSetRange),
                    new NpgsqlParameter("end_set_range", request.EndSetRange),
                    new NpgsqlParameter("start_rep_range", request.StartRepRange),
                    new NpgsqlParameter("end_rep_range", request.EndRepRange),
                    new NpgsqlParameter("day", request.Day),
                    new NpgsqlParameter("day_name", request.DayName),
                };
                var result = _dbRepository.ExecuteNonQuery(query, parameters);

                return new ServiceResponseBase();
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase(ex);
            }
        }

        public ServiceResponseBase DeleteExerciseFromWorkoutPlan(DeleteExerciseFromWorkoutPlanRequest request)
        {
            try
            {
                var checkerQuery = "SELECT 1 FROM workoutplan_exercise WHERE workoutplan_id=@workout_id AND exercise_id=@exercise_id";
                var checkerParams = new IDbDataParameter[] {
                    new NpgsqlParameter("workout_id", request.WorkoutId),
                    new NpgsqlParameter("exercise_id", request.ExerciseId),
                };
                var result = _dbRepository.ExecuteScalar(checkerQuery, checkerParams);
                if (result == null) throw new Exception("Not Found");

                var deleteQuery = "DELETE FROM workoutplan_exercise WHERE workoutplan_id=@workout_id AND exercise_id=@exercise_id";
                var deleteParams = new IDbDataParameter[] {
                    new NpgsqlParameter("workout_id", request.WorkoutId),
                    new NpgsqlParameter("exercise_id", request.ExerciseId),
                };
                _dbRepository.ExecuteNonQuery(deleteQuery, deleteParams);

                return new ServiceResponseBase();
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase(ex);
            }
        }

        public ServiceResponseBase DeleteAllExercisesFromWorkoutPlan(int id)
        {
            try
            {
                var checkerQuery = "SELECT 1 FROM workoutplan_exercise WHERE workoutplan_id=@id";
                var checkerParams = new IDbDataParameter[] { new NpgsqlParameter("id", id) };
                var result = _dbRepository.ExecuteScalar(checkerQuery, checkerParams);
                if (result == null) throw new Exception("Not Found");

                var deleteQuery = "DELETE FROM workoutplan_exercise WHERE workoutplan_id=@id";
                var deleteParams = new IDbDataParameter[] { new NpgsqlParameter("id", id) };
                _dbRepository.ExecuteNonQuery(deleteQuery, deleteParams);

                return new ServiceResponseBase();
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase(ex);
            }
        }

        public ServiceResponseBase UpdateExerciseInWorkoutPlan(UpdateExerciseInWorkoutPlanRequest request)
        {
            try
            {
                var checkerQuery = "SELECT 1 FROM workoutplan_exercise WHERE workoutplan_id=@workout_id AND exercise_id=@exercise_id";
                var checkerParams = new IDbDataParameter[] {
                    new NpgsqlParameter("workout_id", request.WorkoutId),
                    new NpgsqlParameter("exercise_id", request.ExerciseId),
                };
                var result = _dbRepository.ExecuteScalar(checkerQuery, checkerParams);
                if (result == null) throw new Exception("Not Found");

                var updateQuery = @"UPDATE workoutplan_exercise
SET
start_set_range=@start_set_range,
end_set_range=@end_set_range,
start_rep_range=@start_rep_range,
end_rep_range=@end_rep_range,
day=@day,
day_name=@day_name
WHERE workoutplan_id=@id
AND exercise_id=@exercise_id";
                var updateParams = new IDbDataParameter[] {
                    new NpgsqlParameter("workoutplan_id", request.WorkoutId),
                    new NpgsqlParameter("exercise_id", request.ExerciseId),
                    new NpgsqlParameter("start_set_range", request.StartSetRange),
                    new NpgsqlParameter("end_set_range", request.EndSetRange),
                    new NpgsqlParameter("start_rep_range", request.StartRepRange),
                    new NpgsqlParameter("end_rep_range", request.EndRepRange),
                    new NpgsqlParameter("day", request.Day),
                    new NpgsqlParameter("day_name", request.DayName),
                };
                _dbRepository.ExecuteNonQuery(updateQuery, updateParams);

                return new ServiceResponseBase();
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase(ex);
            }
        }
    }
}
