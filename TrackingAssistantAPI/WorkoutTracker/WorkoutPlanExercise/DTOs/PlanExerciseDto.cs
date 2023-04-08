using TrackingAssistantAPI.WorkoutTracker.Exercise.DTOs;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.DTOs
{
    public class PlanExerciseDto : ExerciseDto
    {
        public int StartSetRange { get; set; }
        public int EndSetRange { get; set; }
        public int StartRepRange { get; set; }
        public int EndRepRange { get; set; }
        public int Day { get; set; }
        public string? DayName { get; set; }
    }
}
