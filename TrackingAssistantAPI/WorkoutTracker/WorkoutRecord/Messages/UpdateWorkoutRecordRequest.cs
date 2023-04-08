namespace TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Messages
{
    public class UpdateWorkoutRecordRequest
    {
        public int WorkoutPlanExerciseId { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}
