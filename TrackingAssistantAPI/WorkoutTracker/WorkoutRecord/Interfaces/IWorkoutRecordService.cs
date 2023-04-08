using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Messages;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutRecord.Interfaces
{
    public interface IWorkoutRecordService
    {
        public ServiceResponseBase AddWorkoutRecord(CreateWorkoutRecordRequest request);
        public ServiceResponseBase UpdateWorkoutRecord(UpdateWorkoutRecordRequest request);
        public ServiceResponseBase DeleteWorkoutRecord(int id);
        public GetWorkoutRecordsResponse GetAllWorkoutRecords();
        public GetWorkoutRecordsResponse GetWorkoutRecord(GetWorkoutRecordRequest request);
    }
}
