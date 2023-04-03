using TrackingAssistantAPI.WorkoutTracker.Exercise.DTOs;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.DTOs;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.DTOs
{
    public class WorkoutPlanExerciseDto
    {
        public int WorkoutPlanExerciseId { get; set; }
        public WorkoutPlanDto? WorkoutPlan { get; set; }
        public ExerciseDto? Exercise { get; set; }
        public int StartSetRange { get; set; }
        public int EndSetRange { get; set; }
        public int StartRepRange { get; set; }
        public int EndRepRange { get; set; }
        public int Day { get; set; }
        public string? DayName { get; set; }
    }
}
