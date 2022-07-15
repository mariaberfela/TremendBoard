using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models.Identity;

namespace TremendBoard.Infrastructure.Services.DTOs
{
    public class ProjectUserDetailDTO
    {
        public string ProjectId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRoleName { get; set; }


        public ProjectUserDetailDTO() { }

        public ProjectUserDetailDTO(string projectId, ApplicationUserRole userRole, ApplicationUser user, ApplicationRole role)
        {
            ProjectId = projectId;
            UserId = userRole.UserId;
            RoleId = userRole.RoleId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserRoleName = role.Name;
        }
    }
}
