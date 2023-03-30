using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.Messages;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.Interfaces
{
    public interface IWorkoutPlanService
    {
        public GetAllWorkoutsResponse GetAllWorkoutPlan();
        public GetWorkoutPlanResponse GetWorkoutPlan(int id);
        public CreateWorkoutPlanResponse CreateWorkoutPlan(CreateWorkoutPlanRequest request);
        public ServiceResponseBase DeleteWorkoutPlan(int id);
    }
}
