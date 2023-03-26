using Microsoft.AspNetCore.Mvc;
using TrackingAssistant.Service.WorkoutTracker.Interfaces;
using TrackingAssistant.Service.WorkoutTracker.Messages;

namespace TrackingAssistant.Controllers
{
    // TODO: Study Program.cs class first and what each method does

    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public WorkoutPlanController(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }

        // Figure out what information you need to put here
        [HttpGet]
        public IActionResult GetAllWorkoutPlan()
        {
            _workoutPlanService.GetAllWorkoutPlan();
            return Ok();
            //return NotFound(); if not found
        }

        [HttpGet]
        [Route("/{id}")] // Is this right?
        public IActionResult GetWorkoutPlan()
        {
            return Ok();
            //return NotFound(); if not found
        }

        [HttpPost]
        public IActionResult CreateWorkoutPlan([FromBody] CreateWorkoutPlanRequest request)
        {
            _workoutPlanService.CreateWorkoutPlan(request);
            return Ok();
            //return Created(); default response?
            //return Conflict(); if already exists
        }

        [HttpPut]
        public IActionResult UpdateWorkoutPlan()
        {
            return Ok();
            //return NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteWorkoutPlan()
        {
            return Ok();
        }
    }
}
