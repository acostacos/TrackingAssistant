using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.Exercise.Messages;

namespace TrackingAssistantAPI.WorkoutTracker.Exercise.Interfaces
{
    public interface IExerciseService
    {
        public GetAllExercisesResponse GetAllExercises();
        public GetExerciseResponse GetExercise(int id);
        public CreateExerciseResponse CreateExercise(CreateExerciseRequest request);
        public ServiceResponseBase UpdateExercise(UpdateExerciseRequest request);
        public ServiceResponseBase DeleteExercise(int id);
    }
}
