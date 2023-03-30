using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.DTOs;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.Messages
{
    public class GetAllWorkoutsResponse : ServiceResponseBase
    {
        public List<WorkoutPlanDto>? WorkoutPlans { get; set; }
        public GetAllWorkoutsResponse() : base() { }
        public GetAllWorkoutsResponse(Exception ex) : base(ex) { }
    }
}
