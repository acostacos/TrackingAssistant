namespace TrackingAssistantAPI.WorkoutTracker.Exercise.Messages
{
    public class UpdateExerciseRequest
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public string TargetMuscles { get; set; }
    }
}
