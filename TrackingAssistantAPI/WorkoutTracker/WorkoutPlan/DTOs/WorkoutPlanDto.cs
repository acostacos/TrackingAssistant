namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.DTOs
{
    public class WorkoutPlanDto
    {
        public int WorkoutPlanId { get; set; }
        public string? Name { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
