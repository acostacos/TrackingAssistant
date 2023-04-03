namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.Messages
{
    public class AddExerciseToWorkoutPlanRequest
    {
        public int ExerciseId { get; set; }
        public int WorkoutId { get; set; }
        public int StartSetRange { get; set; }
        public int EndSetRange { get; set; }
        public int StartRepRange { get; set; }
        public int EndRepRange { get; set; }
        public int Day { get; set; }
        public string? DayName { get; set; }
    }
}
