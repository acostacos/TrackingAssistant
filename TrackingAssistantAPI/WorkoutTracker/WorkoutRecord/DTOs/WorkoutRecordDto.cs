namespace TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.DTOs
{
    public class WorkoutRecordDto
    {
        public int WorkoutRecordId { get; set; }
        public int WorkoutPlanExerciseId { get; set; }
        public DateTime RecordTimestamp { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}
