using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.Interfaces;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.Messages;

namespace TrackingAssistantAPI.Controllers
{
    [Route("api/workoutplan/exercise")]
    [ApiController]
    public class WorkoutPlanExerciseController : ControllerBase
    {
        private readonly IWorkoutPlanExerciseService _planExerciseService;
        public WorkoutPlanExerciseController(IWorkoutPlanExerciseService planExerciseService)
        {
            _planExerciseService = planExerciseService; 
        }

        [HttpGet]
        public IActionResult GetAllWorkoutPlansWithExercises()
        {
            var response = _planExerciseService.GetAllWorkoutPlansWithExercises();
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            if (response.WorkoutPlanExercises == null) return NotFound();
            return Ok(response.WorkoutPlanExercises);
        }

        [HttpGet]
        public IActionResult GetWorkoutPlanWithExercises([FromRoute] int id)
        {
            var response = _planExerciseService.GetWorkoutPlanWithExercises(id);
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            if (response.WorkoutPlanExercise == null) return NotFound();
            return Ok(response.WorkoutPlanExercise);
        }

        [HttpPost("add")]
        public IActionResult AddExerciseToWorkoutPlan([FromBody] AddExerciseToWorkoutPlanRequest request)
        {
            var response = _planExerciseService.AddExerciseToWorkoutPlan(request);
            if (response.Exception != null)
            {
                if (response.Exception.Message == "Exerise already added to workout plan") return Conflict(response.Exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            }
            return Ok();
        }

        [HttpPost("delete")]
        public IActionResult DeleteExerciseFromWorkoutPlan([FromBody] DeleteExerciseFromWorkoutPlanRequest request)
        {
            var response = _planExerciseService.DeleteExerciseFromWorkoutPlan(request);
            if (response.Exception != null)
            {
                if (response.Exception.Message == "Exerise not found in workout plan") return NotFound(response.Exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            }
            return Ok();
        }

        [HttpPost("delete/all")]
        public IActionResult DeleteAllExercisesFromWorkoutPlan([FromBody] int id)
        {
            var response = _planExerciseService.DeleteAllExercisesFromWorkoutPlan(id);
            if (response.Exception != null)
            {
                if (response.Exception.Message == "Not Found") return NotFound();
                return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateExerciseInWorkoutPlan([FromBody] UpdateExerciseInWorkoutPlanRequest request)
        {
            var response = _planExerciseService.UpdateExerciseInWorkoutPlan(request);
            if (response.Exception != null)
            {
                if (response.Exception.Message == "Not Found") return NotFound();
                return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            }
            return Ok();
        }
    }
}
