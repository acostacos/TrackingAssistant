using Microsoft.AspNetCore.Mvc;
using TrackingAssistant.Service.WorkoutTracker.Interfaces;
using TrackingAssistant.Service.WorkoutTracker.Messages;

namespace TrackingAssistant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public WorkoutPlanController(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }

        [HttpGet]
        public IActionResult GetAllWorkoutPlan()
        {
            var workoutPlans = _workoutPlanService.GetAllWorkoutPlan();
            if (workoutPlans == null) return NotFound();
            return Ok(workoutPlans);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetWorkoutPlan([FromRoute] int id)
        {
            var workoutPlan = _workoutPlanService.GetWorkoutPlan(id);
            if (workoutPlan == null) return NotFound();
            return Ok(workoutPlan);
        }

        [HttpPost]
        public IActionResult CreateWorkoutPlan([FromBody] CreateWorkoutPlanRequest request)
        {
            var id = _workoutPlanService.CreateWorkoutPlan(request);
            return Created(nameof(GetWorkoutPlan), new { id = id });
        }

        [HttpPut]
        public IActionResult UpdateWorkoutPlan()
        {
            return Ok();
            //return NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteWorkoutPlan([FromRoute] int id)
        {
            return Ok();
        }
    }
}
