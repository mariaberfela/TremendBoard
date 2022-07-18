using System;
using System.Collections.Generic;
using TremendBoard.Infrastructure.Services.DTOs;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class JobTestService: IJobTestService
    {

        private readonly IProjectService _projectService;

        public JobTestService(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public void ReccuringAddTestProjectJob()
        {
            var project = new ProjectDto
            {
                Id = "13",
                Name = "ProiectTest",
                Description = "Acest proiect este introdus in db in fiecare minut si apoi este sters",
                StatusMessage = "Mesaj",
                Deadline = DateTime.Now,
                ProjectStatus = "mesaj",
                ProjectUsers = new List<ProjectUserDto>(),
                Users = new List<UserDto>(),
                Roles = new List<RoleDto>()
            };

            _projectService.Create(project);
            _projectService.Delete(project);        //vreau mai mult sa vad ca merge Task-ul, nu neaparat sa se vada schimbari in bd
                                                    //Am adaugat in projectService de la tema ORM si Delete, ca nu voiam sa injectez unitOfWork aici
        }

        public void FireAndForgetRemoveTestProjectJob(ProjectDto project)
        {
            _projectService.Delete(project);        //Nu bagati in seama, aveam idei ciudate cu job-ul asta
        }
    }
}
