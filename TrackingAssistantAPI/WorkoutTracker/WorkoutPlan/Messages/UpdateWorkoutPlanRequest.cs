namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.Messages
{
    public class UpdateWorkoutPlanRequest
    {
        public int WorkoutPlanId { get; set; }
        public string Name { get; set; }
    }
}
