namespace TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Messages
{
    public class UpdateWorkoutRecordRequest
    {
        public int WorkoutRecordId { get; set; }
        public int WorkoutPlanExerciseId { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}
