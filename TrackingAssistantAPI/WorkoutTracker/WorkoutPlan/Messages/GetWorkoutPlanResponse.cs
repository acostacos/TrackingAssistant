using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.DTOs;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.Messages
{
    public class GetWorkoutPlanResponse : ServiceResponseBase
    {
        public WorkoutPlanDto? WorkoutPlan { get; set; }
        public GetWorkoutPlanResponse() : base() { }
        public GetWorkoutPlanResponse(Exception ex) : base(ex) { }
    }
}
