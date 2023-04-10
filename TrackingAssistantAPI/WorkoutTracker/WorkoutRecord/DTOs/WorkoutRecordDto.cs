using TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.DTOs;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.DTOs
{
    public class WorkoutRecordDto
    {
        public int WorkoutRecordId { get; set; }
        public WorkoutPlanExerciseResponseDto? WorkoutPlanExercise { get; set; }
        public DateTime RecordTimestamp { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}
