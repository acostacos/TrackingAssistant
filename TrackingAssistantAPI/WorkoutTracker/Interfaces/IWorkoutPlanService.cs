using TrackingAssistant.Service.WorkoutTracker.Messages;
using TrackingAssistantAPI.WorkoutTracker.Models;

namespace TrackingAssistant.Service.WorkoutTracker.Interfaces
{
    public interface IWorkoutPlanService
    {
        public List<WorkoutPlan> GetAllWorkoutPlan();
        public WorkoutPlan? GetWorkoutPlan(int id);
        public int CreateWorkoutPlan(CreateWorkoutPlanRequest request);
        public void DeleteWorkoutPlan(int id);
    }
}
