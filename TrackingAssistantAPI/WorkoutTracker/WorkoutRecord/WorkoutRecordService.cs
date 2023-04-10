using Npgsql;
using System.Data;
using TrackingAssistantAPI.Database;
using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.DTOs;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.DTOs;
using TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.DTOs;
using TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Interfaces;
using TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Messages;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutRecord
{
    public class WorkoutRecordService : IWorkoutRecordService
    {
        private readonly IDbRepository _dbRepository;

        public WorkoutRecordService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public ServiceResponseBase AddWorkoutRecord(CreateWorkoutRecordRequest request)
        {
            try
            {
                var query = @"INSERT INTO workoutrecord(workoutplan_exercise_id, record_timestamp, sets, reps)
VALUES(@workoutplan_exercise_id, CAST(CURRENT_TIMESTAMP AS timestamp without time zone), @sets, @reps)";
                var parameters = new IDbDataParameter[] {
                    new NpgsqlParameter("workoutplan_exercise_id", request.WorkoutPlanExerciseId),
                    new NpgsqlParameter("sets", request.Sets),
                    new NpgsqlParameter("reps", request.Reps),
                };
                var id = _dbRepository.ExecuteNonQuery(query, parameters);
                return new ServiceResponseBase();
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase(ex);
            }
        }

        public ServiceResponseBase UpdateWorkoutRecord(UpdateWorkoutRecordRequest request)
        {
            try
            {
                var query = @"UPDATE workoutrecord
SET workoutplan_exercise_id=@workoutplan_exercise_id,
record_timestamp=CAST(CURRENT_TIMESTAMP AS timestamp without time zone),
sets=@sets,
reps=@reps
WHERE workoutrecord_id=@workoutrecord_id";
                var parameters = new IDbDataParameter[] {
                    new NpgsqlParameter("workoutplan_exercise_id", request.WorkoutPlanExerciseId),
                    new NpgsqlParameter("sets", request.Sets),
                    new NpgsqlParameter("reps", request.Reps),
                    new NpgsqlParameter("workoutrecord_id", request.WorkoutRecordId),
                };
                var id = _dbRepository.ExecuteNonQuery(query, parameters);
                return new ServiceResponseBase();
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase(ex);
            }
        }

        public ServiceResponseBase DeleteWorkoutRecord(int id)
        {
            try
            {
                var checkerQuery = "SELECT 1 FROM workoutrecord WHERE workoutrecord_id=@id";
                var checkerParams = new IDbDataParameter[] { new NpgsqlParameter("id", id) };
                var result = _dbRepository.ExecuteScalar(checkerQuery, checkerParams);
                if (result == null) throw new Exception("Not Found");

                var deleteQuery = "DELETE FROM workoutrecord WHERE workoutrecord_id=@id";
                var deleteParams = new IDbDataParameter[] { new NpgsqlParameter("id", id) };
                _dbRepository.ExecuteNonQuery(deleteQuery, deleteParams);

                return new ServiceResponseBase();
            }
            catch (Exception ex)
            {
                return new ServiceResponseBase(ex);
            }
        }

        public GetWorkoutRecordsResponse GetAllWorkoutRecords()
        {
            try
            {
                var query = @"SELECT
	wr.workoutrecord_id AS wr_workoutrecord_id,
	wr.record_timestamp AS wr_record_timestamp,
	wr.sets AS wr_sets,
	wr.reps AS wr_reps,
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
FROM workoutrecord wr
INNER JOIN workoutplan_exercise wpe
ON wr.workoutplan_exercise_id = wpe.workoutplan_exercise_id
INNER JOIN workoutplan wp
ON wpe.workoutplan_id = wp.workoutplan_id
INNER JOIN exercise e
ON wpe.exercise_id = e.exercise_id";
                var result = _dbRepository.ExecuteReader(query);

                var workoutRecords = new List<WorkoutRecordDto>();
                foreach (DataRow row in result.Rows)
                {
                    workoutRecords.Add(new WorkoutRecordDto()
                    {
                        WorkoutRecordId = Convert.ToInt32(row["wr_workoutrecord_id"]),
                        RecordTimestamp = Convert.ToDateTime(row["wr_record_timestamp"]),
                        Sets = Convert.ToInt32(row["wr_sets"]),
                        Reps = Convert.ToInt32(row["wr_reps"]),
                        WorkoutPlanExercise = new WorkoutPlanExerciseResponseDto()
                        {
                            WorkoutPlan = new WorkoutPlanDto()
                            {
                                WorkoutPlanId = Convert.ToInt32(row["wp_id"]),
                                Name = row["wp_name"].ToString(),
                                CreateDate = Convert.ToDateTime(row["wp_create_date"]),
                            },
                            Exercise = new PlanExerciseDto()
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
                            },
                        },
                    });
                }

                return new GetWorkoutRecordsResponse()
                {
                    WorkoutRecords = workoutRecords,
                };
            }
            catch (Exception ex)
            {
                return new GetWorkoutRecordsResponse(ex);
            }
        }

        public GetWorkoutRecordsResponse GetWorkoutRecords(GetWorkoutRecordsRequest request)
        {
            try
            {
                var query = @"SELECT
	wr.workoutrecord_id AS wr_workoutrecord_id,
	wr.record_timestamp AS wr_record_timestamp,
	wr.sets AS wr_sets,
	wr.reps AS wr_reps,
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
FROM workoutrecord wr
INNER JOIN workoutplan_exercise wpe
ON wr.workoutplan_exercise_id = wpe.workoutplan_exercise_id
INNER JOIN workoutplan wp
ON wpe.workoutplan_id = wp.workoutplan_id
INNER JOIN exercise e
ON wpe.exercise_id = e.exercise_id
WHERE {0}";

                if (request.WorkoutPlanExerciseId == null && request.RecordDate == null) throw new Exception("No query parameters added");

                var conditions = new List<string>();
                var parameters = new List<IDbDataParameter>();
                if (request.WorkoutPlanExerciseId != null && request.WorkoutPlanExerciseId != 0)
                {
                    conditions.Add("wpe.workoutplan_exercise_id=@workoutplan_exercise_id");
                    parameters.Add(new NpgsqlParameter("workoutplan_exercise_id", request.WorkoutPlanExerciseId));
                }
                if (request.RecordDate != null)
                {
                    conditions.Add("wr.record_timestamp::date=@record_date::date");
                    parameters.Add(new NpgsqlParameter("record_date", request.RecordDate));
                }

                query = String.Format(query, String.Join(" AND ", conditions));
                var result = _dbRepository.ExecuteReader(query, parameters.ToArray());

                var workoutRecords = new List<WorkoutRecordDto>();
                foreach (DataRow row in result.Rows)
                {
                    workoutRecords.Add(new WorkoutRecordDto()
                    {
                        WorkoutRecordId = Convert.ToInt32(row["wr_workoutrecord_id"]),
                        RecordTimestamp = Convert.ToDateTime(row["wr_record_timestamp"]),
                        Sets = Convert.ToInt32(row["wr_sets"]),
                        Reps = Convert.ToInt32(row["wr_reps"]),
                        WorkoutPlanExercise = new WorkoutPlanExerciseResponseDto()
                        {
                            WorkoutPlan = new WorkoutPlanDto()
                            {
                                WorkoutPlanId = Convert.ToInt32(row["wp_id"]),
                                Name = row["wp_name"].ToString(),
                                CreateDate = Convert.ToDateTime(row["wp_create_date"]),
                            },
                            Exercise = new PlanExerciseDto()
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
                            },
                        },
                    });
                }

                return new GetWorkoutRecordsResponse()
                {
                    WorkoutRecords = workoutRecords,
                };
            }
            catch (Exception ex)
            {
                return new GetWorkoutRecordsResponse(ex);
            }
        }
    }
}
