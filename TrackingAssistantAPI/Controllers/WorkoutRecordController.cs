using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Interfaces;
using TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Messages;

namespace TrackingAssistantAPI.Controllers
{
    [Route("api/record/workout")]
    [ApiController]
    public class WorkoutRecordController : ControllerBase
    {
        private readonly IWorkoutRecordService _workoutRecordService;

        public WorkoutRecordController(IWorkoutRecordService workoutRecordService)
        {
            _workoutRecordService = workoutRecordService;
        }

        [HttpPost]
        public IActionResult AddWorkoutRecord([FromBody] CreateWorkoutRecordRequest request)
        {
            var response = _workoutRecordService.AddWorkoutRecord(request);
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateWorkoutRecord([FromBody] UpdateWorkoutRecordRequest request)
        {
            var response = _workoutRecordService.UpdateWorkoutRecord(request);
            if (response.Exception != null)
            {
                if (response.Exception.Message == "Not Found") return NotFound();
                return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteWorkoutRecord([FromBody] int id)
        {
            var response = _workoutRecordService.DeleteWorkoutRecord(id);
            if (response.Exception != null)
            {
                if (response.Exception.Message == "Not Found") return NotFound();
                return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllWorkoutRecords()
        {
            var response = _workoutRecordService.GetAllWorkoutRecords();
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            if (response.WorkoutRecords == null) return NotFound();
            return Ok(response.WorkoutRecords);
        }

        [HttpPost("get")]
        public IActionResult GetWorkoutPlan([FromBody] GetWorkoutRecordRequest request)
        {
            var response = _workoutRecordService.GetWorkoutRecord(request);
            if (response.Exception != null) return StatusCode(StatusCodes.Status500InternalServerError, response.Exception.Message);
            if (response.WorkoutRecords == null) return NotFound();
            return Ok(response.WorkoutRecords);
        }
    }
}
