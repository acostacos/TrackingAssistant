using TrackingAssistantAPI.Shared;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.Messages
{
    public class CreateWorkoutPlanResponse : ServiceResponseBase
    {
        public int CreatedId { get; set; }
        public CreateWorkoutPlanResponse() : base() { }
        public CreateWorkoutPlanResponse(Exception ex) : base(ex) { }
    }
}
