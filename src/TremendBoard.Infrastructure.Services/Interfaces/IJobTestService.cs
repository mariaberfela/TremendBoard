using System.Collections.Generic;
using TremendBoard.Infrastructure.Data.Models;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IJobTestService
    {
        void FireAndForgetJob();
        void ReccuringJob();
        void DelayedJob();
        void ContinuationJob();
        void CheckProjectsDeadline();
    }
}
