using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Data.Models.ViewModels
{
    public class ProjectUserDetailsViewModel
    {
        public string ProjectId { get; set; }

        public string UserId { get; set; }

        public string RoleId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserRoleName { get; set; }
    }
}
