using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.DTOs;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.Messages
{
    public class GetAllWorkoutPlansWithExercisesResponse : ServiceResponseBase
    {
        public List<WorkoutPlanExerciseResponseDto>? WorkoutPlanExercises { get; set; }
        public GetAllWorkoutPlansWithExercisesResponse() : base() { }
        public GetAllWorkoutPlansWithExercisesResponse(Exception ex) : base(ex) { }
    }
}
