using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;
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
                Description = x.Description
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

            var record = _mapper.Map<ProjectDetailViewModel, Project>(model);
            await _projectService.AddAsync(record);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var project = await _projectService.GetAsync(id);

            if (project == null)
            {
                throw new ApplicationException($"Unable to load project with ID '{id}'.");
            }

            var users = await _unitOfWork.User.GetAllAsync();
            var usersView = _mapper.Map<List<UserDetailViewModel>>(users);
            
            var roles = (await _unitOfWork.Role.GetAllAsync()).Where(x => x.Name != Role.Admin.ToString()).OrderBy(x => x.Name);
            var rolesView = roles.Select(r => _mapper.Map<ApplicationRole, ApplicationRoleDetailViewModel>(r));

            var model = new ProjectDetailViewModel
            {
                Id = id,
                Name = project.Name,
                Description = project.Description,
                ProjectUsers = new List<ProjectUserDetailViewModel>(),
                Status = project.Status,
                Deadline = project.Deadline,
                Users = usersView,
                Roles = rolesView
            };

            var userRoles = _projectService.GetProjectUserRoles(id);

            foreach (var userRole in userRoles)
            {
                var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
                var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);
                
                var projectUser = new ProjectUserDetailViewModel
                {
                    ProjectId = id,
                    UserId = userRole.UserId,
                    RoleId = userRole.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserRoleName = role.Name
                };

                model.ProjectUsers.Add(projectUser);
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
            var project = await _projectService.GetAsync(projectId);

            if (project == null)
            {
                ModelState.AddModelError("Error", "Unable to load the project");
                return View(model);
            }

            _mapper.Map(model, project);

            var users = await _unitOfWork.User.GetAllAsync();
            var usersView = users.Select(user => _mapper.Map<ApplicationUser, UserDetailViewModel>(user));

            var roles = await _unitOfWork.Role.GetAllAsync();
            var rolesView = roles
                .Where(x => x.Name != Role.Admin.ToString())
                .OrderBy(x => x.Name)
                .Select(r => _mapper.Map<ApplicationRole, ApplicationRoleDetailViewModel>(r));

            model.Roles = rolesView;
            model.Users = usersView;

            var userRoles = _projectService.GetProjectUserRoles(project.Id);

            model.ProjectUsers = new List<ProjectUserDetailViewModel>();

            foreach (var userRole in userRoles)
            {
                var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
                var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);
                var projectUser = new ProjectUserDetailViewModel
                {
                    ProjectId = project.Id,
                    UserId = userRole.UserId,
                    RoleId = userRole.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserRoleName = role.Name
                };

                model.ProjectUsers.Add(projectUser);
            }

            await _projectService.Update(project);
            model.StatusMessage = "Project updated successfully";

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