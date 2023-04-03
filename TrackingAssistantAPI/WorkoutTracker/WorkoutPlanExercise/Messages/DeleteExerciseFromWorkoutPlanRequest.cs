namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.Messages
{
    public class DeleteExerciseFromWorkoutPlanRequest
    {
        public int ExerciseId { get; set; }
        public int WorkoutId { get; set; }
    }
}
