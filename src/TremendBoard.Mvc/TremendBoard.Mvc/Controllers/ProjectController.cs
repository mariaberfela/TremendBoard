using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Data.Models.ViewModels;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Enums;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;
using TremendBoard.Mvc.Models.UserViewModels;

namespace TremendBoard.Mvc.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IUnitOfWork unitOfWork, IProjectService projectService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _projectService = projectService;
            _mapper = mapper;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> Index()
        {
            var projects = await _unitOfWork.Project.GetAllAsync();
            var projectsView = projects
                .Select(x => new ProjectDetailViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ProjectStatus = x.ProjectStatus,
                Deadline = x.Deadline
            });

            var model = new ProjectIndexViewModel
            {
                Projects = projectsView
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var project = _mapper.Map<Project>(model);
            await _projectService.Create(project);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var serviceModel = await _projectService.Edit(id);

            var model = new ProjectDetailViewModel
            {
                Id = serviceModel.Id,
                Name = serviceModel.Name,
                Description = serviceModel.Description,
                ProjectStatus = serviceModel.ProjectStatus,
                Deadline = serviceModel.Deadline,
                ProjectUsers = new List<ProjectUserDetailViewModel>(),
                Users = new List<UserDetailViewModel>(),
                Roles = new List<ApplicationRoleDetailViewModel>()
            };

            foreach (var projectUser in serviceModel.ProjectUsers)
            {
                model.ProjectUsers.Add(new ProjectUserDetailViewModel
                {
                    ProjectId = projectUser.ProjectId,
                    UserId = projectUser.UserId,
                    RoleId = projectUser.RoleId,
                    FirstName = projectUser.FirstName,
                    LastName = projectUser.LastName,
                    UserRoleName = projectUser.UserRoleName
                });
            }

            foreach (var user in serviceModel.Users)
            {
                model.Users.Append(new UserDetailViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserRoleId = user.UserRoleId,
                    ApplicationRoles = user.ApplicationRoles,
                    CurrentUserRole = user.CurrentUserRole
                });
            }

            foreach (var role in serviceModel.Roles)
            {
                model.Roles.Append(new ApplicationRoleDetailViewModel
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    UserRoleName = role.UserRoleName,
                    Description = role.Description,
                    StatusMessage = role.StatusMessage
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var projectId = model.Id;
            var project = await _unitOfWork.Project.GetByIdAsync(projectId);

            if (project == null)
            {
                ModelState.AddModelError("Error", "Unable to load the project");
                return View(model);
            }

            var serviceModel = _mapper.Map<ProjectDetailsViewModel>(model);
            serviceModel = await _projectService.Edit(serviceModel, project);

            model = new ProjectDetailViewModel
            {
                Id = serviceModel.Id,
                Name = serviceModel.Name,
                Description = serviceModel.Description,
                ProjectStatus = serviceModel.ProjectStatus,
                Deadline = serviceModel.Deadline,
                ProjectUsers = new List<ProjectUserDetailViewModel>(),
                Users = new List<UserDetailViewModel>(),
                Roles = new List<ApplicationRoleDetailViewModel>()
            };

            foreach (var projectUser in serviceModel.ProjectUsers)
            {
                model.ProjectUsers.Add(new ProjectUserDetailViewModel
                {
                    ProjectId = projectUser.ProjectId,
                    UserId = projectUser.UserId,
                    RoleId = projectUser.RoleId,
                    FirstName = projectUser.FirstName,
                    LastName = projectUser.LastName,
                    UserRoleName = projectUser.UserRoleName
                });
            }

            foreach (var user in serviceModel.Users)
            {
                model.Users.Append(new UserDetailViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserRoleId = user.UserRoleId,
                    ApplicationRoles = user.ApplicationRoles,
                    CurrentUserRole = user.CurrentUserRole
                });
            }

            foreach (var role in serviceModel.Roles)
            {
                model.Roles.Append(new ApplicationRoleDetailViewModel
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    UserRoleName = role.UserRoleName,
                    Description = role.Description,
                    StatusMessage = role.StatusMessage
                });
            }

            model.StatusMessage = $"{project.Name} project has been updated";

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(ProjectUserDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Error");
                return RedirectToAction(nameof(Edit), new { model.ProjectId });
            }

            var project = await _unitOfWork.Project.GetByIdAsync(model.ProjectId);

            var userRole = new ApplicationUserRole
            {
                Project = project,
                UserId = model.UserId,
                RoleId = model.RoleId
            };

            await _unitOfWork.UserRole.AddAsync(userRole);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Edit), new { id = model.ProjectId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var project = await _unitOfWork.Project.GetByIdAsync(id);
            
            if (project == null)
            {
                StatusMessage = "Project not found";
                return View();
            }

            var model = new ProjectDetailViewModel
            {
                Name = project.Name,
                Description = project.Description
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProjectDetailViewModel model)
        {
            var projectId = model.Id;

            var project = await _unitOfWork.Project.GetByIdAsync(projectId);
            
            if (project == null)
            {
                StatusMessage = "Project not found";
                return View();
            }

            _unitOfWork.Project.Remove(project);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}