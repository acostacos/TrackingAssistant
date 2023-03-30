using Microsoft.AspNetCore.Mvc;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.Interfaces;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlan.Messages;

namespace TrackingAssistantAPI.Controllers
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
            var response = _workoutPlanService.GetAllWorkoutPlan();
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            if (response.WorkoutPlans == null) return NotFound();
            return Ok(response.WorkoutPlans);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetWorkoutPlan([FromRoute] int id)
        {
            var response = _workoutPlanService.GetWorkoutPlan(id);
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            if (response.WorkoutPlan == null) return NotFound();
            return Ok(response.WorkoutPlan);
        }

        [HttpPost]
        public IActionResult CreateWorkoutPlan([FromBody] CreateWorkoutPlanRequest request)
        {
            var response = _workoutPlanService.CreateWorkoutPlan(request);
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            return Created(nameof(GetWorkoutPlan), new { id = response.CreatedId });
        }

        [HttpPut]
        public IActionResult UpdateWorkoutPlan()
        {
            return Ok();
            //return NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteWorkoutPlan([FromBody] int id)
        {
            var response = _workoutPlanService.DeleteWorkoutPlan(id);
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            return Ok();
        }
    }
}
