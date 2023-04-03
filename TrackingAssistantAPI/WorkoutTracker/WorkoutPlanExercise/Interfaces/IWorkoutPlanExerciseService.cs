using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.Messages;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.Interfaces
{
    public interface IWorkoutPlanExerciseService
    {
        public GetAllWorkoutPlansWithExercisesResponse GetAllWorkoutPlansWithExercises();
        public GetWorkoutPlanWithExercisesResponse GetWorkoutPlanWithExercises(int id);
        public ServiceResponseBase AddExerciseToWorkoutPlan(AddExerciseToWorkoutPlanRequest request);
        public ServiceResponseBase DeleteExerciseFromWorkoutPlan(DeleteExerciseFromWorkoutPlanRequest request);
        public ServiceResponseBase DeleteAllExercisesFromWorkoutPlan(int id);
        public ServiceResponseBase UpdateExerciseInWorkoutPlan(UpdateExerciseInWorkoutPlanRequest request);
    }
}
