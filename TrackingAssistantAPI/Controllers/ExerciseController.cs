using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingAssistantAPI.WorkoutTracker.Exercise.Interfaces;
using TrackingAssistantAPI.WorkoutTracker.Exercise.Messages;

namespace TrackingAssistantAPI.Controllers
{
    [Route("api/exercise")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public IActionResult GetAllExercise()
        {
            var response = _exerciseService.GetAllExercises();
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            if (response.Exercises == null) return NotFound();
            return Ok(response.Exercises);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetExercise([FromRoute] int id)
        {
            var response = _exerciseService.GetExercise(id);
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            if (response.Exercise == null) return NotFound();
            return Ok(response.Exercise);
        }

        [HttpPost]
        public IActionResult CreateExercise([FromBody] CreateExerciseRequest request)
        {
            var response = _exerciseService.CreateExercise(request);
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            return Created(nameof(GetExercise), new { id = response.CreatedId });
        }

        [HttpPut]
        public IActionResult UpdateExercise([FromBody] UpdateExerciseRequest request)
        {
            var response = _exerciseService.UpdateExercise(request);
            if (response.Exception != null)
            {
                if (response.Exception.Message == "Not Found") return NotFound();
                return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteExercise([FromBody] int id)
        {
            var response = _exerciseService.DeleteExercise(id);
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            return Ok();
        }
    }
}
