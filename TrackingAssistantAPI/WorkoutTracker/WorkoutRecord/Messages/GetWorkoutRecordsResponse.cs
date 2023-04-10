using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.DTOs;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Messages
{
    public class GetWorkoutRecordsResponse : ServiceResponseBase
    {
        public List<WorkoutRecordDto>? WorkoutRecords { get; set; }
        public GetWorkoutRecordsResponse() : base() { }
        public GetWorkoutRecordsResponse(Exception ex) : base(ex) { }
    }
}
