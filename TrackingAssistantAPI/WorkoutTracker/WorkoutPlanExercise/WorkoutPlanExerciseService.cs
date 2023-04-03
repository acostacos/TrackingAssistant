using TrackingAssistantAPI.Database;
using TrackingAssistantAPI.Shared;
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
            throw new NotImplementedException();
        }

        public GetWorkoutPlanWithExercisesResponse GetWorkoutPlanWithExercises(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResponseBase AddExerciseToWorkoutPlan(AddExerciseToWorkoutPlanRequest request)
        {
            throw new NotImplementedException();
        }

        public ServiceResponseBase DeleteExerciseFromWorkoutPlan(DeleteExerciseFromWorkoutPlanRequest request)
        {
            throw new NotImplementedException();
        }

        public ServiceResponseBase DeleteAllExercisesFromWorkoutPlan(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResponseBase UpdateExerciseInWorkoutPlan(UpdateExerciseInWorkoutPlanRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
