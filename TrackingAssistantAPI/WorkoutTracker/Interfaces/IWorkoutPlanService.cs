using TrackingAssistant.Service.WorkoutTracker.Messages;
using TrackingAssistantAPI.WorkoutTracker.Models;

namespace TrackingAssistant.Service.WorkoutTracker.Interfaces
{
    internal interface IWorkoutPlanService
    {
        public List<WorkoutPlan> GetAllWorkoutPlan();
        public void CreateWorkoutPlan(CreateWorkoutPlanRequest request);
    }
}
