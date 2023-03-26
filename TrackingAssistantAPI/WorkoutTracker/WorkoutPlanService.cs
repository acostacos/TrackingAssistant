using System.Data;
using TrackingAssistant.Service.WorkoutTracker.Interfaces;
using TrackingAssistant.Service.WorkoutTracker.Messages;
using TrackingAssistantAPI.Database;
using TrackingAssistantAPI.WorkoutTracker.Models;

namespace TrackingAssistant.Service.WorkoutTracker
{
    internal class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly IDbRepository _dbRepository;
        public WorkoutPlanService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public List<WorkoutPlan> GetAllWorkoutPlan()
        {
            try
            {
                var query = "SELECT NAME FROM WORKOUTPLAN";
                var result = _dbRepository.ExecuteReader(query);

                var workoutPlans = new List<WorkoutPlan>();
                foreach (DataRow row in result.Rows)
                {
                    workoutPlans.Add(new WorkoutPlan()
                    {
                        Name = row["NAME"]?.ToString(),
                    });
                }

                return workoutPlans;
            } catch
            {
                throw;
            }
            
        }

        public void CreateWorkoutPlan(CreateWorkoutPlanRequest request)
        {

        }
    }
}
