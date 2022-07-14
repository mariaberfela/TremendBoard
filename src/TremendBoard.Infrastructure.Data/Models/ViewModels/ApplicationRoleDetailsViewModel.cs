using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Data.Models.ViewModels
{
    public class ApplicationRoleDetailsViewModel
    {
        public string Id { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public string UserRoleName { get; set; }

        public string StatusMessage { get; set; }
    }
}
