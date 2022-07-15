using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Mvc.Models.RoleViewModels;
using TremendBoard.Mvc.Models.UserViewModels;

namespace TremendBoard.Mvc.Models.ProjectViewModels
{
    public class ProjectDetailViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusMessage { get; set; }

        public string ProjectStatus { get; set; }

        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode=true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Deadline { get; set; }

        public IList<ProjectUserDetailViewModel> ProjectUsers { get; set; }
        public IEnumerable<UserDetailViewModel> Users { get; set; }
        public IEnumerable<ApplicationRoleDetailViewModel> Roles { get; set; }


        public ProjectDetailViewModel() { }
        public ProjectDetailViewModel(Project project, IEnumerable<UserDetailViewModel> usersView, IEnumerable<ApplicationRoleDetailViewModel> rolesView)
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            ProjectStatus = project.ProjectStatus;
            Deadline = project.Deadline;
            ProjectUsers = new List<ProjectUserDetailViewModel>();
            Users = usersView;
            Roles = rolesView;
        }
    }
}
