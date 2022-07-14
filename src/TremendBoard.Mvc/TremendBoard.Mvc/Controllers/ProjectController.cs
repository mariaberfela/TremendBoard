using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;
using TremendBoard.Mvc.Models.UserViewModels;


namespace TremendBoard.Mvc.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectService _projectService;

        public ProjectController(IUnitOfWork unitOfWork, IProjectService projectService)
        {
            _projectService = projectService;
            _unitOfWork = unitOfWork;
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
                    Description = x.Description
                }) ;

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
           
            await _projectService.Create(new Project
            {
                Name = model.Name,
                Description = model.Description,
                CreatedDate = DateTime.Now,
                ProjectStatus = model.ProjectStatus,
                Deadline = model.Deadline
            });
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var project = await _projectService.Edit(id);


            var users = new List<UserDetailViewModel>();

            foreach(var user in project.Users)
            {
                var usser = new UserDetailViewModel
                {
                    Id = user.Id,
                    UserRoleId = user.UserRoleId,
                    CurrentUserRole = user.CurrentUserRole,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
                users.Add(usser);
            }

            var roles = new List<ApplicationRoleDetailViewModel>();

            foreach(var role in project.Roles)
            {
                var rolle = new ApplicationRoleDetailViewModel
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    Description = role.Description,
                    UserRoleName = role.UserRoleName,
                    StatusMessage = role.StatusMessage
                };
                roles.Add(rolle);
            }

            var projectDetail = new ProjectDetailViewModel
            {
                Id = id,
                Name = project.Name,
                Description = project.Description,
                StatusMessage = project.StatusMessage,
                Deadline = project.Deadline,
                ProjectStatus = project.ProjectStatus,
                ProjectUsers = new List<ProjectUserDetailViewModel>(),
                Users = users,
                Roles = roles
            };

            return View(projectDetail);
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

            var project2 = new ProjectDetails();

            var proj = await _projectService.Edit(project2);

            var users = new List<UserDetailViewModel>();

            foreach (var user in proj.Users)
            {
                var usser = new UserDetailViewModel
                {
                    Id = user.Id,
                    UserRoleId = user.UserRoleId,
                    CurrentUserRole = user.CurrentUserRole,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
                users.Add(usser);
            }

            var roles = new List<ApplicationRoleDetailViewModel>();

            foreach (var role in proj.Roles)
            {
                var rolle = new ApplicationRoleDetailViewModel
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    Description = role.Description,
                    UserRoleName = role.UserRoleName,
                    StatusMessage = role.StatusMessage
                };
                roles.Add(rolle);
            }

            var projectDetail = new ProjectDetailViewModel
            {
                Id = proj.Id,
                Name = proj.Name,
                Description = proj.Description,
                StatusMessage = proj.StatusMessage,
                Deadline = proj.Deadline,
                ProjectStatus = proj.ProjectStatus,
                ProjectUsers = new List<ProjectUserDetailViewModel>(),
                Users = users,
                Roles = roles
            };

            return View(projectDetail);
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