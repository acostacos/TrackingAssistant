namespace TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Messages
{
    public class GetWorkoutRecordsRequest
    {
        public DateTime? RecordDate { get; set; }
        public int? WorkoutPlanExerciseId { get; set; }
    }
}
