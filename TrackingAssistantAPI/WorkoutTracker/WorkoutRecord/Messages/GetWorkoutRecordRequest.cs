namespace TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Messages
{
    public class GetWorkoutRecordRequest
    {
        public DateTime Date { get; set; }
        public int WorkoutPlanExerciseId { get; set; }
    }
}
