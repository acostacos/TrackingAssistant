namespace TrackingAssistantAPI.Shared
{
    public class ServiceResponseBase
    {
        public Exception? Exception { get; }
        public ServiceResponseBase() { }
        public ServiceResponseBase(Exception ex)
        {
            Exception = ex;
        }
    }
}
