using TrackingAssistantAPI.WorkoutTracker.Exercise.DTOs;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.DTOs;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.DTOs
{
    public class WorkoutPlanExerciseResponseDto
    {
        public int WorkoutPlanExerciseId { get; set; }
        public WorkoutPlanDto? WorkoutPlan { get; set; }
        public List<PlanExerciseDto>? Exercises { get; set; }
    }
}
