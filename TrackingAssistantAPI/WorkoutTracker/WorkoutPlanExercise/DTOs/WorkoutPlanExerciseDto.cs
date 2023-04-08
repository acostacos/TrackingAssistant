using TrackingAssistantAPI.WorkoutTracker.Exercise.DTOs;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.DTOs;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.DTOs
{
    public class WorkoutPlanExerciseDto
    {
        public int WorkoutPlanExerciseId { get; set; }
        public int WorkoutPlanExerciseStartSetRange { get; set; }
        public int WorkoutPlanExerciseEndSetRange { get; set; }
        public int WorkoutPlanExerciseStartRepRange { get; set; }
        public int WorkoutPlanExerciseEndRepRange { get; set; }
        public int WorkoutPlanExerciseDay { get; set; }
        public string? WorkoutPlanExerciseDayName { get; set; }
        public int WorkoutPlanId { get; set; }
        public string? WorkoutPlanName { get; set; }
        public DateTime? WorkoutPlanCreateDate { get; set; }
        public int ExerciseId { get; set; }
        public string? ExerciseName { get; set; }
        public string? ExerciseTargetMuscles { get; set; }
    }
}
