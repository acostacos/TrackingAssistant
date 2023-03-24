using Microsoft.AspNetCore.Mvc;

namespace TrackingAssistant.Controllers
{
    // TODO: Study Program.cs class first and what each method does

    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutPlanController : ControllerBase
    {
        public WorkoutPlanController()
        {
            // Figure out how to do dependency injection
        }

        // Figure out what information you need to put here
        [HttpGet]
        public IActionResult GetWorkoutPlan()
        {
            return Ok();
            //return NotFound(); if not found
        }

        [HttpPost]
        public IActionResult CreateWorkoutPlan()
        {
            return Ok();
            //return Created(); default response?
            //return Conflict(); if already exists
        }

        // Check difference between put and patch
        [HttpPatch]
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
