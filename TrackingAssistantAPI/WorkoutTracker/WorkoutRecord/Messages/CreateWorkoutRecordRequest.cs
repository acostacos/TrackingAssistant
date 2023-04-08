namespace TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Messages
{
    public class CreateWorkoutRecordRequest
    {
        public int WorkoutPlanExerciseId { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}
