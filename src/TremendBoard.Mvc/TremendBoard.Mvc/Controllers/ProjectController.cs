using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.DTO;
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
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;

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
                Deadline = x.Deadline,
                ProjectStatus = x.ProjectStatus
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

            //await _unitOfWork.Project.AddAsync(new Project
            //{
            //    Name = model.Name,
            //    Description = model.Description,
            //    CreatedDate = DateTime.Now
            //});

            //await _unitOfWork.SaveAsync();

            var project = _mapper.Map<Project>(model);
            await _projectService.Create(project);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //var project = await _unitOfWork.Project.GetByIdAsync(id);

            //if (project == null)
            //{
            //    throw new ApplicationException($"Unable to load project with ID '{id}'.");
            //}

            var projectModelEdit = await _projectService.Edit(id);

            //var users = await _unitOfWork.User.GetAllAsync();
            //var usersView = users.Select(user => new UserDetailViewModel
            //{
            //    Id = user.Id,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Username = user.UserName,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber
            //});

            //var roles = await _unitOfWork.Role.GetAllAsync();
            //var rolesView = roles
            //    .Where(x => x.Name != Role.Admin.ToString())
            //    .OrderBy(x => x.Name)
            //    .Select(r => new ApplicationRoleDetailViewModel
            //    {
            //        Id = r.Id,
            //        RoleName = r.Name,
            //        Description = r.Description
            //    });

            //var model = new ProjectDetailViewModel
            //{
            //    Id = id,
            //    Name = project.Name,
            //    Description = project.Description,
            //    ProjectUsers = new List<ProjectUserDetailViewModel>(),
            //    Users = usersView,
            //    Roles = rolesView
            //};

            //var userRoles = _unitOfWork.Project.GetProjectUserRoles(id);

            //foreach (var userRole in userRoles)
            //{
            //    var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
            //    var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);

            //    var projectUser = new ProjectUserDetailViewModel
            //    {
            //        ProjectId = id,
            //        UserId = userRole.UserId,
            //        RoleId = userRole.RoleId,
            //        FirstName = user.FirstName,
            //        LastName = user.LastName,
            //        UserRoleName = role.Name
            //    };

            //    model.ProjectUsers.Add(projectUser);
            //}

            var usersEdit = new List<UserDetailViewModel>();
            foreach (var user in projectModelEdit.Users)
            {
                var userEdit = new UserDetailViewModel
                {
                    Id = user.Id,
                    ApplicationRoles = user.ApplicationRoles,
                    UserRoleId = user.UserRoleId,
                    CurrentUserRole = user.CurrentUserRole,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };

                usersEdit.Add(userEdit);
            };

            var rolesEdit = new List<ApplicationRoleDetailViewModel>();
            foreach (var role in projectModelEdit.Roles)
            {
                var roleEdit = new ApplicationRoleDetailViewModel
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    Description = role.Description,
                    UserRoleName = role.UserRoleName,
                    StatusMessage = role.StatusMessage
                };

                rolesEdit.Add(roleEdit);
            };

            var model = new ProjectDetailViewModel
            {
                Id = id,
                Name = projectModelEdit.Name,
                Description = projectModelEdit.Description,
                Deadline = projectModelEdit.Deadline,
                ProjectStatus = projectModelEdit.ProjectStatus,
                ProjectUsers = new List<ProjectUserDetailViewModel>(),
                Users = usersEdit,
                Roles = rolesEdit
            };

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

            var projectModelEdit = _mapper.Map<ProjectDTO>(model);
            projectModelEdit = await _projectService.Edit(projectModelEdit, project);

            //project.Name = model.Name;
            //project.Description = model.Description;

            //var users = await _unitOfWork.User.GetAllAsync();
            //var usersView = users.Select(user => new UserDetailViewModel
            //{
            //    Id = user.Id,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Username = user.UserName,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber
            //});

            //var roles = await _unitOfWork.Role.GetAllAsync();
            //var rolesView = roles
            //    .Where(x => x.Name != Role.Admin.ToString())
            //    .OrderBy(x => x.Name)
            //    .Select(r => new ApplicationRoleDetailViewModel
            //    {
            //        Id = r.Id,
            //        RoleName = r.Name,
            //        Description = r.Description
            //    });

            //model.Roles = rolesView;
            //model.Users = usersView;

            //var userRoles = _unitOfWork.Project.GetProjectUserRoles(project.Id);

            //model.ProjectUsers = new List<ProjectUserDetailViewModel>();

            //foreach (var userRole in userRoles)
            //{
            //    var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
            //    var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);
            //    var projectUser = new ProjectUserDetailViewModel
            //    {
            //        ProjectId = project.Id,
            //        UserId = userRole.UserId,
            //        RoleId = userRole.RoleId,
            //        FirstName = user.FirstName,
            //        LastName = user.LastName,
            //        UserRoleName = role.Name
            //    };

            //    model.ProjectUsers.Add(projectUser);
            //}

            //_unitOfWork.Project.Update(project);
            //await _unitOfWork.SaveAsync();

            //model.StatusMessage = $"{project.Name} project has been updated";

            var usersEdit = new List<UserDetailViewModel>();
            foreach (var user in projectModelEdit.Users)
            {
                var userEdit = new UserDetailViewModel
                {
                    Id = user.Id,
                    ApplicationRoles = user.ApplicationRoles,
                    UserRoleId = user.UserRoleId,
                    CurrentUserRole = user.CurrentUserRole,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };

                usersEdit.Add(userEdit);
            };

            var rolesEdit = new List<ApplicationRoleDetailViewModel>();
            foreach (var role in projectModelEdit.Roles)
            {
                var roleEdit = new ApplicationRoleDetailViewModel
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    Description = role.Description,
                    UserRoleName = role.UserRoleName,
                    StatusMessage = role.StatusMessage
                };

                rolesEdit.Add(roleEdit);
            };

            var modelProject = new ProjectDetailViewModel
            {
                Id = projectModelEdit.Id,
                Name = projectModelEdit.Name,
                Description = projectModelEdit.Description,
                ProjectStatus = projectModelEdit.ProjectStatus,
                Deadline = projectModelEdit.Deadline,
                ProjectUsers = new List<ProjectUserDetailViewModel>(),
                Users = usersEdit,
                Roles = rolesEdit
            };

            return View(modelProject);
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