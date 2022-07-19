namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IJobTestService
    {
        void FireAndForgetJob();
        void RecurringJob();
        void DelayedJob();
        void ContinuationJob();
    }
}
