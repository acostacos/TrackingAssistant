using TrackingAssistantAPI.Shared;

namespace TrackingAssistantAPI.WorkoutTracker.Exercise.Messages
{
    public class CreateExerciseResponse : ServiceResponseBase
    {
        public int CreatedId { get; set; }
        public CreateExerciseResponse() : base() { }
        public CreateExerciseResponse(Exception ex) : base(ex) { }
    }
}
