using TremendBoard.Infrastructure.Services.DTOs;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IJobTestService
    {
        void ReccuringAddTestProjectJob();

        void FireAndForgetRemoveTestProjectJob(ProjectDto projectDto);


    }
}
