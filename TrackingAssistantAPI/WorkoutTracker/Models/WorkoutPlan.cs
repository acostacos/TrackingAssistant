namespace TrackingAssistantAPI.WorkoutTracker.Models
{
    public class WorkoutPlan
    {
        public int WorkoutPlanId { get; set; }
        public string? Name { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
