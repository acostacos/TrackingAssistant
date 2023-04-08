using TrackingAssistantAPI.Shared;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Messages
{
    public class GetWorkoutRecordsResponse : ServiceResponseBase
    {
        public List<WorkoutRecordService>? WorkoutRecords { get; set; }
        public GetWorkoutRecordsResponse() : base() { }
        public GetWorkoutRecordsResponse(Exception ex) : base(ex) { }
    }
}
