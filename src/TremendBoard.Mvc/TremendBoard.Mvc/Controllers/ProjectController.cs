using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TremendBoard.Infrastructure.Data.DTOs;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Enums;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;
using TremendBoard.Mvc.Models.UserViewModels;
using TremendBoard.Infrastructure.Services;

namespace TremendBoard.Mvc.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public IProjectService _projectService;

        public ProjectController(IUnitOfWork unitOfWork, IProjectService projectService)
        {
            _unitOfWork = unitOfWork;
            _projectService = projectService;
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
                var successful = _projectService.CreateProject(new ProjectDTO
            {
                Name = model.Name,
                Description = model.Description,
                CreatedDate = DateTime.Now,
                ProjectStatus = model.ProjectStatus,
                Deadline = model.Deadline
            }).Result;
            
            return successful
                ? View(model)
                : Json(new { status = "error", message = "error creating project" });

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var project = await _unitOfWork.Project.GetByIdAsync(id);
            if (project == null)
            {
                throw new ApplicationException($"Unable to load project with ID '{id}'.");
            }
            var users = await _unitOfWork.User.GetAllAsync();
            var usersView = users.Select(user => new UserDetailViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            });

            var roles = await _unitOfWork.Role.GetAllAsync();
            var rolesView = roles
                .Where(x => x.Name != Role.Admin.ToString())
                .OrderBy(x => x.Name)
                .Select(r => new ApplicationRoleDetailViewModel
                {
                    Id = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                });

            var model = new ProjectDetailViewModel
            {
                Id = id,
                Name = project.Name,
                Description = project.Description,
                ProjectStatus = project.ProjectStatus,
                Deadline = project.Deadline,
                ProjectUsers = new List<ProjectUserDetailViewModel>(),
                Users = usersView,
                Roles = rolesView
            };

            var userRoles = _unitOfWork.Project.GetProjectUserRoles(id);
            
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
            var project = await _unitOfWork.Project.GetByIdAsync(projectId);

            if (project == null)
            {
                ModelState.AddModelError("Error", "Unable to load the project");
                return View(model);
            }
            var successful2 = _projectService.UpdateProject(new ProjectDetailDTO
            {
                Id = projectId,
                Name = model.Name,
                Description = model.Description,
                ProjectStatus = model.ProjectStatus,
                Deadline = model.Deadline,
                ProjectUsers = (List<ProjectUserDetailViewDTO>)model.ProjectUsers,
                Users = (IEnumerable<UserDetailDTO>)model.Users,
                Roles = (IEnumerable<RoleDetailViewDTO>)model.Roles

        }).Result;

            var newmodel = new ProjectDetailViewModel
            {
                Id = projectId,
                Name = successful2.Name,
                Description = successful2.Description,
                ProjectStatus = successful2.ProjectStatus,
                Deadline = successful2.Deadline,
                ProjectUsers = successful2.ProjectUsers.Select(p => new ProjectUserDetailViewModel
                {
                    ProjectId = p.ProjectId,
                    UserId = p.UserId,
                    RoleId = p.RoleId,  
                    FirstName = p.FirstName,
                    LastName = p.LastName,  
                    UserRoleName = p.UserRoleName
                }).ToList(),
                Users = successful2.Users.Select(p => new UserDetailViewModel 
                {
                    Id = p.Id,
                    ApplicationRoles = model.Users?.FirstOrDefault(u => u.Id == p.Id).ApplicationRoles,
                    UserRoleId = p.UserRoleId,  
                    CurrentUserRole = p.CurrentUserRole,
                    FirstName = p.FirstName,
                    LastName= p.LastName,
                    Username = p.Username,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber
                }),
                Roles = successful2.Roles.Select(p => new ApplicationRoleDetailViewModel 
                { 
                    Id = p.Id,
                    RoleName = p.RoleName,
                    Description= p.Description,
                    UserRoleName= p.UserRoleName,
                    StatusMessage = p.StatusMessage
                })
            };
            model.StatusMessage = $"{project.Name} project has been updated";

            // return View(model);
            return View(newmodel);
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
    /*project.Name = model.Name;
           project.Description = model.Description;

           var users = await _unitOfWork.User.GetAllAsync();
           var usersView = users.Select(user => new UserDetailViewModel
           {
               Id = user.Id,
               FirstName = user.FirstName,
               LastName = user.LastName,
               Username = user.UserName,
               Email = user.Email,
               PhoneNumber = user.PhoneNumber
           });

           var roles = await _unitOfWork.Role.GetAllAsync();
           var rolesView = roles
               .Where(x => x.Name != Role.Admin.ToString())
               .OrderBy(x => x.Name)
               .Select(r => new ApplicationRoleDetailViewModel
               {
                   Id = r.Id,
                   RoleName = r.Name,
                   Description = r.Description
               });

           model.Roles = rolesView;
           model.Users = usersView;

           var userRoles = _unitOfWork.Project.GetProjectUserRoles(project.Id);

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
           }*/
}