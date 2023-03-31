using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.Exercise.DTOs;

namespace TrackingAssistantAPI.WorkoutTracker.Exercise.Messages
{
    public class GetAllExercisesResponse : ServiceResponseBase
    {
        public List<ExerciseDto>? Exercises { get; set; }
        public GetAllExercisesResponse() : base() { }
        public GetAllExercisesResponse(Exception ex) : base(ex) { }
    }
}
