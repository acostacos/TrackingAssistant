using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.Exercise.DTOs;

namespace TrackingAssistantAPI.WorkoutTracker.Exercise.Messages
{
    public class GetExerciseResponse : ServiceResponseBase
    {
        public ExerciseDto? Exercise { get; set; }
        public GetExerciseResponse() : base() { }
        public GetExerciseResponse(Exception ex) : base(ex) { }
    }
}
