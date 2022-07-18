using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Services.DTOs
{
    public class ProjectUserDto
    {
        public string ProjectId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRoleName { get; set; }
    }
}
